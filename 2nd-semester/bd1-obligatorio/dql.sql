-- Para todos los partidos del año corriente hacer una consulta que retorne 
-- código y nombre del equipo local, sus goles, 
-- código y nombre del equipo visitante, sus goles 
-- y la fecha del partido, debe utilizar alias para los campos
-- para respetar el siguiente resultado:
-- CodEquipoLocal | NomEquipoLocal | GolesLocal | CodEquipoVisita | NomEquipoVisita | GolesVisita | Fecha

SELECT 
    E1.codEquipo AS CodEquipoLocal,
    E1.nomEquipo AS NomEquipoLocal,
    P.GL AS GolesLocal,
    E2.codEquipo AS CodEquipoVisita,
    E2.nomEquipo AS NomEquipoVisita,
    P.GV AS GolesVisita,
    P.fecha AS Fecha
FROM
    Partido P
JOIN 
    Equipo E1 ON P.codEquipo_local = E1.codEquipo
JOIN
    Equipo E2 ON P.codEquipo_visita = E2.codEquipo
WHERE
    YEAR(P.fecha) = YEAR(GETDATE());


-- Para cada equipo en cuyo nombre aparece la palabra “FC”, 
-- mostrar su código, nombre, cantidad de partidos jugados de local, 
-- cantidad de goles marcados en dichos partidos y la fecha del último
-- partido de local, ordene los resultados por goles de mayor a menor, 
-- debe respetar el siguiente resultado:
-- codEquipo | nomEquipo | CantPartidosLocal | GolesLocal | FechaUltimoPartido
SELECT 
    E.codEquipo, 
    E.nomEquipo, 
    COUNT(P.codEquipo_local) AS CantPartidosLocal, 
    SUM(P.GL) AS GolesLocal, 
    MAX(P.fecha) AS FechaUltimoPartido
FROM 
    Equipo E
JOIN 
    Partido P ON P.codEquipo_local = E.codEquipo
WHERE 
    E.nomEquipo LIKE '%FC%'
GROUP BY 
    E.codEquipo, 
    E.nomEquipo
ORDER BY 
    GolesLocal DESC;

-- Mostrar la tabla de goleadores ordenada de mayor a menor, 
-- solo incluir aquellos jugadores que marcaron al menos
-- un gol, debe respetar el siguiente formato:
-- carnJug | nomJug | Goles
SELECT 
    D.carnJug, 
    J.nomJug, 
    SUM(D.cntGoles) AS Goles
FROM 
    Detalle D
JOIN 
    Jugador J ON D.carnJug = J.carnJug
GROUP BY 
    D.carnJug, 
    J.nomJug
HAVING 
    SUM(D.cntGoles) > 0
ORDER BY 
    SUM(D.cntGoles) DESC;

-- Para cada árbitro que arbitró partidos, mostrar su cédula, 
-- nombre, apellido, cantidad de tarjetas amarillas y cantidad
-- de tarjetas rojas sacadas, solo incluir los árbitros que 
-- sacaron por lo menos una tarjeta roja, debe respetar el
-- siguiente formato:
-- ciArbitro | nomArbitro | apelArbitro | CantidadAmarillas | CantidadRojas
SELECT 
    A.ciArbitro, 
    A.nomArbitro, 
    A.apelArbitro, 
    SUM(D.cntAmarillas) AS CantidadAmarillas, 
    SUM(D.cntRojas) AS CantidadRojas
FROM 
    Arbitro A
JOIN 
    Formulario F ON A.ciArbitro = F.ciArbitro
JOIN 
    Detalle D ON F.numForm = D.numForm
GROUP BY 
    A.ciArbitro, 
    A.nomArbitro, 
    A.apelArbitro
HAVING 
    SUM(D.cntRojas) > 0
ORDER BY 
    CantidadRojas DESC;

-- Mostrar cedula, nombre y apellido de los jugadores amateur
-- que juegan en equipos de camiseta que tienen color verde, y que
-- jugaron partidos los primeros 10 días de enero de 2023 en
-- canchas de mas de 1500 localidades. 
-- Filtrar resultados repetidos y ordenar por apellido del jugador.
-- La salida debe respetar el siguiente formato:
-- ciJug | nomJug | apeJug
SELECT DISTINCT
    J.carnJug, 
    J.nomJug, 
    J.apeJug
FROM 
    Jugador J
JOIN 
    Equipo E ON J.codEquipo = E.codEquipo
JOIN 
    Partido P ON J.codEquipo = P.codEquipo_local OR J.codEquipo = P.codEquipo_visita
JOIN 
    Cancha CH ON P.nomCancha = CH.nomCancha
WHERE 
    J.tipoJug = 'Amateur' AND
    E.colorEquipo = 'Verde' AND
    P.fecha BETWEEN '2023-01-01' AND '2023-01-10' AND
    CH.capCancha > 1500
ORDER BY 
    J.apeJug;

-- Para las canchas donde se jugaron entre 9 y 15 partidos, mostrar su nombre, 
-- la capacidad y la cantidad de partidos jugados, la consulta debe respetar el
-- siguiente formato:
-- nomCancha | capCancha | CantPartidos
SELECT 
    C.nomCancha, 
    C.capCancha, 
    COUNT(P.fecha) AS CantPartidos
FROM 
    Cancha C
JOIN 
    Partido P ON C.nomCancha = P.nomCancha
GROUP BY 
    C.nomCancha, 
    C.capCancha
HAVING 
    COUNT(P.fecha) BETWEEN 9 AND 15;

-- Mostrar los nombres de los equipos de región sur o de región norte que jugaron
-- mas de 2 partidos en canchas de más de 2000 espectadores 
-- (tener en cuenta cuando fue local y cuando fue visitante).
SELECT 
    E.nomEquipo, 
    COUNT(P.fecha) AS CantPartidos
FROM 
    Equipo E
JOIN 
    Partido P ON E.codEquipo = P.codEquipo_local OR E.codEquipo = P.codEquipo_visita
JOIN 
    Cancha C ON P.nomCancha = C.nomCancha
WHERE 
    (E.regionEquipo = 'Sur' OR E.regionEquipo = 'Norte') AND C.capCancha > 2000
GROUP BY 
    E.nomEquipo
HAVING 
    COUNT(P.fecha) > 2;

-- Mostrar todos los datos del árbitro que sacó mas tarjetas rojas, 
-- la consulta debe respetar el siguiente formato:
-- ciArbitro | nomArbitro | apelArbitro | celularArbitro | dirArbitro | mailArbitro | CantRojas
SELECT TOP 1
    A.ciArbitro,
    A.nomArbitro,
    A.apelArbitro,
    A.celularArbitro,
    A.dirArbitro,
    A.mailArbitro,
    SUM(D.cntRojas) AS CantRojas
FROM 
    Arbitro A
JOIN 
    Formulario F ON A.ciArbitro = F.ciArbitro
JOIN 
    Detalle D ON F.numForm = D.numForm
GROUP BY 
    A.ciArbitro,
    A.nomArbitro,
    A.apelArbitro,
    A.celularArbitro,
    A.dirArbitro,
    A.mailArbitro
ORDER BY 
    CantRojas DESC;

-- Agregar un campo “FundaLiga” de largo 1 carácter en la tabla Equipo, 
-- ponerle el valor “S” a todos los equipos que tienen mas de 50 años de fundados
ALTER TABLE Equipo
ADD FundaLiga CHAR(1);
UPDATE Equipo
SET FundaLiga = 'S'
WHERE YEAR(GETDATE()) - YEAR(fundacionEquipo) > 50;

-- Borrar todos los jugadores profesionales que no tienen fecha de vencimiento en su carnet de salud
DELETE FROM Jugador
WHERE tipoJug = 'Profesional' AND carnetVto IS NULL;