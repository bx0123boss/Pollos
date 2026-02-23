-- 1. Tabla para la paleta de colores
CREATE TABLE ConfiguracionApariencia (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ColorPrimario NVARCHAR(20) DEFAULT '#0d6efd', -- Azul Bootstrap por defecto
    ColorSecundario NVARCHAR(20) DEFAULT '#6c757d', -- Gris Bootstrap por defecto
    UltimaModificacion DATETIME DEFAULT GETDATE()
);


INSERT INTO ConfiguracionApariencia (ColorPrimario, ColorSecundario) 
VALUES ('#720e1e', '#FFD700'); 
