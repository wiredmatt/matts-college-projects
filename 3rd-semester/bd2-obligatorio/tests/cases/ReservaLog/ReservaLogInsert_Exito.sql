USE CatHotel;
GO

-- Al correr la precarga, ya se ingresan varios registros a esta tabla
DECLARE @CantidadLogsInicial INT;

SELECT @CantidadLogsInicial = COUNT(*) FROM ReservaLog;

IF @CantidadLogsInicial > 0
BEGIN
    SELECT CONCAT('ESPERADO: ', @CantidadLogsInicial) AS Result;
END
ELSE
BEGIN
    SELECT CONCAT('NO_ESPERADO: ', @CantidadLogsInicial) AS Result;
END
GO