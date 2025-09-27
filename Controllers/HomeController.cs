using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using control_de_ventas_1._0.Models;

namespace control_de_ventas_1._0.Controllers
{
    /// <summary>
    /// Controlador principal para la aplicación de Control de Ventas
    /// Maneja las páginas principales y la navegación del sistema
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ControlVentasContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ControlVentasContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Página principal del sistema de Control de Ventas
        /// Ruta: /controlventas/
        /// </summary>
        /// <returns>Vista principal con resumen del sistema</returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                _logger.LogInformation("Cargando página principal del sistema de Control de Ventas");

                // Obtener estadísticas básicas para mostrar en el dashboard
                var estadisticas = new
                {
                    TotalCategorias = await _context.Categoria.CountAsync(),
                    TotalProductos = await _context.Productos.CountAsync(),
                    TotalVentas = await _context.Venta.CountAsync(),
                    VentasHoy = await _context.Venta
                        .Where(v => v.Fecha.Date == DateTime.Today)
                        .CountAsync(),
                    UltimaVenta = await _context.Venta
                        .OrderByDescending(v => v.Fecha)
                        .FirstOrDefaultAsync()
                };

                // Obtener categorías para el selector
                var categorias = await _context.Categoria
                    .Select(c => c.Nombre)
                    .OrderBy(n => n)
                    .ToListAsync();

                ViewBag.Estadisticas = estadisticas;
                ViewBag.Categorias = categorias;
                ViewBag.AñoActual = DateTime.Now.Year;

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la página principal");
                ViewBag.Error = "Error al cargar los datos del sistema";
                return View();
            }
        }

        /// <summary>
        /// Página de consultas avanzadas
        /// Ruta: /controlventas/consultas
        /// </summary>
        public IActionResult Consultas()
        {
            _logger.LogInformation("Cargando página de consultas avanzadas");
            return View();
        }

        /// <summary>
        /// Página de configuración del sistema
        /// Ruta: /controlventas/configuracion
        /// </summary>
        public IActionResult Configuracion()
        {
            _logger.LogInformation("Cargando página de configuración");
            return View();
        }

        /// <summary>
        /// Página de ayuda y documentación
        /// Ruta: /controlventas/ayuda
        /// </summary>
        public IActionResult Ayuda()
        {
            _logger.LogInformation("Cargando página de ayuda");
            return View();
        }

        /// <summary>
        /// Manejo de errores generales
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}