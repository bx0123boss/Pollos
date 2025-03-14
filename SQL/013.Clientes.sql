IF OBJECT_ID('Clientes', 'U') IS NOT NULL
    DROP TABLE Clientes;
	
CREATE TABLE Clientes (
    IdCliente INT IDENTITY(1,1) PRIMARY KEY,
	Nombre VARCHAR (300),
	Telefono numeric (15,0),
	Direccion VARCHAR (300),
	Referencia VARCHAR (300),
	Colonia VARCHAR (300)
);