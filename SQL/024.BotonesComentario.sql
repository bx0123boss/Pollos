BEGIN TRANSACTION;
IF OBJECT_ID('BotonesComentario', 'U') IS NOT NULL
    DROP TABLE BotonesComentario;

CREATE TABLE BotonesComentario (
    IdBotonesComentario INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(30) NOT NULL
);
COMMIT TRANSACTION;
