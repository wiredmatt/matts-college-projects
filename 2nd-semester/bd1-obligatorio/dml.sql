USE "REVISTA_mc";

DELETE FROM Detalle;

DELETE FROM Partido;

DELETE FROM Cancha;

DELETE FROM Jugador;

DELETE FROM Equipo;

INSERT INTO
    Equipo (
        codEquipo,
        nomEquipo,
        presEquipo,
        fundacionEquipo,
        regionEquipo,
        colorEquipo
    )
VALUES (
        '001',
        'Real Madrid',
        'Florentino Pérez',
        '1902-03-06',
        'Sur',
        'Verde'
    );

INSERT INTO
    Equipo (
        codEquipo,
        nomEquipo,
        presEquipo,
        fundacionEquipo,
        regionEquipo,
        colorEquipo
    )
VALUES (
        '002',
        'FC Barcelona',
        'Joan Laporta',
        '1899-11-29',
        'Sur',
        'Verde'
    );

INSERT INTO
    Equipo (
        codEquipo,
        nomEquipo,
        presEquipo,
        fundacionEquipo,
        regionEquipo,
        colorEquipo
    )
VALUES (
        '003',
        'FC Manchester United',
        'Joel Glazer',
        '1878-12-01',
        'Norte',
        'Verde'
    );

INSERT INTO
    Equipo (
        codEquipo,
        nomEquipo,
        presEquipo,
        fundacionEquipo,
        regionEquipo,
        colorEquipo
    )
VALUES (
        '004',
        'Bayern de Múnich',
        'Herbert Hainer',
        '1900-02-27',
        'Este',
        'Verde'
    );

INSERT INTO
    Equipo (
        codEquipo,
        nomEquipo,
        presEquipo,
        fundacionEquipo,
        regionEquipo,
        colorEquipo
    )
VALUES (
        '005',
        'Juventus',
        'Andrea Agnelli',
        '1897-11-01',
        'Oeste',
        'Negro y Blanco'
    );

INSERT INTO
    Jugador (
        carnJug,
        ciJug,
        nomJug,
        apeJug,
        nacJug,
        telJug,
        tipoJug,
        codEquipo
    )
VALUES (
        '0001',
        '123456789',
        'Lionel',
        'Messi',
        '1987-06-24',
        '123456',
        'Amateur',
        '002'
    );

INSERT INTO
    Jugador (
        carnJug,
        ciJug,
        nomJug,
        apeJug,
        nacJug,
        telJug,
        tipoJug,
        codEquipo
    )
VALUES (
        '1000',
        '12345',
        'Martin',
        'Messi',
        '2015-04-21',
        '1234567',
        'Amateur',
        '002'
    );

INSERT INTO
    Jugador (
        carnJug,
        ciJug,
        nomJug,
        apeJug,
        nacJug,
        telJug,
        tipoJug,
        codEquipo
    )
VALUES (
        '2000',
        '777777',
        'Soy ',
        'Yo',
        '1985-02-10',
        '777777',
        'Amateur',
        '001'
    );

INSERT INTO
    Jugador (
        carnJug,
        ciJug,
        nomJug,
        apeJug,
        nacJug,
        telJug,
        tipoJug,
        codEquipo
    )
VALUES (
        '0002',
        '987654321',
        'Cristiano',
        'Ronaldo',
        '1985-02-05',
        '654321',
        'Amateur',
        '001'
    );

INSERT INTO
    Jugador (
        carnJug,
        ciJug,
        nomJug,
        apeJug,
        nacJug,
        telJug,
        tipoJug,
        codEquipo,
        carnetNro,
        carnetVto
    )
VALUES (
        '0003',
        '111111111',
        'Neymar',
        'Jr.',
        '1992-02-05',
        '111111',
        'Profesional',
        '002',
        '123',
        '2025-10-10'
    );

INSERT INTO
    Jugador (
        carnJug,
        ciJug,
        nomJug,
        apeJug,
        nacJug,
        telJug,
        tipoJug,
        codEquipo,
        carnetNro,
        carnetVto
    )
VALUES (
        '0004',
        '222222222',
        'Sergio',
        'Ramos',
        '1986-03-30',
        '222222',
        'Profesional',
        '001',
        '1233',
        '2025-10-10'
    );

INSERT INTO
    Jugador (
        carnJug,
        ciJug,
        nomJug,
        apeJug,
        nacJug,
        telJug,
        tipoJug,
        codEquipo,
        carnetNro,
        carnetVto
    )
VALUES (
        '0005',
        '333333333',
        'Robert',
        'Lewandowski',
        '1988-08-21',
        '333333',
        'Profesional',
        '004',
        '12345',
        '2025-10-10'
    );

INSERT INTO
    Jugador (
        carnJug,
        ciJug,
        nomJug,
        apeJug,
        nacJug,
        telJug,
        tipoJug,
        codEquipo,
        carnetNro
    )
VALUES (
        '0006',
        '444444444',
        'Pro',
        'SinVto',
        '1982-09-11',
        '444444',
        'Profesional',
        '004',
        '123456'
    );

INSERT INTO
    Jugador (
        carnJug,
        ciJug,
        nomJug,
        apeJug,
        nacJug,
        telJug,
        tipoJug,
        codEquipo,
        carnetNro
    )
VALUES (
        '0007',
        '666333',
        'Pro',
        'SinVtoOtro',
        '1982-02-14',
        '919191',
        'Profesional',
        '005',
        '545321'
    );

INSERT INTO
    Cancha (
        nomCancha,
        capCancha,
        codEquipo
    )
VALUES (
        'Santiago Bernabéu',
        81044,
        '001'
    );

INSERT INTO
    Cancha (
        nomCancha,
        capCancha,
        codEquipo
    )
VALUES ('Camp Nou', 99354, '002');

INSERT INTO
    Cancha (
        nomCancha,
        capCancha,
        codEquipo
    )
VALUES ('Old Trafford', 74879, '003');

INSERT INTO
    Cancha (
        nomCancha,
        capCancha,
        codEquipo
    )
VALUES ('Allianz Arena', 75000, '004');

INSERT INTO
    Cancha (
        nomCancha,
        capCancha,
        codEquipo
    )
VALUES (
        'Allianz Stadium',
        41507,
        '005'
    );

INSERT INTO
    Arbitro (
        ciArbitro,
        mailArbitro,
        nomArbitro,
        apelArbitro,
        celularArbitro,
        dirArbitro
    )
VALUES (
        '111111',
        'referee1@example.com',
        'Juan',
        'Pérez',
        '123456',
        'Calle Principal 123'
    );

INSERT INTO
    Arbitro (
        ciArbitro,
        mailArbitro,
        nomArbitro,
        apelArbitro,
        celularArbitro,
        dirArbitro
    )
VALUES (
        '222222',
        'referee2@example.com',
        'María',
        'González',
        '654321',
        'Avenida Central 456'
    );

INSERT INTO
    Arbitro (
        ciArbitro,
        mailArbitro,
        nomArbitro,
        apelArbitro,
        celularArbitro,
        dirArbitro
    )
VALUES (
        '333333',
        'referee3@example.com',
        'José',
        'Martínez',
        NULL,
        'Calle Secundaria 789'
    );

INSERT INTO
    Arbitro (
        ciArbitro,
        mailArbitro,
        nomArbitro,
        apelArbitro,
        celularArbitro,
        dirArbitro
    )
VALUES (
        '444444',
        'referee4@example.com',
        'Ana',
        'Rodríguez',
        '789012',
        'Avenida Norte 012'
    );

INSERT INTO
    Arbitro (
        ciArbitro,
        mailArbitro,
        nomArbitro,
        apelArbitro,
        celularArbitro,
        dirArbitro
    )
VALUES (
        '555555',
        'referee5@example.com',
        'Carlos',
        'López',
        '345678',
        'Calle Este 345'
    );

INSERT INTO
    Formulario (numForm, fchForm, ciArbitro)
VALUES (
        1,
        '2024-05-20 15:30:00',
        '111111'
    );

INSERT INTO
    Formulario (numForm, fchForm, ciArbitro)
VALUES (
        2,
        '2024-05-21 16:00:00',
        '222222'
    );

INSERT INTO
    Formulario (numForm, fchForm, ciArbitro)
VALUES (
        3,
        '2024-05-22 17:30:00',
        '333333'
    );

INSERT INTO
    Formulario (numForm, fchForm, ciArbitro)
VALUES (
        4,
        '2024-05-23 18:00:00',
        '444444'
    );

INSERT INTO
    Formulario (numForm, fchForm, ciArbitro)
VALUES (
        5,
        '2024-05-24 19:30:00',
        '555555'
    );

INSERT INTO
    Detalle (
        numForm,
        linDet,
        cntRojas,
        cntAmarillas,
        cntGoles,
        carnJug
    )
VALUES (1, 1, 0, 1, 2, '0001');

INSERT INTO
    Detalle (
        numForm,
        linDet,
        cntRojas,
        cntAmarillas,
        cntGoles,
        carnJug
    )
VALUES (1, 2, 1, 2, 1, '0002');

INSERT INTO
    Detalle (
        numForm,
        linDet,
        cntRojas,
        cntAmarillas,
        cntGoles,
        carnJug
    )
VALUES (2, 1, 0, 0, 1, '0003');

INSERT INTO
    Detalle (
        numForm,
        linDet,
        cntRojas,
        cntAmarillas,
        cntGoles,
        carnJug
    )
VALUES (2, 2, 1, 1, 0, '0004');

INSERT INTO
    Detalle (
        numForm,
        linDet,
        cntRojas,
        cntAmarillas,
        cntGoles,
        carnJug
    )
VALUES (3, 1, 0, 0, 0, '0005');

INSERT INTO
    Detalle (
        numForm,
        linDet,
        cntRojas,
        cntAmarillas,
        cntGoles,
        carnJug
    )
VALUES (5, 1, 8, 0, 0, '0005');

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '001',
        '002',
        '2023-01-02',
        2,
        1,
        '111111',
        'Camp Nou'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '003',
        '004',
        '2023-01-06',
        3,
        2,
        '111111',
        'Camp Nou'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '001',
        '002',
        '2023-01-09',
        2,
        1,
        '111111',
        'Camp Nou'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '003',
        '004',
        '2023-01-07',
        1,
        2,
        '222222',
        'Old Trafford'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '005',
        '001',
        '2023-01-05',
        0,
        0,
        '333333',
        'Allianz Stadium'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '002',
        '003',
        '2023-01-04',
        3,
        1,
        '444444',
        'Santiago Bernabéu'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '004',
        '005',
        '2023-01-01',
        2,
        2,
        '555555',
        'Allianz Arena'
    );

-- Llenar Camp Nou para que salga en la consulta de 9 - 15
INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '005',
        '003',
        '2023-01-01',
        1,
        1,
        '555555',
        'Camp Nou'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '002',
        '003',
        '2023-01-01',
        1,
        3,
        '555555',
        'Camp Nou'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '001',
        '003',
        '2023-01-01',
        2,
        1,
        '555555',
        'Camp Nou'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '001',
        '004',
        '2023-01-01',
        1,
        4,
        '555555',
        'Camp Nou'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '002',
        '004',
        '2023-01-01',
        3,
        2,
        '555555',
        'Camp Nou'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '002',
        '005',
        '2023-01-01',
        4,
        1,
        '555555',
        'Camp Nou'
    );

-- Partidos para consulta del año actual
INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '004',
        '005',
        '2024-10-10',
        2,
        2,
        '555555',
        'Allianz Arena'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '001',
        '002',
        '2024-10-20',
        3,
        1,
        '555555',
        'Allianz Arena'
    );

INSERT INTO
    Partido (
        codEquipo_local,
        codEquipo_visita,
        fecha,
        GL,
        GV,
        ciArbitro,
        nomCancha
    )
VALUES (
        '003',
        '005',
        '2024-10-24',
        5,
        3,
        '555555',
        'Allianz Arena'
    );