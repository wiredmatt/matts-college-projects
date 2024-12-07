USE CatHotel;
GO

-- Al correr la precarga, ya se ingresan varios registros a esta tabla
DECLARE @CantidadReservasInicial INT;

SELECT @CantidadReservasInicial = COUNT(*) FROM Reserva;

-- Ya hay una reserva en la tabla con estos mismos datos
INSERT INTO Reserva VALUES (1,'Habitacion1', '2023-11-01', '2023-11-08', 1000.00);

DECLARE @CantidadReservasDespues INT;

SELECT @CantidadReservasDespues = COUNT(*) FROM Reserva;

IF @CantidadReservasDespues > @CantidadReservasInicial
BEGIN
    SELECT CONCAT('NO_ESPERADO: ', @CantidadReservasDespues, '>', @CantidadReservasInicial) AS Result;
END
ELSE
BEGIN
    SELECT CONCAT('ESPERADO: ', @CantidadReservasDespues, '=', @CantidadReservasInicial) AS Result;
END
GO