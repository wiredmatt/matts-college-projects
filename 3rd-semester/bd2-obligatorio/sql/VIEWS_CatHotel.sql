-- 6. Crear una vista que liste el monto total a facturar por propietario por las reservas y servicios del
-- mes pasado. Se debe listar el nombre del propietario, el monto total de sus reservas, el monto total
-- de servicios adicionales que contrató y la suma de ambos montos (monto a facturar)
USE CatHotel;
GO

CREATE VIEW FacturacionMesAnterior AS
SELECT P.propietarioNombre, 
       SUM(R.reservaMonto) AS MontoReservas,
       SUM(S.servicioPrecio * RS.cantidad) AS MontoServicios,
       SUM(R.reservaMonto) + SUM(S.servicioPrecio * RS.cantidad) AS MontoFacturar
FROM Propietario P
    JOIN Gato G ON P.propietarioDocumento = G.propietarioDocumento
    JOIN Reserva R ON G.gatoID = R.gatoID
    JOIN Reserva_Servicio RS ON R.reservaID = RS.reservaID
    JOIN Servicio S ON RS.servicioNombre = S.servicioNombre
WHERE
    MONTH(R.reservaFechaInicio) = MONTH(GETDATE()) - 1
    AND YEAR(R.reservaFechaInicio) = YEAR(GETDATE())
GROUP BY P.propietarioNombre;
GO
