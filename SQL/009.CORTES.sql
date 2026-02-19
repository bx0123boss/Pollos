BEGIN TRANSACTION;

IF OBJECT_ID('HistorialCortes', 'U') IS NOT NULL
    DROP TABLE HistorialCortes;

CREATE TABLE HistorialCortes (
    IdHistorialCortes INT IDENTITY(1,1) PRIMARY KEY,
    Monto VARCHAR(150),
    FechaHora DATETIME
);

IF OBJECT_ID('Cortes', 'U') IS NOT NULL
    DROP TABLE Cortes;
CREATE TABLE Cortes (
    IdCortes INT IDENTITY(1,1) PRIMARY KEY,
    Concepto VARCHAR(150),
    Total NUMERIC(9,2),
    FormaPago VARCHAR(25),
	FechaHora DATETIME,
    IdHistorialCortes INT,
    FOREIGN KEY (IdHistorialCortes) REFERENCES HistorialCortes(IdHistorialCortes)
);

IF OBJECT_ID('Corte', 'U') IS NOT NULL
    DROP TABLE Corte;
CREATE TABLE Corte (
    IdCorte INT IDENTITY(1,1) PRIMARY KEY,
    Concepto VARCHAR(150),
    Total NUMERIC(9,2),
    FechaHora DATETIME,
    FormaPago VARCHAR(25)
);

IF OBJECT_ID('CortesMeseros', 'U') IS NOT NULL
    DROP TABLE CortesMeseros;
CREATE TABLE CortesMeseros (
    IdCortesMeseros INT IDENTITY(1,1) PRIMARY KEY,
	IdHistorialCortes INT,
    Mesero VARCHAR(30),
    Ventas NUMERIC(17,2),
    Mesas INT,
	FOREIGN KEY (IdHistorialCortes) REFERENCES HistorialCortes(IdHistorialCortes)
);

COMMIT TRANSACTION;