USE CatHotel;
GO

DECLARE @__reservaID INT;

EXEC SP_ReservarHabitacion 
    5,
    'Habitacion5',
    '2024-11-01',
    '2024-11-02',
    600.00,
    @__reservaID OUTPUT;

EXEC SP_ReservarHabitacion 
    6,
    'Habitacion5',
    '2024-11-09',
    '2024-11-10',
    700.00,
    @__reservaID OUTPUT;

-- @__reservaID debe ser 0 pues la habitacion5 ya esta reservada, la misma tiene capacidad para 1 solo animal.
IF @__reservaID = 0
BEGIN
    SELECT CONCAT('ESPERADO: ', @__reservaID) AS Result;
END
ELSE
BEGIN
    SELECT CONCAT('NO ESPERADO: ', @__reservaID) AS Result;
END
GO