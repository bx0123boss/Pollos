INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Jugo Naranja', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'JUGOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);
INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Jugo Toronja', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'JUGOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Jugo Zanahoria', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'JUGOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Jugo Combinado', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'JUGOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);
INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Jugo Fruta de Temporada', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'JUGOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Licuado Fresa', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'LICUADOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Licuado Manzana', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'LICUADOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Licuado Frutos Rojos', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'LICUADOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Licuado Platano', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'LICUADOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Licuado Mango', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'LICUADOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Licuado Avena', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'LICUADOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Licuado Combinado', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'LICUADOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Licuado Combinado', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'LICUADOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

--MALTEADAS

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Malteada Chocolate', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'MALTEADAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Malteada Vainilla', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'MALTEADAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Malteada Fresa', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'MALTEADAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Malteada Rompope', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'MALTEADAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

-- COCTEL DE FRUTAS


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Coctel de frutas Granola', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'COCTEL DE FRUTAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Coctel de frutas Arandano', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'COCTEL DE FRUTAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Coctel de frutas Almendra', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'COCTEL DE FRUTAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

--COCTEL DE FRUTAS c YOGURT

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Coctel de frutas Granola con YOGURT', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'COCTEL DE FRUTAS c YOGURT'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Coctel de frutas Arandano con YOGURT', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'COCTEL DE FRUTAS c YOGURT'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Coctel de frutas Almendra con YOGURT', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'COCTEL DE FRUTAS c YOGURT'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

--EXTRA JUGOS

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Ingrediente Extra p/Jugo Avena', -- Nombre
    1, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'EXTRA JUGOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Ingrediente Extra p/Jugo Chia', -- Nombre
    1, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'EXTRA JUGOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Ingrediente Extra p/Jugo Miel', -- Nombre
    1, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'EXTRA JUGOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Ingrediente Extra p/Jugo Jenjibre', -- Nombre
    1, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'EXTRA JUGOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Ingrediente Extra p/Jugo Linaza', -- Nombre
    1, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'EXTRA JUGOS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

--ENSALADAS


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Ensalada', -- Nombre
    65, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'ENSALADAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

--TORTAS

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Torta Milanesa', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'TORTAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Torta Pollo', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'TORTAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Torta Jamon', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'TORTAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

--SANDWICH

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Sandwich Pollo', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'SANDWICH'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Sandwich Atun', -- Nombre
    30, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'SANDWICH'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

--WAFLES

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Wafle Cajeta', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'WAFLES'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);


INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Wafle Lechera', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'WAFLES'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);
INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Wafle Mermelada', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'WAFLES'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Wafle Miel', -- Nombre
    35, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'WAFLES'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

--CHILAQUILES

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Chilaquiles Sencillos', -- Nombre
    55, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'CHILAQUILES'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Chilaquiles Especiales', -- Nombre
    65, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'CHILAQUILES'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

--ADEREZO P/ENSALADAS

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Aderezo Ranch', -- Nombre
    1, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'ADEREZO P/ENSALADAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Aderezo Mil Islas', -- Nombre
    1, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'ADEREZO P/ENSALADAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);

INSERT INTO INVENTARIO (
    Nombre,
    Precio,
    IdCategoria,
    CostoTotal,
    Comanda,
    IdSubCategoria
)
VALUES (
    'Aderezo Fresas', -- Nombre
    1, -- Precio
    (SELECT IdCategoria FROM CATEGORIAS WHERE Nombre = 'ADEREZO P/ENSALADAS'), -- IdCategoria
    0, -- CostoTotal
    1, -- Comanda
    1 -- IdSubCategoria
);