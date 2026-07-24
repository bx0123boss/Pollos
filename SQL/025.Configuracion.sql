BEGIN TRANSACTION;
IF OBJECT_ID('Configuracion', 'U') IS NOT NULL
    DROP TABLE Configuracion;
	CREATE TABLE Configuracion (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Lugar VARCHAR(100) NULL,
    Font VARCHAR(100) NULL,
    Impresora VARCHAR(150) NULL,
    MaxChar INT DEFAULT 0,
    FontSize INT DEFAULT 10,
    MaxCharDescription INT DEFAULT 0,
    ConIva BIT DEFAULT 0,
    LogoPath VARCHAR(255) NULL,
    DatosTicket VARCHAR(MAX) NULL,
    PieDeTicket VARCHAR(MAX) NULL,
    MediaCarta BIT DEFAULT 0,
    Whatsapp VARCHAR(250) NULL,
    Bascula BIT DEFAULT 0,
    PuntoB BIT DEFAULT 0
);
COMMIT TRANSACTION;
