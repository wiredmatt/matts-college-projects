-- a. Escribir un procedimiento almacenado para reservar una habitación.
-- Se debe actualizar el estado de DISPONIBLE a LLENA si se alcanzó la capacidad de la
-- habitación con la reserva en cuestión
-- No permitir realizar la reserva si el estado de la habitación es LLENA o LIMPIANDO.
-- Se debe retornar el número de reserva asignado (cero sino se logró reservar)
USE CatHotel;
GO

CREATE PROCEDURE SP_ReservarHabitacion
    @gatoID INT,
    @habitacionNombre CHAR(30),
    @reservaFechaInicio DATE,
    @reservaFechaFin DATE,
    @reservaMonto DECIMAL(7,2),
    @reservaID INT OUTPUT
AS
BEGIN
    DECLARE @habitacionEstado VARCHAR(20);
    DECLARE @habitacionCapacidad INT;
    DECLARE @habitacionOcupantes INT;

    SELECT @habitacionEstado = habitacionEstado, @habitacionCapacidad = habitacionCapacidad
    FROM Habitacion
    WHERE habitacionNombre = @habitacionNombre;

    IF @habitacionEstado = 'DISPONIBLE'
    BEGIN
        SELECT @habitacionOcupantes = COUNT(*)
        FROM Reserva
        WHERE habitacionNombre = @habitacionNombre
        AND reservaFechaInicio <= @reservaFechaFin
        AND reservaFechaFin >= @reservaFechaInicio;

        IF @habitacionOcupantes < @habitacionCapacidad
        BEGIN
            INSERT INTO Reserva (gatoID, habitacionNombre, reservaFechaInicio, reservaFechaFin, reservaMonto)
            VALUES (@gatoID, @habitacionNombre, @reservaFechaInicio, @reservaFechaFin, @reservaMonto);

            -- Conseguir la ID de la reserva recien insertada.
            -- Si la base de datos estuviera bien diseñada, 
            -- no sería necesario hacer esto ya que podriamos 
            -- usar SCOPE_IDENTITY().
            -- Al estar usando un trigger INSTEAD OF INSERT en la tabla Reserva,
            -- no se producen los valores IDENTITY de la tabla Reserva al generar
            -- un nuevo insert.
            --
            -- La forma correcta de resolver este problema seria empleando
            -- SQL Transaction y un bloque TRY-CATCH para manejar errores.
            SELECT TOP 1 @reservaID = reservaID 
            FROM ReservaLog 
            WHERE gatoID = @gatoID AND habitacionNombre = @habitacionNombre 
            AND reservaFechaInicio = @reservaFechaInicio 
            AND reservaFechaFin = @reservaFechaFin
            AND reservaMonto = @reservaMonto
            AND usuarioLogin = SUSER_SNAME()
            AND equipoNombre = HOST_NAME()
            AND fechaHoraRegistro < GETDATE() -- el insert es generado antes de ejecutar esta consulta.
            ORDER BY reservaID DESC;

            IF ISNULL(@reservaID, 0) > 0
            BEGIN
                UPDATE Habitacion
                SET habitacionEstado = CASE
                    WHEN @habitacionOcupantes + 1 = @habitacionCapacidad THEN 'LLENA'
                    ELSE 'DISPONIBLE'
                    END 
                WHERE habitacionNombre = @habitacionNombre;
            END
            ELSE
            BEGIN
                SET @reservaID = 0;
            END

        END
        ELSE
        BEGIN
            SET @reservaID = 0;
        END
    END
    ELSE
    BEGIN
        SET @reservaID = 0;
    END
END

GO


-- b. Mediante una función que reciba un nombre de servicio, devolver un booleano indicando si 
-- este año el servicio fue contratado más veces que el año pasado
CREATE FUNCTION F_SERVICIO_CONTRATADO_MAS_VECES_ANIO
(
@NOMBRE_SERVICIO VARCHAR(30)
)
RETURNS BIT
AS
BEGIN

	DECLARE @RESULTADO BIT;
	DECLARE @CONT_ESTE_ANIO INT;
    DECLARE @CONT_ANIO_PASADO INT;	

 -- Cantidad de contrataciones de este año
    SELECT @CONT_ESTE_ANIO = SUM(RS.cantidad)
    FROM Reserva_Servicio RS
	INNER JOIN Reserva R ON RS.reservaID = R.reservaID
    WHERE RS.servicioNombre = @NOMBRE_SERVICIO
    AND YEAR(R.reservaFechaInicio) = YEAR(GETDATE());

-- Contamos de contrataciones del año pasado
    SELECT @CONT_ANIO_PASADO = SUM(RS.cantidad)
    FROM Reserva_Servicio RS
	INNER JOIN Reserva R ON RS.reservaID = R.reservaID
    WHERE RS.servicioNombre = @NOMBRE_SERVICIO
    AND YEAR(R.reservaFechaInicio) = YEAR(GETDATE()) - 1;

-- Comparamos y establecemos el resultado
    SET @RESULTADO = CASE 
                        WHEN @CONT_ESTE_ANIO > @CONT_ANIO_PASADO 
                        THEN 1 
                        ELSE 0 
                     END;

    RETURN @RESULTADO;
	
END
GO


