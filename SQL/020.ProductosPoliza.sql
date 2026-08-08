BEGIN TRANSACTION;

IF OBJECT_ID('ProductosPoliza', 'U') IS NOT NULL
    DROP TABLE ProductosPoliza;

CREATE TABLE ProductosPoliza (
    Id INT IDENTITY(1,1) PRIMARY KEY, -- Genera 1, 2, 3... automáticamente
    IdProducto NVARCHAR(50) NULL,      -- Guarda el ID de tu producto si lo necesitas
    Nombre NVARCHAR(100) NULL,
    Cantidad NVARCHAR(20) NULL,
    Costo NVARCHAR(20) NULL,
    CostoExtra NVARCHAR(20) NULL,
    CostoReal NVARCHAR(20) NULL,
    IVA NVARCHAR(10) NULL,
    FolioPoliza NVARCHAR(50) NULL
);

COMMIT TRANSACTION;