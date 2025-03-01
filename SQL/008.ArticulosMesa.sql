BEGIN TRANSACTION;
IF OBJECT_ID('ArticulosMesa', 'U') IS NOT NULL
    DROP TABLE ArticulosMesa;

CREATE TABLE ArticulosMesa (
    IdArticulosMesa INT PRIMARY KEY IDENTITY(1,1),
    IdInventario INT FOREIGN KEY REFERENCES Inventario(IdInventario),
    Cantidad NUMERIC(6,2),
    Total NUMERIC(8,2),
    Comentario VARCHAR(250),
    IdMesa INT FOREIGN KEY REFERENCES MESAS(IdMesa),
    IdMesero INT FOREIGN KEY REFERENCES USUARIOS(IdUsuario),
    FechaHora DATETIME,
    Estatus VARCHAR(15),
    IdUsuarioCancelo INT FOREIGN KEY REFERENCES USUARIOS(IdUsuario),
	Ids VARCHAR(50),
	IdPromo INT
);
COMMIT TRANSACTION;
