-- =============================================
-- Consulta solicitada:
-- Obtener el nombre de la categoría y la fecha de la última venta realizada según la fecha
-- =============================================

SELECT TOP 1 c.Nombre AS NombreCategoria, v.Fecha AS FechaVenta
FROM Venta v
JOIN Producto p ON v.CodigoProducto = p.CodigoProducto
JOIN Categoria c ON p.CodigoCategoria = c.CodigoCategoria
ORDER BY v.Fecha DESC;
