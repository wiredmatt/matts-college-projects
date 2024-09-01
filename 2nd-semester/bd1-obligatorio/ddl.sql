USE master;

DROP DATABASE IF EXISTS "REVISTA_mc";

-- CREAR Y MARCAR DB COMO DB ACTIVA
CREATE DATABASE "REVISTA_mc";

USE "REVISTA_mc";

-- CREAR TABLAS

CREATE TABLE Equipo (
    codEquipo CHAR(3) NOT NULL,
    nomEquipo VARCHAR(50),
    presEquipo VARCHAR(50) NOT NULL,
    fundacionEquipo DATE NOT NULL,
    regionEquipo VARCHAR(50),
    colorEquipo VARCHAR(50),


    CONSTRAINT PK_Equipo PRIMARY KEY (codEquipo),
    CONSTRAINT AK_Equipo UNIQUE (presEquipo),
    
    CONSTRAINT CK_Equipo_Fundacion CHECK (fundacionEquipo < GETDATE())
);

CREATE TABLE Jugador (
    carnJug CHAR(30) NOT NULL,
    ciJug CHAR(30) NOT NULL,
    nomJug VARCHAR(50) NOT NULL,
    apeJug VARCHAR(50) NOT NULL,
    nacJug DATE,
    telJug CHAR(30) NOT NULL,
    tipoJug VARCHAR(15) NOT NULL DEFAULT ('Amateur'),
    carnetNro CHAR(30),
    carnetVto DATE,
    codEquipo CHAR(3),


    CONSTRAINT PK_Jugador PRIMARY KEY (carnJug),
    CONSTRAINT AK_Jugador_CI UNIQUE (ciJug),
    CONSTRAINT AK_Jugador_Tel UNIQUE (telJug),
    
    CONSTRAINT FK_Jugador_Equipo FOREIGN KEY (codEquipo) REFERENCES Equipo (codEquipo),
    
    -- Verificar que el tipo de jugador sea 'Profesional' o 'Amateur'
    CONSTRAINT CK_Jugador_Tipo CHECK (tipoJug IN ('Profesional', 'Amateur'))
);

CREATE TABLE Cancha (
    nomCancha CHAR(50) NOT NULL,
    capCancha NUMERIC(10) NOT NULL,
    codEquipo CHAR(3),


CONSTRAINT PK_Cancha PRIMARY KEY (nomCancha),

    CONSTRAINT FK_Cancha_Equipo FOREIGN KEY (codEquipo) REFERENCES Equipo (codEquipo),
    
    CONSTRAINT CK_Cancha_Cap CHECK (capCancha >= 1000)
);

CREATE TABLE Arbitro (
    ciArbitro CHAR(30) NOT NULL,
    mailArbitro CHAR(50) NOT NULL,
    nomArbitro VARCHAR(50) NOT NULL,
    apelArbitro VARCHAR(50) NOT NULL,
    celularArbitro CHAR(30),
    dirArbitro VARCHAR(50),


    CONSTRAINT PK_Arbitro PRIMARY KEY (ciArbitro),
    CONSTRAINT AK_Arbitro UNIQUE (mailArbitro),
);

CREATE TABLE Formulario (
    numForm NUMERIC(10),
    fchForm DATETIME,
    ciArbitro CHAR(30),


    CONSTRAINT PK_Formulario PRIMARY KEY (numForm),
    
    CONSTRAINT FK_Formulario_Arbitro FOREIGN KEY (ciArbitro) REFERENCES Arbitro (ciArbitro)
);

CREATE TABLE Detalle (
    numForm NUMERIC(10) NOT NULL,
    linDet NUMERIC(10) NOT NULL,
    cntRojas NUMERIC(10) NOT NULL,
    cntAmarillas NUMERIC(10) NOT NULL,
    cntGoles NUMERIC(10) NOT NULL,
    carnJug CHAR(30) NOT NULL,


    CONSTRAINT PK_Detalle PRIMARY KEY (numForm, linDet),
    
    CONSTRAINT FK_Detalle_Formulario FOREIGN KEY (numForm) REFERENCES Formulario (numForm),
    CONSTRAINT FK_Detalle_Jugador FOREIGN KEY (carnJug) REFERENCES Jugador (carnJug)
);

CREATE TABLE Partido (
    codEquipo_local CHAR(3) NOT NULL,
    codEquipo_visita CHAR(3) NOT NULL,
    fecha DATE NOT NULL,
    GL NUMERIC(10) NOT NULL,
    GV NUMERIC(10) NOT NULL,
    ciArbitro CHAR(30),
    nomCancha CHAR(50),
    CONSTRAINT PK_Partido PRIMARY KEY (
        codEquipo_local,
        codEquipo_visita,
        fecha
    ),
    CONSTRAINT FK_Partido_Equipo_local FOREIGN KEY (codEquipo_local) REFERENCES Equipo (codEquipo),
    CONSTRAINT FK_Partido_Equipo_visita FOREIGN KEY (codEquipo_visita) REFERENCES Equipo (codEquipo),
    CONSTRAINT FK_Partido_Arbitro FOREIGN KEY (ciArbitro) REFERENCES Arbitro (ciArbitro),
    CONSTRAINT FK_Cancha FOREIGN KEY (nomCancha) REFERENCES Cancha (nomCancha)
);