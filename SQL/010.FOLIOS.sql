CREATE TABLE Folios (
    IdFolio INT IDENTITY(1,1) PRIMARY KEY,
    ModalidadVenta VARCHAR(50),  -- Agregué tamaño para evitar errores
    Estatus VARCHAR(15),
    IdCliente INT,  -- Se asume FK a una tabla de Clientes
    FechaHora DATETIME,
    Total NUMERIC(8,2),
    Utilidad NUMERIC(8,2),
    --FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente) -- Ajustar según tu estructura
);

CREATE TABLE ArticulosFolio (
    IdArticulosFolio INT IDENTITY(1,1) PRIMARY KEY,
    IdProducto INT,  -- Se asume FK a una tabla de Productos
    IdFolio INT,
    Cantidad NUMERIC(6,2),
    Comentario VARCHAR(250),
    Total NUMERIC(8,2),
    IdExtra VARCHAR(25),
    FOREIGN KEY (IdFolio) REFERENCES Folios(IdFolio),
    FOREIGN KEY (IdProducto) REFERENCES Productos(IdProducto) -- Ajustar según tu estructura
);
