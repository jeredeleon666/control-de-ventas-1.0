-- =============================================
-- Consulta solicitada:
-- Obtener el nombre de la categoría del producto de la última venta realizada según la fecha
-- =============================================

SELECT TOP 1 c.Nombre AS NombreCategoria
FROM Venta v
JOIN Producto p ON v.CodigoProducto = p.CodigoProducto
JOIN Categoria c ON p.CodigoCategoria = c.CodigoCategoria
ORDER BY v.Fecha DESC;