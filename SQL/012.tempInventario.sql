IF OBJECT_ID('tempInventario', 'U') IS NOT NULL
    DROP TABLE tempInventario;
	--(id,cantidad, producto, precio, total,ide)
CREATE TABLE tempInventario (
	IdTempInventario INT IDENTITY(1,1) PRIMARY KEY,
    id VARCHAR (30),
	cantidad numeric (17,5),
	ide VARCHAR (30)
);
