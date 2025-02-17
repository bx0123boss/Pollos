IF OBJECT_ID('Folios', 'U') IS NOT NULL
    DROP TABLE Folios;

CREATE TABLE Folios (
    IdFolio INT IDENTITY(1,1) PRIMARY KEY,
    ModalidadVenta VARCHAR(50), 
    Estatus VARCHAR(15),
    IdCliente INT,  -- Se asume FK a una tabla de Clientes
    FechaHora DATETIME,
    Total NUMERIC(17,2),
    Descuento NUMERIC(17,2),
	Utilidad NUMERIC(17,2),
	IdMesa INT,
	FOREIGN KEY (IdMesa) REFERENCES MESAS(IdMesa)
    --FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente) 
);
IF OBJECT_ID('ArticulosFolio', 'U') IS NOT NULL
    DROP TABLE ArticulosFolio;

CREATE TABLE ArticulosFolio (
    IdArticulosFolio INT IDENTITY(1,1) PRIMARY KEY,
    IdInventario INT,  
    IdFolio INT,
    Cantidad NUMERIC(6,2),
    Comentario VARCHAR(250),
    Total NUMERIC(8,2),
    IdExtra VARCHAR(25),
    FOREIGN KEY (IdFolio) REFERENCES Folios(IdFolio),
    FOREIGN KEY (IdInventario) REFERENCES Inventario(IdInventario) 
);
