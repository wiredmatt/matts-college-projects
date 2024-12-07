USE CatHotel;

--4)_A

--CONSUMIR STP:

--Caso 1:
--Devuelve 0 por que la habitacion no exite

DECLARE @reservaID INT; -- Variable para capturar el resultado

EXEC SP_ReservarHabitacion
    @gatoID = 101, 
    @habitacionNombre = 'Habitacion 100', -- Nombre de la habitación no existe en la tabla Habitacion
    @reservaFechaInicio = '2024-12-01', 
    @reservaFechaFin = '2024-12-05',
    @reservaMonto = 200.00, 
    @reservaID = @reservaID OUTPUT; 

PRINT @reservaID

--Caso 2:
--Devuelve 0 por que la habitacion 10 su estado es Limpiando

DECLARE @reservaID INT; -- Variable para capturar el resultado

EXEC SP_ReservarHabitacion
    @gatoID = 5, 
    @habitacionNombre = 'Habitacion10', 
    @reservaFechaInicio = '2024-12-01', 
    @reservaFechaFin = '2024-12-05',
    @reservaMonto = 200.00, 
    @reservaID = @reservaID OUTPUT; 

PRINT @reservaID

--Caso 3: 
--Devuelve 1 por que la habitacion 11 tiene capacidad 1 y cambia su estado a LLeno

DECLARE @reservaID INT; -- Variable para capturar el resultado

EXEC SP_ReservarHabitacion
    @gatoID = 3, 
    @habitacionNombre = 'Habitacion11', 
    @reservaFechaInicio = '2024-12-01', 
    @reservaFechaFin = '2024-12-05',
    @reservaMonto = 200.00, 
    @reservaID = @reservaID OUTPUT; 

PRINT @reservaID

--Caso 4: 
--Devuelve 0 por que la habitacion 11 tiene su estado es LLeno

DECLARE @reservaID INT; -- Variable para capturar el resultado

EXEC SP_ReservarHabitacion
    @gatoID = 3, 
    @habitacionNombre = 'Habitacion11', 
    @reservaFechaInicio = '2024-12-01', 
    @reservaFechaFin = '2024-12-05',
    @reservaMonto = 200.00, 
    @reservaID = @reservaID OUTPUT; 

PRINT @reservaID

select *
from Habitacion

select *
from Reserva

--(OK)
--------------------------------------------------------------------------------------------------------------------------------------

--4_B

--CONSUMIR FUNSION:

--Consulto
  SELECT SUM(RS.cantidad) as este_anio
    FROM Reserva_Servicio RS
	INNER JOIN Reserva R ON RS.reservaID = R.reservaID
    WHERE RS.servicioNombre = 'REVISION_VETERINARIA'
    AND YEAR(R.reservaFechaInicio) = YEAR(GETDATE());

 SELECT SUM(RS.cantidad)as anio_pasado
    FROM Reserva_Servicio RS 
	INNER JOIN Reserva R ON RS.reservaID = R.reservaID
    WHERE RS.servicioNombre = 'REVISION_VETERINARIA'
    AND YEAR(R.reservaFechaInicio) = YEAR(GETDATE())-1;

--Caso 1:
--REVISION_VETERINARIA --> tiene 4 el anio pasado y 2 este, devuelve 0
SELECT dbo.F_SERVICIO_CONTRATADO_MAS_VECES_ANIO('REVISION_VETERINARIA')
AS EL_SERVICIO_TUBO_MAS_CANTIDAD_ESTE_ANIO_QUE_EL_ANTERIOR;

--Consulto
  SELECT SUM(RS.cantidad) as este_anio
    FROM Reserva_Servicio RS
	INNER JOIN Reserva R ON RS.reservaID = R.reservaID
    WHERE RS.servicioNombre = 'PELUQUERIA'
    AND YEAR(R.reservaFechaInicio) = YEAR(GETDATE());

 SELECT SUM(RS.cantidad)as anio_pasado
    FROM Reserva_Servicio RS 
	INNER JOIN Reserva R ON RS.reservaID = R.reservaID
    WHERE RS.servicioNombre = 'PELUQUERIA'
    AND YEAR(R.reservaFechaInicio) = YEAR(GETDATE())-1;


--Caso 2:
--PELUQUERIA --> tiene este anio 5 y el anio pasado 3, devuelve 1
SELECT dbo.F_SERVICIO_CONTRATADO_MAS_VECES_ANIO('PELUQUERIA')
AS EL_SERVICIO_TUBO_MAS_CANTIDAD_ESTE_ANIO_QUE_EL_ANTERIOR;


--(ok)
------------------------------------------------------------------------------------------------------------------------------------------

--5_A 

-- realizo varos insert a la vez
INSERT INTO Reserva VALUES  
							(2,'Habitacion5', '2024-11-05', '2024-11-15', 300.00),
							(3,'Habitacion6', '2024-10-25', '2024-11-06', 400.00),
							(4,'Habitacion7', '2024-11-25', '2024-12-09', 500.00);

-- Veo la tabla ReservaLog
select *
from Reserva

-- Modifico el monto de varias resrevas a la vez
UPDATE Reserva
SET reservaMonto = reservaMonto + 100
WHERE reservaID IN (14,15,16);

-- Veo la tabla ReservaLog por fechaHoraRegistro
select *
from ReservaLog
order by fechaHoraRegistro desc


--(OK)
------------------------------------------------------------------------------------------------------------------------------------------

--5_B

-- realizo insert en reservas
INSERT INTO Reserva VALUES  
							(1,'Habitacion2', '2023-11-05', '2023-11-15', 500.00),-- no pasa por fecha
							(1,'Habitacion3', '2023-10-25', '2023-11-06', 700.00),-- no pasa por fecha
							(1,'Habitacion4', '2023-11-25', '2023-12-09', 600.00);-- lo ingresa OK

Select *
From Reserva

--(OK)
--------------------------------------------------------------------------------------------------------------------------------------------

--6)_ Vista

--Consumirlo:

Select *
from FacturacionMesAnterior


--(OK)