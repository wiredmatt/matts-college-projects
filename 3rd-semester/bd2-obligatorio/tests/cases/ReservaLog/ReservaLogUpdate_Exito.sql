USE CatHotel;
GO

DECLARE @__reservaID INT;
DECLARE @__reservaMontoInicial INT = 600.00;

EXEC SP_ReservarHabitacion 
    5,
    'Habitacion12',
    '2025-11-01',
    '2025-11-02',
    @__reservaMontoInicial,
    @__reservaID OUTPUT;

DECLARE @__reservaMontoNuevo INT = 700.00;

UPDATE Reserva
SET reservaMonto = @__reservaMontoNuevo
WHERE reservaID = @__reservaID;

DECLARE @__reservaMontoPostUpdate INT;
DECLARE @__reservaMontoAnteriorPostUpdate INT;

SELECT 
    @__reservaMontoPostUpdate = reservaMonto,
    @__reservaMontoAnteriorPostUpdate = reservaMontoPrevio
FROM ReservaLog
WHERE reservaID = @__reservaID;

IF @__reservaMontoPostUpdate = @__reservaMontoNuevo AND @__reservaMontoAnteriorPostUpdate = @__reservaMontoInicial
BEGIN
    SELECT CONCAT('ESPERADO: ', @__reservaMontoNuevo, '=', @__reservaMontoPostUpdate, 
                  ' & ',  
                  @__reservaMontoAnteriorPostUpdate, '=', @__reservaMontoInicial) 
    AS Result;
END
ELSE
BEGIN
    SELECT CONCAT('NO_ESPERADO: ', @__reservaMontoNuevo, '=', ISNULL(@__reservaMontoPostUpdate, 0), 
                  ' & ',  
                  @__reservaMontoAnteriorPostUpdate, '=', ISNULL(@__reservaMontoInicial, 0)) 
    AS Result;
END
