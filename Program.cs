using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Data.SqlClient;
using control_de_ventas_1._0.Models;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURACIÓN DE SERVICIOS
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ControlVentasContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// CONFIGURACIÓN DE MIDDLEWARE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

app.MapControllerRoute(
    name: "controlventas_alt",
    pattern: "controlventas",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapGet("/", () => Results.Redirect("/controlventas/"));

// INICIALIZACIÓN DE BASE DE DATOS
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var dbContext = services.GetRequiredService<ControlVentasContext>();

    try
    {
        var assembly = Assembly.GetExecutingAssembly();
        logger.LogInformation("Iniciando proceso de inicialización de base de datos");

        // VERIFICAR Y CREAR LA BASE DE DATOS SI NO EXISTE
        var masterConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            .Replace("Database=VentasDB", "Database=master");

        bool databaseExists = false;

        using (var masterConnection = new SqlConnection(masterConnectionString))
        {
            masterConnection.Open();
            logger.LogInformation("Conectado a base de datos master para verificación");

            using (var checkCommand = new SqlCommand("SELECT 1 FROM sys.databases WHERE name = 'VentasDB'", masterConnection))
            {
                databaseExists = checkCommand.ExecuteScalar() != null;

                if (!databaseExists)
                {
                    logger.LogInformation("Creando base de datos 'VentasDB'");

                    var createDbResourceName = "control_de_ventas_1._0.Scripts.CrearBDVentas.sql";
                    var createDbScript = GetEmbeddedScript(assembly, createDbResourceName, logger);

                    using (var createDbCommand = new SqlCommand(createDbScript, masterConnection))
                    {
                        createDbCommand.ExecuteNonQuery();
                        logger.LogInformation("Base de datos creada exitosamente");
                    }
                }
                else
                {
                    logger.LogInformation("Base de datos 'VentasDB' ya existe");
                }
            }
        }

        var ventasConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        if (!databaseExists)
        {
            // BASE DE DATOS NUEVA - CREAR TODO DESDE CERO
            using (var ventasConnection = new SqlConnection(ventasConnectionString))
            {
                ventasConnection.Open();
                logger.LogInformation("Inicializando base de datos nueva");

                // CREAR ESQUEMA (TABLAS)
                logger.LogInformation("Creando esquema de base de datos");

                var createSchemaResourceName = "control_de_ventas_1._0.Scripts.CrearEsquema.sql";
                var createSchemaScript = GetEmbeddedScript(assembly, createSchemaResourceName, logger);

                using (var createSchemaCommand = new SqlCommand(createSchemaScript, ventasConnection))
                {
                    createSchemaCommand.CommandTimeout = 300;
                    createSchemaCommand.ExecuteNonQuery();
                    logger.LogInformation("Esquema creado exitosamente");
                }

                // INSERTAR DATOS DE PRUEBA
                logger.LogInformation("Insertando datos de prueba");

                var createTestDataResourceName = "control_de_ventas_1._0.Scripts.CrearDatosTest.sql";
                var createTestDataScript = GetEmbeddedScript(assembly, createTestDataResourceName, logger);

                using (var testDataCommand = new SqlCommand(createTestDataScript, ventasConnection))
                {
                    testDataCommand.CommandTimeout = 300;
                    testDataCommand.ExecuteNonQuery();
                    logger.LogInformation("Datos de prueba insertados exitosamente");
                }
            }
        }
        else
        {
            logger.LogInformation("Base de datos existente - no se requieren cambios");
        }

        logger.LogInformation("Proceso de inicialización de base de datos completado");

    }
    catch (SqlException sqlEx)
    {
        logger.LogError(sqlEx, "Error de SQL Server durante la inicialización. Código: {ErrorNumber}", sqlEx.Number);
        throw;
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error durante la inicialización de la base de datos");
        throw;
    }
}

// INICIAR LA APLICACIÓN
using (var scope = app.Services.CreateScope())
{
    var startupLogger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    startupLogger.LogInformation("Aplicación iniciada correctamente");
}

app.Run();

// MÉTODO AUXILIAR
static string GetEmbeddedScript(Assembly assembly, string resourceName, ILogger logger)
{
    using (var stream = assembly.GetManifestResourceStream(resourceName))
    {
        if (stream == null)
        {
            var errorMessage = $"No se encontró el recurso embebido: '{resourceName}'";
            logger.LogError(errorMessage);
            throw new FileNotFoundException(errorMessage, resourceName);
        }

        using (var reader = new StreamReader(stream))
        {
            return reader.ReadToEnd();
        }
    }
}