USE CatHotel;
GO

--Creo el Indice de la FK de propietario
CREATE INDEX IDX_GATO_PROPIETARIO ON Gato(propietarioDocumento)

--Creo el Indice de la FK de habitacionNombre
CREATE INDEX IDX_RESERVA_GATO ON Reserva(gatoID);

CREATE INDEX IDX_RESERVA_HABITACION ON Reserva(habitacionNombre);

GO