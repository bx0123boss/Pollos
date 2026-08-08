BEGIN TRANSACTION;

-- Eliminar la tabla si ya existe
IF OBJECT_ID('ProductosPoliza2', 'U') IS NOT NULL
    DROP TABLE ProductosPoliza2;

-- Crear la tabla ProductosPoliza2
CREATE TABLE ProductosPoliza2 (
    Id INT IDENTITY(1,1) PRIMARY KEY,          -- Autonumeración → IDENTITY
    Nombre NVARCHAR(100) NULL,                 -- Texto corto → NVARCHAR
    Cantidad NVARCHAR(20) NULL,                -- Texto corto → NVARCHAR
    Costo NVARCHAR(20) NULL,                   -- Texto corto → NVARCHAR
    CostoExtra NVARCHAR(20) NULL,              -- Texto corto → NVARCHAR
    CostoReal NVARCHAR(20) NULL,               -- Texto corto → NVARCHAR
    PrecioVenta NVARCHAR(20) NULL,             -- Texto corto → NVARCHAR
    IdPoliza NVARCHAR(50) NULL                 -- Texto corto → NVARCHAR (relación con Poliza2)
);

COMMIT TRANSACTION;