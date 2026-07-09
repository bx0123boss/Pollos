BEGIN TRANSACTION;

IF OBJECT_ID('dbo.Poliza', 'U') IS NOT NULL
    DROP TABLE dbo.Poliza;

CREATE TABLE dbo.NombreTabla (
    Id INT IDENTITY(1,1) PRIMARY KEY,       -- Autonumeración (llave primaria)
    Folio VARCHAR(MAX) NULL,                -- Texto largo
    Fecha DATETIME NULL,                    -- Fecha/Hora
    FechaCaptura DATETIME NULL,             -- Fecha/Hora
    CostoTotal DECIMAL(18, 2) NULL,         -- Texto corto en imagen -> Convertido a Moneda/Decimal
    CostoExtra DECIMAL(18, 2) NULL,         -- Texto corto en imagen -> Convertido a Moneda/Decimal
    IdProv VARCHAR(50) NULL,                -- Texto corto
    Proveedor VARCHAR(150) NULL,            -- Texto corto
    IVA DECIMAL(18, 2) NULL,                -- Texto corto en imagen -> Convertido a Moneda/Decimal
    Total DECIMAL(18, 2) NULL,              -- Texto corto en imagen -> Convertido a Moneda/Decimal
    IdAlmacen INT NULL                      -- Número
);

COMMIT TRANSACTION;