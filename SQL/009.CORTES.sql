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

COMMIT TRANSACTION;