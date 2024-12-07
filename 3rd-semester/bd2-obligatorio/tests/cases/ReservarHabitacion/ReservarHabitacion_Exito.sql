USE CatHotel;
GO

DECLARE @__reservaID INT;

EXEC SP_ReservarHabitacion 
    5,
    'Habitacion11',
    '2024-11-01',
    '2024-11-02',
    600.00,
    @__reservaID OUTPUT;

-- @__reservaID debe ser distinto de 0 pues la habitacion5 tiene capacidad para 1 animal.
IF @__reservaID > 0
BEGIN
    SELECT CONCAT('ESPERADO: ', @__reservaID) AS Result;
END
ELSE
BEGIN
    SELECT CONCAT('NO_ESPERADO: ', @__reservaID) AS Result;
END
GO