USE CatHotel;
GO

-- a. 
-- Cada vez que se crea una nueva reserva se debe crear un registro de auditoria con todos
-- los datos ingresados en una tabla ReservaLog (definir su estructura libremente). Y
-- adicionalmente cada vez que se modifica el campo monto de una reserva: debe registrar
-- monto previo y nuevo monto en la tabla ReservaLog.
-- En todos los casos se debe grabar fecha-hora de registro, usuario(login), nombre de equipo
-- desde el que se realizó la modificación.
CREATE TABLE ReservaLog (
    reservaID INT NOT NULL,
    gatoID INT NOT NULL,
    habitacionNombre CHAR(30) NOT NULL,
    reservaFechaInicio DATE NOT NULL,
    reservaFechaFin DATE NOT NULL,
    reservaMonto DECIMAL(7,2) NOT NULL,
    reservaMontoPrevio DECIMAL(7,2) NOT NULL,
    fechaHoraRegistro DATETIME NOT NULL,
    usuarioLogin VARCHAR(50) NOT NULL,
    equipoNombre VARCHAR(50) NOT NULL,
    CONSTRAINT FK_ReservaLog_Reserva FOREIGN KEY (reservaID) REFERENCES Reserva(reservaID)
);
GO

-- Trigger para insertar registros en ReservaLog al crear una nueva reserva
CREATE TRIGGER TR_ReservaLog_Insert_Update
ON Reserva
AFTER INSERT, UPDATE
AS
BEGIN
	-- Si estoy en un Insert
	IF EXISTS (SELECT * FROM inserted) AND NOT EXISTS (SELECT * FROM deleted)
		BEGIN
			INSERT INTO ReservaLog (
				reservaID, 
				gatoID, 
				habitacionNombre, 
				reservaFechaInicio, 
				reservaFechaFin, 
				reservaMonto, 
				reservaMontoPrevio, 
				fechaHoraRegistro, 
				usuarioLogin, 
				equipoNombre
			)
			SELECT 
				I.reservaID, 
				I.gatoID, 
				I.habitacionNombre, 
				I.reservaFechaInicio, 
				I.reservaFechaFin, 
				I.reservaMonto, 
				I.reservaMonto, 
				GETDATE(), 
				SUSER_SNAME(), 
				HOST_NAME()
			FROM INSERTED I;
		END
	-- Si estoy en un UPDATE		
	IF EXISTS (SELECT * FROM inserted) AND EXISTS (SELECT * FROM deleted)
		BEGIN
			IF UPDATE(reservaMonto)--Si solo se modifica el campo reservaMonto
				BEGIN
					INSERT INTO ReservaLog (
						reservaID, 
						gatoID, 
						habitacionNombre, 
						reservaFechaInicio, 
						reservaFechaFin, 
						reservaMonto, 
						reservaMontoPrevio, 
						fechaHoraRegistro, 
						usuarioLogin, 
						equipoNombre
					)
					SELECT 
						I.reservaID,
						I.gatoID,
						I.habitacionNombre,
						I.reservaFechaInicio,
						I.reservaFechaFin,
						I.reservaMonto,
						D.reservaMonto,
						GETDATE(),
						SUSER_SNAME(),
						HOST_NAME()
					FROM INSERTED I
					INNER JOIN DELETED D ON I.reservaID = D.reservaID;
				END
			END   
END;
GO



-- b. Antes de insertar una nueva reserva, se debe controlar posibles solapamientos de reservas 
-- (un gato no podría estar alojado simultáneamente 2 veces en el hotel).  
-- Se debe dar de alta las reservas válidas y simplemente ignorar las reservas solapadas 
CREATE TRIGGER TR_Verificar_Solapamiento_Reserva
ON Reserva
INSTEAD OF INSERT
AS
BEGIN
    INSERT INTO Reserva(gatoID, habitacionNombre, reservaFechaInicio, reservaFechaFin, reservaMonto)
    SELECT I.gatoID, I.habitacionNombre, I.reservaFechaInicio, I.reservaFechaFin, I.reservaMonto
    FROM Inserted I
    WHERE NOT EXISTS (
                        SELECT *
                        FROM Reserva R
                        WHERE R.gatoID = I.gatoID AND
                                    (
                                        -- Control del rango de fechas
                                        (I.reservaFechaInicio BETWEEN R.reservaFechaInicio AND R.reservaFechaFin)
                                        OR (I.reservaFechaFin BETWEEN R.reservaFechaInicio AND R.reservaFechaFin)
                                        OR (R.reservaFechaInicio BETWEEN I.reservaFechaInicio AND I.reservaFechaFin)
                                    )
                    )
END
GO

