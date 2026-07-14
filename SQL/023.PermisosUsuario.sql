BEGIN TRANSACTION;
IF OBJECT_ID('PermisosUsuario', 'U') IS NOT NULL
    DROP TABLE PermisosUsuario;


CREATE TABLE PermisosUsuario (
    id INT PRIMARY KEY IDENTITY(1,1),
    IdUsuario INT NOT NULL,
    Permiso VARCHAR(50) NOT NULL,
    UNIQUE (IdUsuario, Permiso)
);


COMMIT TRANSACTION;
