using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using control_de_ventas_1._0.Models;
using System.ComponentModel.DataAnnotations;

namespace control_de_ventas_1._0.Controllers
{
    /// <summary>
    /// Controlador para consultas y operaciones relacionadas con ventas
    /// Proporciona endpoints para filtrar ventas por categoría y año
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class VentasController : ControllerBase
    {
        private readonly ControlVentasContext _context;
        private readonly ILogger<VentasController> _logger;

        public VentasController(ControlVentasContext context, ILogger<VentasController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene la lista de nombres de categorías que tuvieron ventas en el año especificado
        /// </summary>
        /// <param name="año">Año para filtrar las ventas (ejemplo: 2024)</param>
        /// <returns>Lista de nombres de categorías con ventas en el año especificado</returns>
        [HttpGet("categorias-con-ventas/{año:int}")]
        [ProducesResponseType(typeof(ApiResponse<List<string>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<List<string>>>> ObtenerCategoriasConVentas(
            [Range(1900, 3000, ErrorMessage = "El año debe estar entre 1900 y 3000")] int año)
        {
            try
            {
                _logger.LogInformation("Consultando categorías con ventas para el año: {Año}", año);

                // Verificar que el contexto esté disponible
                if (_context?.Categoria == null || _context?.Productos == null || _context?.Venta == null)
                {
                    _logger.LogError("El contexto de base de datos no está inicializado correctamente");
                    return StatusCode(500, new ApiErrorResponse
                    {
                        Success = false,
                        Mensaje = "Error de configuración de base de datos",
                        Error = "DbContext no inicializado"
                    });
                }

                // Consulta corregida - usar las propiedades correctas del DbContext
                var categoriasConVentas = await _context.Categoria
                    .Where(categoria => _context.Venta
                        .Any(venta => venta.CodigoProductoNavigation != null &&
                                     venta.CodigoProductoNavigation.CodigoCategoriaNavigation != null &&
                                     venta.CodigoProductoNavigation.CodigoCategoriaNavigation.CodigoCategoria == categoria.CodigoCategoria &&
                                     venta.Fecha.Year == año))
                    .Select(categoria => categoria.Nombre)
                    .Distinct()
                    .OrderBy(nombre => nombre)
                    .ToListAsync();

                if (!categoriasConVentas.Any())
                {
                    _logger.LogWarning("No se encontraron categorías con ventas para el año: {Año}", año);
                    return NotFound(new ApiErrorResponse
                    {
                        Success = false,
                        Mensaje = $"No se encontraron categorías con ventas en el año {año}",
                        Año = año,
                        Data = new List<string>()
                    });
                }

                _logger.LogInformation("Se encontraron {Cantidad} categorías con ventas para el año {Año}",
                    categoriasConVentas.Count, año);

                return Ok(new ApiResponse<List<string>>
                {
                    Success = true,
                    Data = categoriasConVentas,
                    Total = categoriasConVentas.Count,
                    Año = año
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener categorías con ventas para el año: {Año}", año);
                return StatusCode(500, new ApiErrorResponse
                {
                    Success = false,
                    Mensaje = "Error interno del servidor al procesar la solicitud",
                    Error = ex.Message,
                    Año = año
                });
            }
        }

        /// <summary>
        /// Obtiene las ventas filtradas por nombre de categoría y año específico
        /// </summary>
        /// <param name="nombreCategoria">Nombre de la categoría para filtrar</param>
        /// <param name="año">Año para filtrar las ventas</param>
        /// <returns>Lista de ventas con detalles de producto y categoría</returns>
        [HttpGet("por-categoria")]
        [ProducesResponseType(typeof(ApiResponse<List<VentaDetalleDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<List<VentaDetalleDto>>>> ObtenerVentasPorCategoria(
            [FromQuery, Required(ErrorMessage = "El nombre de la categoría es requerido")] string nombreCategoria,
            [FromQuery, Range(1900, 3000, ErrorMessage = "El año debe estar entre 1900 y 3000")] int año)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreCategoria))
                {
                    return BadRequest(new ApiErrorResponse
                    {
                        Success = false,
                        Mensaje = "El nombre de la categoría no puede estar vacío",
                        Error = "Parámetro nombreCategoria requerido"
                    });
                }

                _logger.LogInformation("Consultando ventas para categoría: {Categoria}, año: {Año}",
                    nombreCategoria, año);

                var ventasFiltradas = await _context.Venta
                    .Include(v => v.CodigoProductoNavigation)
                        .ThenInclude(p => p.CodigoCategoriaNavigation)
                    .Where(venta =>
                        venta.Fecha.Year == año &&
                        venta.CodigoProductoNavigation != null &&
                        venta.CodigoProductoNavigation.CodigoCategoriaNavigation != null &&
                        venta.CodigoProductoNavigation.CodigoCategoriaNavigation.Nombre == nombreCategoria)
                    .Select(venta => new VentaDetalleDto
                    {
                        CodigoVenta = venta.CodigoVenta,
                        Fecha = venta.Fecha,
                        CodigoProducto = venta.CodigoProducto,
                        NombreProducto = venta.CodigoProductoNavigation.Nombre,
                        CodigoCategoria = venta.CodigoProductoNavigation.CodigoCategoria,
                        NombreCategoria = venta.CodigoProductoNavigation.CodigoCategoriaNavigation.Nombre
                    })
                    .OrderByDescending(v => v.Fecha)
                    .ToListAsync();

                if (!ventasFiltradas.Any())
                {
                    _logger.LogWarning("No se encontraron ventas para categoría: {Categoria}, año: {Año}",
                        nombreCategoria, año);

                    return NotFound(new ApiErrorResponse
                    {
                        Success = false,
                        Mensaje = $"No se encontraron ventas para la categoría '{nombreCategoria}' en el año {año}",
                        Filtros = new { categoria = nombreCategoria, año = año },
                        Data = new List<VentaDetalleDto>()
                    });
                }

                _logger.LogInformation("Se encontraron {Cantidad} ventas para categoría: {Categoria}, año: {Año}",
                    ventasFiltradas.Count, nombreCategoria, año);

                return Ok(new ApiResponse<List<VentaDetalleDto>>
                {
                    Success = true,
                    Data = ventasFiltradas,
                    Total = ventasFiltradas.Count,
                    Filtros = new { categoria = nombreCategoria, año = año }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener ventas por categoría: {Categoria}, año: {Año}",
                    nombreCategoria, año);

                return StatusCode(500, new ApiErrorResponse
                {
                    Success = false,
                    Mensaje = "Error interno del servidor al procesar la solicitud",
                    Error = ex.Message
                });
            }
        }

        /// <summary>
        /// Obtiene un resumen estadístico de ventas por categoría en un año específico
        /// </summary>
        /// <param name="año">Año para generar el resumen</param>
        /// <returns>Resumen con totales de ventas por categoría</returns>
        [HttpGet("resumen-por-categoria/{año:int}")]
        [ProducesResponseType(typeof(ApiResponse<List<ResumenVentaCategoriaDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse<List<ResumenVentaCategoriaDto>>>> ObtenerResumenVentasPorCategoria(
            [Range(1900, 3000, ErrorMessage = "El año debe estar entre 1900 y 3000")] int año)
        {
            try
            {
                _logger.LogInformation("Generando resumen de ventas por categoría para el año: {Año}", año);

                var resumenVentas = await _context.Categoria
                    .Where(categoria => _context.Venta
                        .Any(venta => venta.CodigoProductoNavigation != null &&
                                     venta.CodigoProductoNavigation.CodigoCategoriaNavigation != null &&
                                     venta.CodigoProductoNavigation.CodigoCategoriaNavigation.CodigoCategoria == categoria.CodigoCategoria &&
                                     venta.Fecha.Year == año))
                    .Select(categoria => new ResumenVentaCategoriaDto
                    {
                        CodigoCategoria = categoria.CodigoCategoria,
                        NombreCategoria = categoria.Nombre,
                        TotalVentas = _context.Venta.Count(v =>
                            v.CodigoProductoNavigation != null &&
                            v.CodigoProductoNavigation.CodigoCategoriaNavigation != null &&
                            v.CodigoProductoNavigation.CodigoCategoriaNavigation.CodigoCategoria == categoria.CodigoCategoria &&
                            v.Fecha.Year == año),
                        CantidadProductos = _context.Productos.Count(p =>
                            p.CodigoCategoria == categoria.CodigoCategoria &&
                            _context.Venta.Any(v => v.CodigoProducto == p.CodigoProducto && v.Fecha.Year == año)),
                        PrimeraVenta = _context.Venta
                            .Where(v => v.CodigoProductoNavigation != null &&
                                       v.CodigoProductoNavigation.CodigoCategoriaNavigation != null &&
                                       v.CodigoProductoNavigation.CodigoCategoriaNavigation.CodigoCategoria == categoria.CodigoCategoria &&
                                       v.Fecha.Year == año)
                            .Min(v => v.Fecha),
                        UltimaVenta = _context.Venta
                            .Where(v => v.CodigoProductoNavigation != null &&
                                       v.CodigoProductoNavigation.CodigoCategoriaNavigation != null &&
                                       v.CodigoProductoNavigation.CodigoCategoriaNavigation.CodigoCategoria == categoria.CodigoCategoria &&
                                       v.Fecha.Year == año)
                            .Max(v => v.Fecha)
                    })
                    .OrderByDescending(r => r.TotalVentas)
                    .ToListAsync();

                if (!resumenVentas.Any())
                {
                    return NotFound(new ApiErrorResponse
                    {
                        Success = false,
                        Mensaje = $"No se encontraron ventas en el año {año}",
                        Año = año,
                        Data = new List<ResumenVentaCategoriaDto>()
                    });
                }

                _logger.LogInformation("Resumen generado para {Cantidad} categorías en el año {Año}",
                    resumenVentas.Count, año);

                return Ok(new ApiResponse<List<ResumenVentaCategoriaDto>>
                {
                    Success = true,
                    Data = resumenVentas,
                    Total = resumenVentas.Count,
                    Año = año,
                    Resumen = new
                    {
                        totalCategorias = resumenVentas.Count,
                        totalVentasGlobal = resumenVentas.Sum(r => r.TotalVentas)
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al generar resumen de ventas para el año: {Año}", año);
                return StatusCode(500, new ApiErrorResponse
                {
                    Success = false,
                    Mensaje = "Error interno del servidor al procesar la solicitud",
                    Error = ex.Message
                });
            }
        }
    }

    // =============================================================================
    // DTOS Y RESPONSE MODELS
    // =============================================================================

    public class VentaDetalleDto
    {
        public int CodigoVenta { get; set; }
        public DateTime Fecha { get; set; }
        public int CodigoProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int CodigoCategoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
    }

    public class ResumenVentaCategoriaDto
    {
        public int CodigoCategoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
        public int TotalVentas { get; set; }
        public int CantidadProductos { get; set; }
        public DateTime PrimeraVenta { get; set; }
        public DateTime UltimaVenta { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public int Total { get; set; }
        public int? Año { get; set; }
        public object? Filtros { get; set; }
        public object? Resumen { get; set; }
    }

    public class ApiErrorResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public string? Error { get; set; }
        public int? Año { get; set; }
        public object? Filtros { get; set; }
        public object? Data { get; set; }
    }
}