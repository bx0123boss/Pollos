IF OBJECT_ID('Promos', 'U') IS NOT NULL
    DROP TABLE Promos;
	
CREATE TABLE Promos (
    IdPromo INT IDENTITY(1,1) PRIMARY KEY,
    CodigoPromo AS 'C' + CAST(IdPromo AS VARCHAR(10)),
	Nombre VARCHAR (30),
	Precio numeric (17,5),
	Lunes BIT,
	Martes BIT,
	Miercoles BIT,
	Jueves BIT,
	Viernes BIT,
	Sabado BIT,
	Domingo BIT,
);

IF OBJECT_ID('ArticulosPromo', 'U') IS NOT NULL
    DROP TABLE ArticulosPromo;
CREATE TABLE ArticulosPromo (
	IdArticulosPromo INT IDENTITY(1,1) PRIMARY KEY,
	IdPromo INT,
	IdSubcategoria INT,
	Cantidad NUMERIC(6,2),
	);