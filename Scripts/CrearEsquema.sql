-- =============================================
-- Script 2: Crear tablas, relaciones y objetos en VentasDB
-- Este script se ejecuta en el contexto de 'VentasDB'
-- =============================================

-- Crear tabla Categoria si no existe
IF OBJECT_ID('Categoria', 'U') IS NULL
BEGIN
    CREATE TABLE Categoria (
        CodigoCategoria INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(100) NOT NULL
    );
END

-- Crear tabla Producto si no existe
IF OBJECT_ID('Producto', 'U') IS NULL
BEGIN
    CREATE TABLE Producto (
        CodigoProducto INT PRIMARY KEY IDENTITY(1,1),
        Nombre NVARCHAR(100) NOT NULL,
        CodigoCategoria INT NOT NULL,
        CONSTRAINT FK_Producto_Categoria FOREIGN KEY (CodigoCategoria) 
            REFERENCES Categoria(CodigoCategoria)
    );
END

-- Crear tabla Venta si no existe
IF OBJECT_ID('Venta', 'U') IS NULL
BEGIN
    CREATE TABLE Venta (
        CodigoVenta INT PRIMARY KEY IDENTITY(1,1),
        Fecha DATETIME NOT NULL DEFAULT GETDATE(),
        CodigoProducto INT NOT NULL,
        CONSTRAINT FK_Venta_Producto FOREIGN KEY (CodigoProducto) 
            REFERENCES Producto(CodigoProducto)
    );
END