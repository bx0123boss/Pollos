IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'VentasPagos')
BEGIN
    CREATE TABLE VentasPagos (
        IdVentaPago INT IDENTITY(1,1) PRIMARY KEY,
        IdFolio INT NOT NULL,
        MetodoPago VARCHAR(100) NOT NULL,
        Monto DECIMAL(18,2) NOT NULL,
        FechaHora DATETIME DEFAULT GETDATE()
    );
END;
GO