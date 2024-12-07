USE CatHotel;

-- a.
-- Mostrar el nombre del gato, el nombre del propietario,
-- la habitación
-- y el monto de la reserva más reciente en la
-- habitación con la capacidad más alta
SELECT TOP 1 G.gatoNombre, P.propietarioNombre, R.habitacionNombre, R.reservaMonto AS Monto
FROM
    Habitacion H
    JOIN Reserva R ON H.habitacionNombre = R.habitacionNombre
    JOIN Gato G ON R.gatoID = G.gatoID
    JOIN Propietario P ON G.propietarioDocumento = P.propietarioDocumento
WHERE
    H.habitacionCapacidad = (
        SELECT MAX(H2.habitacionCapacidad)
        FROM Habitacion H2
    )
ORDER BY R.reservaFechaInicio DESC;

-- b.
-- Mostrar los 3 servicios más solicitados, con su nombre, precio y cantidad total solicitada en
-- el año anterior. Solo listar el servicio si cumple que tiene una cantidad total solicitada mayor
-- o igual que 5
SELECT TOP 3 S.servicioNombre, S.servicioPrecio, SUM(RS.cantidad) AS CantidadTotal
FROM
    Servicio S
    JOIN Reserva_Servicio RS ON S.servicioNombre = RS.servicioNombre
    JOIN Reserva R ON RS.reservaID = R.reservaID
WHERE
    YEAR(R.reservaFechaInicio) = YEAR(GETDATE()) - 1
GROUP BY
    S.servicioNombre,
    S.servicioPrecio
HAVING
    SUM(RS.cantidad) >= 5
ORDER BY CantidadTotal DESC;

-- c.
-- Listar nombre de gato y nombre de habitación para las reservas que tienen asociados todos
-- los servicios adicionales disponibles
SELECT G.gatoNombre, H.habitacionNombre
FROM GATO G
INNER JOIN Reserva R ON G.gatoID = R.gatoID
INNER JOIN Habitacion H ON H.habitacionNombre = R.habitacionNombre
INNER JOIN Reserva_Servicio RS ON RS.reservaID = R.reservaID
INNER JOIN Servicio S ON S.servicioNombre = RS.servicioNombre
GROUP BY G.gatoNombre, H.habitacionNombre
HAVING COUNT(DISTINCT S.servicioNombre) = (
											SELECT COUNT(S.servicioNombre)
											FROM SERVICIO S
											);

-- d.
-- Listar monto total de reserva por año y por gato (nombre) para los gatos que tienen más de
-- 10 años de edad, son de raza "Persa" y que en el año tuvieron montos total de reserva
-- superior a 500 dólares.
SELECT YEAR(R.reservaFechaInicio) AS Anio, G.gatoNombre, SUM(R.reservaMonto) AS MontoTotal
FROM Gato G
    JOIN Reserva R ON G.gatoID = R.gatoID
WHERE
    G.gatoEdad > 10
    AND G.gatoRaza = 'Persa'
GROUP BY
    YEAR(R.reservaFechaInicio),
    G.gatoNombre
HAVING
    SUM(R.reservaMonto) > 500
ORDER BY Anio, MontoTotal DESC;

-- e.
-- Mostrar el ranking de reservas más caras, tomando como monto total de una reserva el monto
-- propio de la reserva más los servicios adicionales contratados en la reserva
SELECT TABLA_RESERVA_TOTAL.reservaID, TABLA_RESERVA_TOTAL.MONTO_TOTAL
FROM (
	   SELECT  R.reservaID, 
			   R.reservaMonto + (
								 CASE						
									WHEN(							
										 SELECT SUM(S.servicioPrecio * RS.cantidad)
										 FROM Reserva_Servicio RS
										 INNER JOIN Servicio S ON S.servicioNombre = RS.servicioNombre
										 WHERE RS.reservaID = R.reservaID 
										 ) IS NULL --EVITO QUE SI NO TIENE SERVICIOS ME DE NULL 
									THEN 0
									ELSE
										(							
										 SELECT SUM(S.servicioPrecio * RS.cantidad)
										 FROM Reserva_Servicio RS
										 INNER JOIN Servicio S ON S.servicioNombre = RS.servicioNombre
										 WHERE RS.reservaID = R.reservaID 
										 ) 
									END
								 ) AS MONTO_TOTAL
	   FROM Reserva R		
	 ) AS TABLA_RESERVA_TOTAL
ORDER BY TABLA_RESERVA_TOTAL.MONTO_TOTAL DESC

-- f.
-- Calcular el promedio de duración en días de las reservas realizadas durante el año en curso.
-- Deben ser consideradas solo aquellas reservas en las que se contrató el servicio
-- "CONTROL_PARASITOS" pero no se contrató el servicio "REVISION_VETERINARIA"
SELECT AVG(
        DATEDIFF(
            DAY, R.reservaFechaInicio, R.reservaFechaFin
        )
    ) AS PromedioDuracion
FROM
    Reserva R
    JOIN Reserva_Servicio RS ON R.reservaID = RS.reservaID
WHERE
    YEAR(R.reservaFechaInicio) = YEAR(GETDATE())
    AND RS.servicioNombre = 'CONTROL_PARASITOS'
    AND NOT EXISTS (
        SELECT *
        FROM Reserva_Servicio RS2
        WHERE
            RS2.reservaID = R.reservaID
            AND RS2.servicioNombre = 'REVISION_VETERINARIA'
    );

--g
--Para cada habitación, listar su nombre, la cantidad de días que ha estado ocupada y la
--cantidad de días transcurridos desde la fecha de inicio de la primera reserva en el hotel.
--Además, incluir una columna adicional que indique la categoría de rentabilidad, asignando
--el valor "REDITUABLE" si la habitación estuvo ocupada más del 60% de los días, "MAGRO"
--si estuvo ocupada entre el 40% y el 60%, y "NOESNEGOCIO" si estuvo ocupada menos del 40%.
SELECT HABITACION_R.habitacionNombre, 
       HABITACION_R.CANT_DIAS_DESDE_INICIO, 
	   HABITACION_R.CANT_DIAS_RESERVADOS,
	   (
	    CASE 
			WHEN HABITACION_R.CANT_DIAS_RESERVADOS * 100 / HABITACION_R.CANT_DIAS_DESDE_INICIO > 60 THEN 'REDITUABLE'
			WHEN HABITACION_R.CANT_DIAS_RESERVADOS * 100 / HABITACION_R.CANT_DIAS_DESDE_INICIO BETWEEN 40 AND 60 THEN 'MAGRO'
            ELSE 'NO ES NEGOCIO'
		END 
	   ) AS CATEGORIA_DE_RENTABILIDAD
FROM (
	  SELECT H.habitacionNombre, 
			 SUM(DATEDIFF(DAY,R.reservaFechaInicio, R.reservaFechaFin)) AS CANT_DIAS_RESERVADOS,
			 (DATEDIFF(DAY,(
                            SELECT TOP 1 reservaFechaInicio 
                            FROM Reserva Rv 
                            WHERE RV.habitacionNombre = H.habitacionNombre 
                            ORDER BY reservaFechaInicio ASC
                            ), GETDATE())
			 ) AS CANT_DIAS_DESDE_INICIO	 
	  FROM Habitacion H
	  INNER JOIN Reserva R ON H.habitacionNombre = R.habitacionNombre
	  GROUP BY H.habitacionNombre
	) AS HABITACION_R