BEGIN TRANSACTION;

-- Eliminar la tabla si ya existe
IF OBJECT_ID('AbonosProveedores', 'U') IS NOT NULL
    DROP TABLE AbonosProveedores;

-- Crear la tabla AbonosProveedores
CREATE TABLE AbonosProveedores (
    Id INT IDENTITY(1,1) PRIMARY KEY,          -- Autonumeración → IDENTITY
    idProveedor INT NULL,                      -- Relación con Proveedores (Id)
    Fecha NVARCHAR(20) NULL,                   -- Texto corto → NVARCHAR
    Monto NVARCHAR(20) NULL,                   -- Texto corto → NVARCHAR
    AdeudoActual NVARCHAR(20) NULL             -- Texto corto → NVARCHAR
);

COMMIT TRANSACTION;