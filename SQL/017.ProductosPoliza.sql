BEGIN TRANSACTION;

-- Verificar y eliminar la tabla si ya existe
IF OBJECT_ID('dbo.ProductosPoliza', 'U') IS NOT NULL
    DROP TABLE dbo.ProductosPoliza;

-- Crear la tabla con la estructura correspondiente
CREATE TABLE dbo.ProductosPoliza (
    Id INT IDENTITY(1,1) PRIMARY KEY,       -- O VARCHAR(50) si el Id contiene letras o guiones
    Nombre VARCHAR(150) NULL,               -- Texto corto
    Cantidad DECIMAL(18, 2) NULL,           -- Texto corto en imagen -> Convertido a Número/Decimal (o INT si siempre son enteros)
    Costo DECIMAL(18, 2) NULL,              -- Texto corto en imagen -> Convertido a Decimal/Moneda
    CostoExtra DECIMAL(18, 2) NULL,         -- Texto corto en imagen -> Convertido a Decimal/Moneda
    CostoReal DECIMAL(18, 2) NULL,          -- Texto corto en imagen -> Convertido a Decimal/Moneda
    IVA DECIMAL(18, 2) NULL,                -- Texto corto en imagen -> Convertido a Decimal/Moneda
    FolioPoliza VARCHAR(50) NULL            -- Texto corto
);

COMMIT TRANSACTION;