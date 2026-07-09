BEGIN TRANSACTION;

-- Eliminar la tabla si ya existe
IF OBJECT_ID('Poliza2', 'U') IS NOT NULL
    DROP TABLE Poliza2;

-- Crear la tabla Poliza2
CREATE TABLE Poliza2 (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Folio NVARCHAR(MAX) NULL,          -- Texto largo en Access → NVARCHAR(MAX)
    Fecha DATETIME NULL,                -- Fecha/Hora en Access → DATETIME
    FechaCancelacion DATETIME NULL,     -- Fecha/Hora en Access → DATETIME
    CostoTotal NVARCHAR(50) NULL,       -- Texto corto en Access → NVARCHAR(50)
    CostoExtra NVARCHAR(50) NULL        -- Texto corto en Access → NVARCHAR(50)
);

COMMIT TRANSACTION;