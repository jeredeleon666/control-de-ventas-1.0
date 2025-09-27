# control de ventas 1.0
<img width="1655" height="865" alt="Captura de pantalla 2025-09-26 a la(s) 22 57 06" src="https://github.com/user-attachments/assets/75a2c350-7e39-42e6-9c7d-5ce21d33eb82" />
<img width="1680" height="1050" alt="Captura de pantalla 2025-09-21 a la(s) 11 39 41" src="https://github.com/user-attachments/assets/3044a9e6-d13a-4aab-84f4-efc0a664aa6c" />
Okay, aquí tienes la documentación actualizada en HTML, quitando la sección "Código Fuente Principal (Program.cs)" y añadiendo la sugerencia de descarga/clonado en la sección de instalación.

```html
<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Documentación - Sistema de Control de Ventas</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">
    <style>
        /* Opcional: Ajustar el scroll para anclajes suaves */
        html { scroll-behavior: smooth; }
    </style>
</head>
<body class="bg-gray-100 text-gray-800 font-sans">
    <!-- Navbar -->
    <nav class="bg-gradient-to-r from-blue-800 to-indigo-900 text-white sticky top-0 z-10 shadow-lg">
        <div class="container mx-auto px-4 py-3">
            <div class="flex justify-between items-center">
                <a href="#top" class="text-xl font-bold flex items-center">
                    <i class="fas fa-chart-line mr-2"></i>Sistema de Control de Ventas
                </a>
                <div class="hidden md:flex space-x-6">
                    <a href="#requisitos" class="hover:text-blue-300 transition"><i class="fas fa-cogs mr-1"></i>Requisitos</a>
                    <a href="#arquitectura-db" class="hover:text-blue-300 transition"><i class="fas fa-database mr-1"></i>BD</a>
                    <a href="#tecnologias" class="hover:text-blue-300 transition"><i class="fas fa-microchip mr-1"></i>Tecnologías</a>
                    <a href="#endpoints" class="hover:text-blue-300 transition"><i class="fas fa-plug mr-1"></i>API</a>
                </div>
            </div>
        </div>
    </nav>

    <!-- Hero Section -->
    <header class="bg-gradient-to-r from-blue-600 to-indigo-700 text-white py-16">
        <div class="container mx-auto px-4 text-center">
            <h1 class="text-4xl md:text-5xl font-bold mb-4 flex justify-center items-center">
                <i class="fas fa-file-alt mr-3"></i>Documentación Técnica
            </h1>
            <p class="text-lg md:text-xl max-w-3xl mx-auto">Sistema web empresarial desarrollado en ASP.NET Core 8.0 para la gestión integral y consulta analítica de ventas segmentadas por categorías de productos.</p>
        </div>
    </header>

    <!-- Main Content -->
    <main class="container mx-auto px-4 py-8">
        <!-- Table of Contents -->
        <section id="indice" class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-list mr-2 text-blue-600"></i>Tabla de Contenidos</h2>
            <ul class="list-disc pl-5 space-y-2">
                <li><a href="#requisitos" class="text-blue-600 hover:underline">Requisitos del Sistema</a></li>
                <li><a href="#arquitectura-db" class="text-blue-600 hover:underline">Arquitectura de la Base de Datos</a></li>
                <li><a href="#analisis-diseno" class="text-blue-600 hover:underline">Análisis Técnico y Decisiones de Diseño</a></li>
                <li><a href="#tecnologias" class="text-blue-600 hover:underline">Tecnologías Utilizadas</a></li>
                <li><a href="#instalacion" class="text-blue-600 hover:underline">Instalación y Configuración</a></li>
                <li><a href="#funcionalidades" class="text-blue-600 hover:underline">Funcionalidades</a></li>
                <li><a href="#endpoints" class="text-blue-600 hover:underline">API Endpoints</a></li>
                <li><a href="#scripts-db" class="text-blue-600 hover:underline">Scripts de Base de Datos</a></li>
                <li><a href="#decisiones-impl" class="text-blue-600 hover:underline">Decisiones de Implementación</a></li>
            </ul>
        </section>

        <!-- Requisitos del Sistema -->
        <section id="requisitos" class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-cogs mr-2 text-blue-600"></i>Requisitos del Sistema</h2>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Prerrequisitos Obligatorios</h3>
            <ul class="list-disc pl-5 mb-4 space-y-1">
                <li><strong>SQL Server LocalDB</strong> (incluido con Visual Studio o .NET SDK)
                    <ul class="list-disc pl-5">
                        <li>Instancia: <code class="bg-gray-100 px-1 rounded">(localdb)\MSSQLLocalDB</code></li>
                        <li><strong>No requiere configuración de puerto específico</strong></li>
                        <li>Conexión mediante Named Pipes (local)</li>
                        <li><em>Nota</em>: El sistema también es compatible con otras ediciones de SQL Server</li>
                    </ul>
                </li>
                <li><strong>.NET 8 SDK</strong> o superior</li>
                <li><strong>Puerto 5281</strong> disponible para la aplicación web (configuración específica del proyecto)</li>
            </ul>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Verificación de Prerequisitos</h3>
            <pre class="bg-gray-800 text-green-400 p-4 rounded-lg overflow-x-auto text-sm"><code># Verificar SQL Server LocalDB
sqllocaldb info MSSQLLocalDB
sqllocaldb start MSSQLLocalDB

# Verificar conectividad a LocalDB
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -Q "SELECT @@VERSION"

# Verificar .NET 8
dotnet --version

# Verificar puerto especifico del proyecto
netstat -an | findstr :5281</code></pre>
        </section>

        <!-- Arquitectura de la Base de Datos -->
        <section id="arquitectura-db" class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-database mr-2 text-blue-600"></i>Arquitectura de la Base de Datos</h2>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Modelo Relacional Implementado</h3>
            <p class="mb-4">El sistema implementa un esquema relacional normalizado de tres entidades principales con integridad referencial completa:</p>
            <div class="flex justify-center mb-4">
                <pre class="text-sm bg-gray-100 p-4 rounded-lg overflow-x-auto">
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│    Categoria    │    │    Producto     │    │     Venta       │
├─────────────────┤    ├─────────────────┤    ├─────────────────┤
│ CodigoCategoria │◄──┐│ CodigoProducto  │◄──┐│ CodigoVenta     │
│ Nombre          │   └│ Nombre          │   └│ Fecha           │
└─────────────────┘    │ CodigoCategoria │    │ CodigoProducto  │
                       └─────────────────┘    └─────────────────┘
                </pre>
            </div>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Especificaciones Técnicas del Esquema</h3>
            <ul class="list-disc pl-5 space-y-1">
                <li><strong>Relaciones Implementadas:</strong>
                    <ul class="list-disc pl-5">
                        <li><code>Categoria (1:N) Producto</code>: Una categoría puede contener múltiples productos</li>
                        <li><code>Producto (1:N) Venta</code>: Un producto puede tener múltiples registros de venta</li>
                        <li><strong>Integridad Referencial</strong>: Constraints de clave foránea con CASCADE en consultas</li>
                    </ul>
                </li>
                <li><strong>Índices y Constraints:</strong>
                    <ul class="list-disc pl-5">
                        <li>Primary Keys con <code>IDENTITY(1,1)</code> para auto-incremento</li>
                        <li>Foreign Key Constraints con nombres descriptivos</li>
                        <li>Campos NOT NULL en atributos críticos</li>
                        <li>Valores por defecto (<code>GETDATE()</code>) para auditoría temporal</li>
                    </ul>
                </li>
            </ul>
        </section>

        <!-- Análisis Técnico y Decisiones de Diseño -->
        <section id="analisis-diseno" class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-project-diagram mr-2 text-blue-600"></i>Analisis Tecnico y Decisiones de Diseno</h2>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Analisis del Modelo de Datos Normalizado</h3>
            <p class="mb-4">La arquitectura de datos implementa un modelo relacional de tercer forma normal (3NF) con las siguientes características:</p>
            <ul class="list-disc pl-5 space-y-1">
                <li><strong>Entidades del Dominio:</strong>
                    <ul class="list-disc pl-5">
                        <li><strong>Categoria</strong>: Entidad maestro que funciona como taxonomía de clasificación</li>
                        <li><strong>Producto</strong>: Entidad de catálogo con referencia categórica</li>
                        <li><strong>Venta</strong>: Entidad transaccional con timestamp automático</li>
                    </ul>
                </li>
                <li><strong>Relaciones de Cardinalidad:</strong>
                    <pre class="text-sm inline bg-gray-100 px-2 py-1 rounded">Categoria ||-----o{ Producto ||-----o{ Venta</pre>
                </li>
            </ul>
            <h3 class="text-xl font-semibold mb-2 text-gray-700 mt-4">Decisiones Arquitectonicas Implementadas</h3>
            <ul class="list-disc pl-5 space-y-2">
                <li><strong>Estrategia de Persistencia de Datos</strong>: La implementación mantiene <strong>absoluta fidelidad</strong> al esquema de referencia proporcionado, aplicando principios de:
                    <ul class="list-disc pl-5">
                        <li><strong>Code First Database Approach</strong>: Utilización de Entity Framework Core con scaffold reverso</li>
                        <li><strong>Modelo de Dominio Puro</strong>: Sin modificaciones a la estructura base de tablas</li>
                        <li><strong>Integridad Referencial</strong>: Implementación de constraints FK completas</li>
                    </ul>
                </li>
                <li><strong>Generacion Automatica de Contexto ORM</strong>: Implementación de scaffold para generación automática de:
                    <ul class="list-disc pl-5">
                        <li><code>ControlVentasContext.cs</code>: DbContext principal</li>
                        <li><code>Categorium.cs</code>: Entidad de categoria (pluralizacion automatica de EF)</li>
                        <li><code>Producto.cs</code>: Entidad de producto</li>
                        <li><code>Ventum.cs</code>: Entidad de venta (pluralizacion automatica de EF)</li>
                    </ul>
                </li>
                <li><strong>Arquitectura de Base de Datos Autodesplegable</strong>: Implementación de una estrategia de <strong>Infrastructure as Code</strong> para la inicialización automática del entorno de datos.
                    <details class="mt-2">
                        <summary class="cursor-pointer text-blue-600 font-medium">Ver detalles</summary>
                        <p class="mt-2"><strong>Problematica de Despliegue Identificada:</strong> La distribucion de bases de datos preconfiguradas presenta los siguientes vectores de riesgo tecnico:</p>
                        <ul class="list-disc pl-5">
                            <li>Incompatibilidades de version entre instancias de SQL Server</li>
                            <li>Complejidad de configuracion de connection strings personalizados</li>
                            <li>Dependencias de permisos de usuario en sistemas de evaluacion</li>
                            <li>Overhead operacional para inicializacion de entornos de prueba</li>
                        </ul>
                        <p class="mt-2"><strong>Solucion de Ingenieria Implementada:</strong> Desarrollo de un sistema de <strong>auto-bootstrap</strong> que se conecta a <code>(localdb)\MSSQLLocalDB</code> y ejecuta de manera condicional:</p>
                        <ol class="list-decimal pl-5">
                            <li>Verificacion de existencia de la base de datos <code>VentasDB</code></li>
                            <li>Ejecucion automatica de scripts DDL para creacion de esquema</li>
                            <li>Poblacion inicial con datasets de prueba representativos</li>
                            <li>Capacidad de regeneracion completa ante eliminacion accidental</li>
                        </ol>
                    </details>
                </li>
            </ul>
        </section>

        <!-- Tecnologias Utilizadas -->
        <section id="tecnologias" class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-microchip mr-2 text-blue-600"></i>Tecnologias Utilizadas</h2>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Stack Tecnologico Backend</h3>
            <ul class="list-disc pl-5 mb-4 space-y-1">
                <li><strong>Framework</strong>: ASP.NET Core 8.0 (LTS)</li>
                <li><strong>ORM</strong>: Entity Framework Core 8.0.x</li>
                <li><strong>Base de Datos</strong>: Microsoft SQL Server (compatible con versiones 2019+)</li>
                <li><strong>Patron Arquitectonico</strong>: Model-View-Controller (MVC) con API REST</li>
                <li><strong>Inyeccion de Dependencias</strong>: Built-in DI Container de .NET Core</li>
            </ul>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Stack Tecnologico Frontend</h3>
            <ul class="list-disc pl-5 mb-4 space-y-1">
                <li><strong>Lenguajes</strong>: HTML5, CSS3, JavaScript (ES6+)</li>
                <li><strong>Framework CSS</strong>: Tailwind CSS 3.x (CDN)</li>
                <li><strong>Iconografia</strong>: Font Awesome 6.4.0</li>
                <li><strong>Arquitectura</strong>: SPA components con Vanilla JavaScript</li>
                <li><strong>Responsive Design</strong>: Mobile-first approach</li>
            </ul>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Herramientas de Desarrollo</h3>
            <ul class="list-disc pl-5 space-y-1">
                <li><strong>IDE Recomendado</strong>: Visual Studio 2022 / Visual Studio Code</li>
                <li><strong>Package Manager</strong>: NuGet Package Manager</li>
                <li><strong>Control de Versiones</strong>: Git</li>
                <li><strong>Scaffolding</strong>: Entity Framework Core Tools</li>
            </ul>
        </section>

        <!-- Instalacion y Configuracion -->
        <section id="instalacion" class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-download mr-2 text-blue-600"></i>Instalacion y Configuracion</h2>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Descargar o Clonar el Repositorio</h3>
            <p class="mb-4">Se recomienda descargar o clonar el repositorio para obtener todos los archivos del proyecto de forma completa.</p>
            <pre class="bg-gray-800 text-green-400 p-4 rounded-lg overflow-x-auto text-sm mb-4"><code># Opción 1: Clonar el repositorio
git clone https://github.com/usuario/sistema-control-ventas.git
cd sistema-control-ventas

# Opción 2: Descargar como archivo ZIP (desde GitHub)
# Luego extraer el archivo ZIP en una carpeta local.</code></pre>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Restaurar Dependencias</h3>
            <pre class="bg-gray-800 text-green-400 p-4 rounded-lg overflow-x-auto text-sm mb-4"><code>dotnet restore</code></pre>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Ejecutar la Aplicacion</h3>
            <pre class="bg-gray-800 text-green-400 p-4 rounded-lg overflow-x-auto text-sm mb-4"><code>dotnet run</code></pre>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Acceder al Sistema</h3>
            <ul class="list-disc pl-5 space-y-1">
                <li>URL: <code class="bg-gray-100 px-1 rounded">https://localhost:5001</code> o <code class="bg-gray-100 px-1 rounded">http://localhost:5000</code></li>
                <li>El sistema creara automaticamente la base de datos y datos de prueba</li>
            </ul>
        </section>

        <!-- Funcionalidades -->
        <section id="funcionalidades" class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-star mr-2 text-blue-600"></i>Funcionalidades</h2>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Consultas Disponibles</h3>
            <ul class="list-disc pl-5 space-y-1">
                <li><strong>Consulta por Categoria y Año</strong>: Obtener todas las ventas de una categoria especifica en un año determinado</li>
                <li><strong>Resumen Anual</strong>: Estadisticas generales del año consultado, Detalle por categoria con metricas completas</li>
                <li><strong>Gestion Dinamica de Categorias</strong>: Carga automatica de categorias con ventas por año, Interfaz intuitiva con tags seleccionables</li>
            </ul>
            <h3 class="text-xl font-semibold mb-2 text-gray-700 mt-4">Interfaz de Usuario</h3>
            <ul class="list-disc pl-5 space-y-1">
                <li><strong>Responsive Design</strong>: Compatible con dispositivos moviles y desktop</li>
                <li><strong>Interfaz Moderna</strong>: Gradientes, animaciones y efectos visuales</li>
                <li><strong>Navegacion Intuitiva</strong>: Formularios guiados paso a paso</li>
                <li><strong>Retroalimentacion Visual</strong>: Alertas, loading states y mensajes informativos</li>
            </ul>
        </section>

        <!-- API Endpoints -->
        <section id="endpoints" class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-plug mr-2 text-blue-600"></i>API Endpoints</h2>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">GET <code class="bg-gray-100 px-1 rounded">/api/ventas/categorias-con-ventas/{año}</code></h3>
            <p class="mb-2">Obtiene las categorias que tienen ventas en el año especificado.</p>
            <h4 class="text-lg font-medium mb-1">Respuesta:</h4>
            <pre class="bg-gray-100 p-4 rounded-lg overflow-x-auto text-sm mb-4"><code>{
    "success": true,
    "data": ["Electronicos", "Ropa", "Alimentos"],
    "total": 3,
    "mensaje": "Categorias obtenidas correctamente"
}</code></pre>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">GET <code class="bg-gray-100 px-1 rounded">/api/ventas/por-categoria</code></h3>
            <p class="mb-2">Consulta ventas por categoria y año.</p>
            <h4 class="text-lg font-medium mb-1">Parametros:</h4>
            <ul class="list-disc pl-5 mb-4 space-y-1">
                <li><code>nombreCategoria</code>: Nombre de la categoria</li>
                <li><code>año</code>: Año a consultar</li>
            </ul>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">GET <code class="bg-gray-100 px-1 rounded">/api/ventas/resumen-por-categoria/{año}</code></h3>
            <p>Obtiene el resumen completo de ventas por categoria.</p>
        </section>

        <!-- Scripts de Base de Datos -->
        <section id="scripts-db" class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-file-code mr-2 text-blue-600"></i>Scripts de Base de Datos</h2>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Arquitectura de Inicializacion Automatica</h3>
            <p class="mb-4">El sistema implementa un proceso de <strong>auto-bootstrap</strong> con los siguientes scripts embebidos:</p>
            <h4 class="text-lg font-medium mb-1">Script 1: Creacion de Base de Datos</h4>
            <pre class="bg-gray-100 p-4 rounded-lg overflow-x-auto text-sm mb-4"><code>-- =============================================
-- Script 1: Crear Base de datos
-- Recurso: control_de_ventas_1._0.Scripts.CreatBDVentas.sql
-- =============================================

CREATE DATABASE VentasDB;</code></pre>
            <h4 class="text-lg font-medium mb-1">Script 2: Creacion del Esquema</h4>
            <pre class="bg-gray-100 p-4 rounded-lg overflow-x-auto text-sm mb-4"><code>-- =============================================
-- Script 2: Crear tablas, relaciones y objetos en VentasDB
-- Recurso: control_de_ventas_1._0.Scripts.CreatEsquema.sql
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
END</code></pre>
            <h4 class="text-lg font-medium mb-1">Script 3: Poblacion de Datos de Prueba</h4>
            <pre class="bg-gray-100 p-4 rounded-lg overflow-x-auto text-sm mb-4"><code>-- =============================================
-- Script 3: Insercion de datos de prueba
-- Recurso: control_de_ventas_1._0.Scripts.CreatDatosTest.sql
-- Ejecuta solo cuando la base de datos es creada por primera vez
-- =============================================</code></pre>
            <h3 class="text-xl font-semibold mb-2 text-gray-700 mt-4">Proceso de Inicializacion Inteligente</h3>
            <p class="mb-4">El sistema ejecuta la siguiente logica en <code class="bg-gray-100 px-1 rounded">Program.cs</code>:</p>
            <ol class="list-decimal pl-5 space-y-1">
                <li><strong>Verificacion de BD</strong>: Conecta a <code>master</code> para verificar existencia de <code>VentasDB</code></li>
                <li><strong>Creacion condicional</strong>: Si no existe, ejecuta scripts de creacion y poblacion</li>
                <li><strong>Validacion de esquema</strong>: Verifica tablas principales: <code>Productos</code>, <code>Clientes</code>, <code>Categorias</code>, <code>Usuarios</code>, <code>Ventas</code></li>
                <li><strong>Migraciones EF</strong>: Aplica migraciones pendientes de Entity Framework</li>
                <li><strong>Logging detallado</strong>: Proporciona informacion completa del proceso de inicializacion</li>
            </ol>
            <h3 class="text-xl font-semibold mb-2 text-gray-700 mt-4">Caracteristicas del Sistema de BD</h3>
            <ul class="list-disc pl-5 space-y-1">
                <li><strong>Auto-recuperacion</strong>: Recreacion completa si la BD es eliminada</li>
                <li><strong>Idempotencia</strong>: Scripts pueden ejecutarse multiples veces sin errores</li>
                <li><strong>Logging completo</strong>: Informacion detallada de cada paso del proceso</li>
                <li><strong>Timeout extendido</strong>: 300 segundos para operaciones de creacion de datos</li>
                <li><strong>Manejo de errores</strong>: Captura especifica de <code>SqlException</code> y errores generales</li>
            </ul>
        </section>

        <!-- Decisiones de Implementacion -->
        <section id="decisiones-impl" class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-lightbulb mr-2 text-blue-600"></i>Decisiones de Implementacion</h2>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Estrategia de Scaffolding vs. Code-First</h3>
            <ul class="list-disc pl-5 mb-4 space-y-1">
                <li><strong>Decision:</strong> Implementacion de Database-First con Scaffold-DbContext</li>
                <li><strong>Justificacion Tecnica:</strong>
                    <ul class="list-disc pl-5">
                        <li><strong>Fidelidad al Esquema</strong>: Garantiza correspondencia exacta con el modelo de datos especificado</li>
                        <li><strong>Reduccion de Surface Attack</strong>: Minimiza errores de mapeo manual en configuracion de entidades</li>
                        <li><strong>Mantenibilidad</strong>: Regeneracion automatica ante cambios de esquema</li>
                        <li><strong>Performance</strong>: Configuraciones optimizadas generadas por el motor de EF Core</li>
                    </ul>
                </li>
            </ul>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Arquitectura de Persistencia Autodesplegable</h3>
            <ul class="list-disc pl-5 mb-4 space-y-1">
                <li><strong>Decision:</strong> Implementacion de Infrastructure as Code embebida</li>
                <li><strong>Analisis de Requisitos:</strong>
                    <pre class="text-sm bg-gray-100 p-2 rounded inline-block ml-2">
Requisito: Evaluacion tecnica sin dependencias externas
├── Problema: Configuracion manual de BD → Alta probabilidad de fallos
├── Solucion: Scripts DDL embebidos → Inicializacion automatica
└── Resultado: Zero-configuration deployment
                    </pre>
                </li>
                <li><strong>Beneficios Tecnicos:</strong>
                    <ul class="list-disc pl-5">
                        <li><strong>Portabilidad</strong>: Ejecucion en cualquier entorno con SQL Server</li>
                        <li><strong>Consistencia</strong>: Datasets identicos en cada inicializacion</li>
                        <li><strong>Resilencia</strong>: Auto-recuperacion ante corrupcion o eliminacion</li>
                        <li><strong>Evaluacion</strong>: Enfoque en logica de negocio vs. configuracion de infraestructura</li>
                    </ul>
                </li>
            </ul>
            <h3 class="text-xl font-semibold mb-2 text-gray-700">Diseno de API RESTful</h3>
            <ul class="list-disc pl-5 space-y-1">
                <li><strong>Decision:</strong> Implementacion de endpoints especializados por dominio de consulta</li>
                <li><strong>Endpoints Implementados:</strong>
                    <pre class="text-sm bg-gray-100 p-2 rounded inline-block ml-2">
GET /api/ventas/categorias-con-ventas/{año}    # Obtencion de taxonomias activas
GET /api/ventas/por-categoria                  # Consultas filtradas por dominio
GET /api/ventas/resumen-por-categoria/{año}    # Agregaciones y metricas
                    </pre>
                </li>
                <li><strong>Principios de Diseno:</strong>
                    <ul class="list-disc pl-5">
                        <li><strong>Single Responsibility</strong>: Cada endpoint maneja un caso de uso especifico</li>
                        <li><strong>Idempotencia</strong>: Operaciones GET sin efectos secundarios</li>
                        <li><strong>Cacheable</strong>: Respuestas con estructura consistent para optimizacion de cliente</li>
                        <li><strong>Stateless</strong>: Sin mantenimiento de sesion en servidor</li>
                    </ul>
                </li>
            </ul>
        </section>

        <!-- Datos de Prueba -->
        <section class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-database mr-2 text-blue-600"></i>Datos de Prueba</h2>
            <p>El sistema genera automaticamente:</p>
            <ul class="list-disc pl-5 space-y-1">
                <li><strong>10 categorias</strong> predefinidas (Electronicos, Ropa, Alimentos, etc.)</li>
                <li><strong>Productos distribuidos</strong> en cada categoria</li>
                <li><strong>Ventas de muestra</strong> distribuidas en diferentes años (2018-2024)</li>
                <li><strong>Fechas realistas</strong> para simular operacion real</li>
            </ul>
        </section>

        <!-- Contribucion -->
        <section class="mb-12 bg-white rounded-xl shadow-lg p-6">
            <h2 class="text-2xl font-bold mb-4 text-gray-800 border-b pb-2"><i class="fas fa-handshake mr-2 text-blue-600"></i>Contribucion</h2>
            <p>Este proyecto fue desarrollado como una demostracion de habilidades tecnicas, siguiendo las mejores practicas de desarrollo .NET y diseno de bases de datos.</p>
        </section>

        <!-- Footer -->
        <footer class="bg-blue-900 text-white text-center py-6 mt-12 rounded-xl">
            <div class="container mx-auto px-4">
                <p class="font-semibold">© Derechos reservados a favor de Jeremías de León – jeredeleon@yahoo.com</p>
                <p class="text-sm text-gray-300">Sistema de Control de Ventas - Desarrollado con ASP.NET Core</p>
            </div>
        </footer>
    </main>
</body>
</html>
```
