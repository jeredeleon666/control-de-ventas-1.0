-- =============================================
-- Script 3: Insertar datos de ejemplo en VentasDB
-- Este script se ejecuta en el contexto de 'VentasDB'
-- Inserta categorias, productos y ventas desde 2015 hasta 2025
-- =============================================
-- Insertar categorias
INSERT INTO Categoria (Nombre) VALUES 
(N'Electronicos'),
(N'Ropa'),
(N'Alimentos'),
(N'Hogar'),
(N'Juguetes'),
(N'Libros'),
(N'Deportes'),
(N'Cosmeticos'),
(N'Automotriz'),
(N'Jardineria');

-- Insertar productos (asignados a categorias 1-10)
INSERT INTO Producto (Nombre, CodigoCategoria) VALUES 
-- Categoria Electronicos (1)
(N'Telefono Inteligente Galaxy S10', 1),
(N'Tablet iPad Air', 1),
(N'Auriculares Bluetooth Sony', 1),
(N'Camara Digital Canon', 1),
(N'Laptop Dell XPS', 1),
-- Categoria Ropa (2)
(N'Camisa Casual Hombre', 2),
(N'Jeans Mujer Levi''s', 2),
(N'Zapatillas Nike Air', 2),
(N'Vestido Verano', 2),
(N'Chaqueta Invierno', 2),
-- Categoria Alimentos (3)
(N'Arroz Integral 5kg', 3),
(N'Leche Deslactosada', 3),
(N'Pan Integral', 3),
(N'Fruta Manzana', 3),
(N'Carne Res', 3),
-- Categoria Hogar (4)
(N'Sofa Moderno', 4),
(N'Microondas LG', 4),
(N'Sabanas Algodon', 4),
(N'Lampara LED', 4),
(N'Juego de Ollas', 4),
-- Categoria Juguetes (5)
(N'Muñeca Barbie', 5),
(N'Coche a Control Remoto', 5),
(N'Juego de Mesa Monopoly', 5),
(N'Pelota Futbol', 5),
(N'Construccion Lego', 5),
-- Categoria Libros (6)
(N'1984 de George Orwell', 6),
(N'El Alquimista', 6),
(N'Harry Potter Vol 1', 6),
(N'Cocina para Principiantes', 6),
(N'Historia del Mundo', 6),
-- Categoria Deportes (7)
(N'Raqueta Tenis Wilson', 7),
(N'Bicicleta MTB', 7),
(N'Guantes Boxeo', 7),
(N'Balon Baloncesto', 7),
(N'Botas Futbol', 7),
-- Categoria Cosmeticos (8)
(N'Crema Hidratante', 8),
(N'Perfume Chanel', 8),
(N'Maquinilla Rasurar', 8),
(N'Labial Rojo', 8),
(N'Shampoo Anti-Caspa', 8),
-- Categoria Automotriz (9)
(N'Aceite Motor 5W30', 9),
(N'Bateria Auto', 9),
(N'Neumatico Michelin', 9),
(N'Cadenas Nieve', 9),
(N'Limpia Vidrios', 9),
-- Categoria Jardineria (10)
(N'Semillas Tomate', 10),
(N'Tijeras Poda', 10),
(N'Maceta Ceramica', 10),
(N'Abono Organico', 10),
(N'Regalo Manguera', 10);

-- Insertar ventas: 10 por año desde 2015 hasta 2025

-- Año 2015
INSERT INTO Venta (Fecha, CodigoProducto) VALUES 
('2015-01-15 10:30:00', 1),
('2015-03-22 14:20:00', 6),
('2015-05-10 09:15:00', 11),
('2015-07-18 16:45:00', 16),
('2015-09-05 11:00:00', 21),
('2015-11-12 13:30:00', 26),
('2015-02-28 08:50:00', 31),
('2015-04-14 15:10:00', 36),
('2015-06-20 12:25:00', 41),
('2015-08-07 17:40:00', 46);

-- Año 2016
INSERT INTO Venta (Fecha, CodigoProducto) VALUES 
('2016-01-20 11:15:00', 2),
('2016-03-15 13:45:00', 7),
('2016-05-25 10:00:00', 12),
('2016-07-30 16:20:00', 17),
('2016-09-12 09:35:00', 22),
('2016-11-08 14:50:00', 27),
('2016-02-10 12:10:00', 32),
('2016-04-18 17:25:00', 37),
('2016-06-05 11:40:00', 42),
('2016-08-22 15:55:00', 47);

-- Año 2017
INSERT INTO Venta (Fecha, CodigoProducto) VALUES 
('2017-01-05 09:20:00', 3),
('2017-03-28 12:35:00', 8),
('2017-05-15 16:10:00', 13),
('2017-07-20 10:50:00', 18),
('2017-09-30 14:15:00', 23),
('2017-11-25 18:30:00', 28),
('2017-02-12 13:00:00', 33),
('2017-04-08 11:25:00', 38),
('2017-06-18 15:40:00', 43),
('2017-08-14 17:05:00', 48);

-- Año 2018
INSERT INTO Venta (Fecha, CodigoProducto) VALUES 
('2018-01-10 14:00:00', 4),
('2018-03-05 10:45:00', 9),
('2018-05-20 13:20:00', 14),
('2018-07-15 17:35:00', 19),
('2018-09-25 12:10:00', 24),
('2018-11-10 16:25:00', 29),
('2018-02-18 15:50:00', 34),
('2018-04-22 09:15:00', 39),
('2018-06-10 11:30:00', 44),
('2018-08-28 14:45:00', 49);

-- Año 2019
INSERT INTO Venta (Fecha, CodigoProducto) VALUES 
('2019-01-25 16:20:00', 5),
('2019-03-12 11:55:00', 10),
('2019-05-08 15:10:00', 15),
('2019-07-22 12:40:00', 20),
('2019-09-18 17:05:00', 25),
('2019-11-15 10:30:00', 30),
('2019-02-05 14:15:00', 35),
('2019-04-30 18:50:00', 40),
('2019-06-25 13:25:00', 45),
('2019-08-20 16:40:00', 50);

-- Año 2020
INSERT INTO Venta (Fecha, CodigoProducto) VALUES 
('2020-01-15 12:35:00', 1),
('2020-03-10 09:50:00', 6),
('2020-05-05 14:20:00', 11),
('2020-07-12 11:10:00', 16),
('2020-09-08 16:45:00', 21),
('2020-11-20 13:25:00', 26),
('2020-02-28 10:05:00', 31),
('2020-04-15 17:40:00', 36),
('2020-06-22 15:15:00', 41),
('2020-08-18 12:30:00', 46);

-- Año 2021
INSERT INTO Venta (Fecha, CodigoProducto) VALUES 
('2021-01-20 17:50:00', 2),
('2021-03-25 14:35:00', 7),
('2021-05-12 11:20:00', 12),
('2021-07-30 16:55:00', 17),
('2021-09-15 13:40:00', 22),
('2021-11-10 10:25:00', 27),
('2021-02-10 15:10:00', 32),
('2021-04-05 12:45:00', 37),
('2021-06-20 18:20:00', 42),
('2021-08-25 14:00:00', 47);

-- Año 2022
INSERT INTO Venta (Fecha, CodigoProducto) VALUES 
('2022-01-08 13:15:00', 3),
('2022-03-18 16:30:00', 8),
('2022-05-22 12:05:00', 13),
('2022-07-15 17:50:00', 18),
('2022-09-28 11:35:00', 23),
('2022-11-12 15:10:00', 28),
('2022-02-15 18:25:00', 33),
('2022-04-10 14:50:00', 38),
('2022-06-05 10:40:00', 43),
('2022-08-20 13:55:00', 48);

-- Año 2023
INSERT INTO Venta (Fecha, CodigoProducto) VALUES 
('2023-01-30 11:20:00', 4),
('2023-03-08 15:45:00', 9),
('2023-05-15 17:10:00', 14),
('2023-07-20 12:30:00', 19),
('2023-09-12 16:05:00', 24),
('2023-11-18 13:40:00', 29),
('2023-02-22 10:15:00', 34),
('2023-04-28 14:35:00', 39),
('2023-06-15 18:50:00', 44),
('2023-08-10 11:25:00', 49);

-- Año 2024
INSERT INTO Venta (Fecha, CodigoProducto) VALUES 
('2024-01-12 14:40:00', 5),
('2024-03-20 10:55:00', 10),
('2024-05-10 13:20:00', 15),
('2024-07-25 17:45:00', 20),
('2024-09-05 12:10:00', 25),
('2024-11-15 16:30:00', 30),
('2024-02-18 15:25:00', 35),
('2024-04-12 11:50:00', 40),
('2024-06-28 14:05:00', 45),
('2024-08-22 18:20:00', 50);

-- Año 2025 (hasta septiembre)
INSERT INTO Venta (Fecha, CodigoProducto) VALUES 
('2025-01-15 16:15:00', 1),
('2025-03-10 12:40:00', 6),
('2025-05-05 15:55:00', 11),
('2025-07-12 11:30:00', 16),
('2025-09-01 14:10:00', 21),
('2025-01-28 10:20:00', 26),
('2025-04-15 17:45:00', 31),
('2025-06-20 13:15:00', 36),
('2025-08-08 16:30:00', 41),
('2025-09-20 09:50:00', 46);