## 📋 Tabla de Contenidos

<img width="1655" height="865" alt="Captura de pantalla 2025-09-26 a la(s) 22 57 06" src="https://github.com/user-attachments/assets/75a2c350-7e39-42e6-9c7d-5ce21d33eb82" />
<img width="1639" height="854" alt="Captura de pantalla 2025-09-26 a la(s) 22 57 57" src="https://github.com/user-attachments/assets/a3438ab6-a5b9-408c-a85c-f43409a232af" />


- [Requisitos del Sistema](#requisitos-del-sistema)
- [Arquitectura de la Base de Datos](#arquitectura-de-la-base-de-datos)
- [Analisis Tecnico y Decisiones de Diseno](#analisis-tecnico-y-decisiones-de-diseno)
- [Tecnologias Utilizadas](#tecnologias-utilizadas)
- [Instalacion y Configuracion](#instalacion-y-configuracion)
- [Funcionalidades](#funcionalidades)
- [API Endpoints](#api-endpoints)
- [Scripts de Base de Datos](#scripts-de-base-de-datos)
- [Decisiones de Implementacion](#decisiones-de-implementacion)

Sistema web empresarial desarrollado en ASP.NET Core 8.0 para la gestion integral y consulta analitica de ventas segmentadas por categorias de productos, implementando una arquitectura de microservicios con API REST y persistencia de datos en SQL Server.

## ⚙️ Requisitos del Sistema

### Prerrequisitos Obligatorios
- **SQL Server LocalDB** (incluido con Visual Studio o .NET SDK)
  - Instancia: `(localdb)\MSSQLLocalDB`
  - **No requiere configuracion de puerto especifico**
  - Conexion mediante Named Pipes (local)
  - **Nota**: El sistema tambien es compatible con otras ediciones de SQL Server
- **.NET 8 SDK** o superior
- **Puerto 5281** disponible para la aplicacion web (configuracion especifica del proyecto)

### Verificacion de Prerequisitos
```bash
# Verificar SQL Server LocalDB
sqllocaldb info MSSQLLocalDB
sqllocaldb start MSSQLLocalDB

# Verificar conectividad a LocalDB
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -Q "SELECT @@VERSION"

# Verificar .NET 8
dotnet --version

# Verificar puerto especifico del proyecto
netstat -an | findstr :5281
```

## 🎯 Analisis Tecnico y Decisiones de Diseno

### Analisis del Modelo de Datos Normalizado

La arquitectura de datos implementa un modelo relacional de tercer forma normal (3NF) con las siguientes caracteristicas:

**Entidades del Dominio:**
- **Categoria**: Entidad maestro que funciona como taxonomia de clasificacion
- **Producto**: Entidad de catalogo con referencia categorica
- **Venta**: Entidad transaccional con timestamp automatico

**Relaciones de Cardinalidad:**
```
Categoria ||-----o{ Producto ||-----o{ Venta
    1                N          1         N
```

### Decisiones Arquitectonicas Implementadas

#### 1. Estrategia de Persistencia de Datos
La implementacion mantiene **absoluta fidelidad** al esquema de referencia proporcionado, aplicando principios de:

- **Code First Database Approach**: Utilizacion de Entity Framework Core con scaffold reverso
- **Modelo de Dominio Puro**: Sin modificaciones a la estructura base de tablas
- **Integridad Referencial**: Implementacion de constraints FK completas

#### 2. Generacion Automatica de Contexto ORM
Implementacion de scaffold para generacion automatica de:

```bash
Scaffold-DbContext "Data Source=localhost;Initial Catalog=VentasDB;Integrated Security=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context ControlVentasContext
```

**Artefactos Generados:**
- `ControlVentasContext.cs`: DbContext principal
- `Categorium.cs`: Entidad de categoria (pluralizacion automatica de EF)
- `Producto.cs`: Entidad de producto
- `Ventum.cs`: Entidad de venta (pluralizacion automatica de EF)

#### 3. Arquitectura de Base de Datos Autodesplegable
Implementacion de una estrategia de **Infrastructure as Code** para la inicializacion automatica del entorno de datos:

**Problematica de Despliegue Identificada:** 
La distribucion de bases de datos preconfiguradas presenta los siguientes vectores de riesgo tecnico:
- Incompatibilidades de version entre instancias de SQL Server
- Complejidad de configuracion de connection strings personalizados
- Dependencias de permisos de usuario en sistemas de evaluacion
- Overhead operacional para inicializacion de entornos de prueba

**Solucion de Ingenieria Implementada:**
Desarrollo de un sistema de **auto-bootstrap** que se conecta a `(localdb)\MSSQLLocalDB` y ejecuta de manera condicional:

1. **Verificacion de existencia** de la base de datos `VentasDB`
2. **Ejecucion automatica** de scripts DDL para creacion de esquema
3. **Poblacion inicial** con datasets de prueba representativos
4. **Capacidad de regeneracion** completa ante eliminacion accidental

## 📄 Scripts de Base de Datos

### Arquitectura de Inicializacion Automatica
El sistema implementa un proceso de **auto-bootstrap** con los siguientes scripts embebidos:

#### Script 1: Creacion de Base de Datos
```sql
-- =============================================
-- Script 1: Crear Base de datos
-- Recurso: control_de_ventas_1._0.Scripts.CreatBDVentas.sql
-- =============================================

CREATE DATABASE VentasDB;
```

#### Script 2: Creacion del Esquema
```sql
-- =============================================
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
END
```

#### Script 3: Poblacion de Datos de Prueba
```sql
-- =============================================
-- Script 3: Insercion de datos de prueba
-- Recurso: control_de_ventas_1._0.Scripts.CreatDatosTest.sql
-- Ejecuta solo cuando la base de datos es creada por primera vez
-- =============================================
```

### Proceso de Inicializacion Inteligente
El sistema ejecuta la siguiente logica en `Program.cs`:

1. **Verificacion de BD**: Conecta a `master` para verificar existencia de `VentasDB`
2. **Creacion condicional**: Si no existe, ejecuta scripts de creacion y poblacion
3. **Validacion de esquema**: Verifica tablas principales: `Productos`, `Clientes`, `Categorias`, `Usuarios`, `Ventas`
4. **Migraciones EF**: Aplica migraciones pendientes de Entity Framework
5. **Logging detallado**: Proporciona informacion completa del proceso de inicializacion

### Caracteristicas del Sistema de BD
- **Auto-recuperacion**: Recreacion completa si la BD es eliminada
- **Idempotencia**: Scripts pueden ejecutarse multiples veces sin errores
- **Logging completo**: Informacion detallada de cada paso del proceso
- **Timeout extendido**: 300 segundos para operaciones de creacion de datos
- **Manejo de errores**: Captura especifica de `SqlException` y errores generales

## 🗃️ Arquitectura de la Base de Datos

### Modelo Relacional Implementado
El sistema implementa un esquema relacional normalizado de tres entidades principales con integridad referencial completa:

```
┌─────────────────┐    ┌─────────────────┐    ┌─────────────────┐
│    Categoria    │    │    Producto     │    │     Venta       │
├─────────────────┤    ├─────────────────┤    ├─────────────────┤
│ CodigoCategoria │◄──┐│ CodigoProducto  │◄──┐│ CodigoVenta     │
│ Nombre          │   └│ Nombre          │   └│ Fecha           │
└─────────────────┘    │ CodigoCategoria │    │ CodigoProducto  │
                       └─────────────────┘    └─────────────────┘
```

### Especificaciones Tecnicas del Esquema

**Relaciones Implementadas:**
- `Categoria (1:N) Producto`: Una categoria puede contener multiples productos
- `Producto (1:N) Venta`: Un producto puede tener multiples registros de venta
- **Integridad Referencial**: Constraints de clave foranea con CASCADE en consultas

**Indices y Constraints:**
- Primary Keys con IDENTITY(1,1) para auto-incremento
- Foreign Key Constraints con nombres descriptivos
- Campos NOT NULL en atributos criticos
- Valores por defecto (GETDATE()) para auditoria temporal

## 💻 Tecnologias Utilizadas

### Stack Tecnologico Backend
- **Framework**: ASP.NET Core 8.0 (LTS)
- **ORM**: Entity Framework Core 8.0.x
- **Base de Datos**: Microsoft SQL Server (compatible con versiones 2019+)
- **Patron Arquitectonico**: Model-View-Controller (MVC) con API REST
- **Inyeccion de Dependencias**: Built-in DI Container de .NET Core

### Stack Tecnologico Frontend
- **Lenguajes**: HTML5, CSS3, JavaScript (ES6+)
- **Framework CSS**: Tailwind CSS 3.x (CDN)
- **Iconografia**: Font Awesome 6.4.0
- **Arquitectura**: SPA components con Vanilla JavaScript
- **Responsive Design**: Mobile-first approach

### Herramientas de Desarrollo
- **IDE Recomendado**: Visual Studio 2022 / Visual Studio Code
- **Package Manager**: NuGet Package Manager
- **Control de Versiones**: Git
- **Scaffolding**: Entity Framework Core Tools

## ⚡ Requisitos del Sistema

### Software Requerido
- **.NET 8 SDK** o superior
- **SQL Server** (una sola edicion recomendada):
  - **SQL Server LocalDB** (incluido con Visual Studio/.NET SDK) - *Recomendado*
  - **SQL Server Express** (gratuito, instalacion independiente)
  - **SQL Server Developer Edition** (gratuito, funcionalidad completa)
  - **SQL Server Standard/Enterprise** (licencias comerciales)

### Consideraciones Importantes de Instalacion

> **⚠️ ADVERTENCIA DE RENDIMIENTO**  
> **NO instale multiples ediciones de SQL Server simultaneamente** en el mismo sistema. La coexistencia de LocalDB + SQL Server Express/Developer puede causar:
> - Conflictos de puertos y named pipes
> - Degradacion significativa del rendimiento del sistema
> - Problemas de conectividad intermitentes
> - Consumo excesivo de recursos de memoria

**Recomendacion de Configuracion:**
- **Para desarrollo**: Usar unicamente SQL Server LocalDB
- **Para produccion**: Migrar a SQL Server Express o superior
- **Para evaluacion tecnica**: LocalDB es la opcion optima

### Verificacion de Prerequisitos
```bash
# Para SQL Server LocalDB (configuracion por defecto)
sqllocaldb info
sqllocaldb info MSSQLLocalDB
sqllocaldb start MSSQLLocalDB
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -Q "SELECT @@VERSION"

# Para SQL Server Express/Developer (configuracion alternativa)
sqlcmd -S localhost -E -Q "SELECT @@VERSION, @@SERVERNAME"
sqlcmd -S .\SQLEXPRESS -E -Q "SELECT @@VERSION, @@SERVERNAME"

# Verificar version de .NET SDK
dotnet --version
# Output esperado: 8.0.x o superior

# Verificar puerto especifico del proyecto
netstat -an | findstr :5281
```

### URLs de la Aplicacion (Puerto 5281)
Una vez iniciada la aplicacion, estara disponible en:
- **Pagina principal**: `http://localhost:5281/controlventas/`
- **API de ventas**: `http://localhost:5281/api/ventas/`
- **Documentacion Swagger**: `http://localhost:5281/swagger/`

### Configuracion de Connection String

**Para SQL Server LocalDB (por defecto):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=VentasDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

**Para SQL Server Express (alternativo):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VentasDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

**Para SQL Server Developer/Standard (alternativo):**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=VentasDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

> **📝 NOTA DE CONFIGURACION**  
> El sistema detectara automaticamente la instancia disponible. Si tiene multiples versiones instaladas, modifique el `appsettings.json` con la cadena de conexion correspondiente a su instalacion activa.

## 🚀 Instalacion y Configuracion

### 1. Descargar o Clonar el Repositorio
Se recomienda descargar o clonar el repositorio para obtener todos los archivos del proyecto de forma completa.

```bash
# Opción 1: Clonar el repositorio
git clone https://github.com/usuario/sistema-control-ventas.git  
cd sistema-control-ventas

# Opción 2: Descargar como archivo ZIP (desde GitHub)
# Luego extraer el archivo ZIP en una carpeta local.
```

### 2. Restaurar Dependencias
```bash
dotnet restore
```

### 3. Ejecutar la Aplicacion
```bash
dotnet run
```

### 4. Acceder al Sistema
- URL: `https://localhost:5001` o `http://localhost:5000`
- El sistema creara automaticamente la base de datos y datos de prueba

## ✨ Funcionalidades

### Consultas Disponibles
1. **Consulta por Categoria y Año**
   - Obtener todas las ventas de una categoria especifica en un año determinado
   
2. **Resumen Anual**
   - Estadisticas generales del año consultado
   - Detalle por categoria con metricas completas
   
3. **Gestion Dinamica de Categorias**
   - Carga automatica de categorias con ventas por año
   - Interfaz intuitiva con tags seleccionables

### Interfaz de Usuario
- **Responsive Design**: Compatible con dispositivos moviles y desktop
- **Interfaz Moderna**: Gradientes, animaciones y efectos visuales
- **Navegacion Intuitiva**: Formularios guiados paso a paso
- **Retroalimentacion Visual**: Alertas, loading states y mensajes informativos

## 🔌 API Endpoints

### GET `/api/ventas/categorias-con-ventas/{año}`
Obtiene las categorias que tienen ventas en el año especificado.

**Respuesta:**
```json
{
    "success": true,
    "data": ["Electronicos", "Ropa", "Alimentos"],
    "total": 3,
    "mensaje": "Categorias obtenidas correctamente"
}
```

### GET `/api/ventas/por-categoria`
Consulta ventas por categoria y año.

**Parametros:**
- `nombreCategoria`: Nombre de la categoria
- `año`: Año a consultar

### GET `/api/ventas/resumen-por-categoria/{año}`
Obtiene el resumen completo de ventas por categoria.

## 🎯 Decisiones de Implementacion Tecnica

### 1. Estrategia de Scaffolding vs. Code-First
**Decision:** Implementacion de Database-First con Scaffold-DbContext

**Justificacion Tecnica:**
- **Fidelidad al Esquema**: Garantiza correspondencia exacta con el modelo de datos especificado
- **Reduccion de Surface Attack**: Minimiza errores de mapeo manual en configuracion de entidades
- **Mantenibilidad**: Regeneracion automatica ante cambios de esquema
- **Performance**: Configuraciones optimizadas generadas por el motor de EF Core

### 2. Arquitectura de Persistencia Autodesplegable
**Decision:** Implementacion de Infrastructure as Code embebida

**Analisis de Requisitos:**
```
Requisito: Evaluacion tecnica sin dependencias externas
├── Problema: Configuracion manual de BD → Alta probabilidad de fallos
├── Solucion: Scripts DDL embebidos → Inicializacion automatica
└── Resultado: Zero-configuration deployment
```

**Beneficios Tecnicos:**
- **Portabilidad**: Ejecucion en cualquier entorno con SQL Server
- **Consistencia**: Datasets identicos en cada inicializacion
- **Resilencia**: Auto-recuperacion ante corrupcion o eliminacion
- **Evaluacion**: Enfoque en logica de negocio vs. configuracion de infraestructura

### 3. Diseno de API RESTful
**Decision:** Implementacion de endpoints especializados por dominio de consulta

**Endpoints Implementados:**
```http
GET /api/ventas/categorias-con-ventas/{año}    # Obtencion de taxonomias activas
GET /api/ventas/por-categoria                  # Consultas filtradas por dominio
GET /api/ventas/resumen-por-categoria/{año}    # Agregaciones y metricas
```

**Principios de Diseno:**
- **Single Responsibility**: Cada endpoint maneja un caso de uso especifico
- **Idempotencia**: Operaciones GET sin efectos secundarios
- **Cacheable**: Respuestas con estructura consistent para optimizacion de cliente
- **Stateless**: Sin mantenimiento de sesion en servidor

## 📊 Datos de Prueba

El sistema genera automaticamente:
- **10 categorias** predefinidas (Electronicos, Ropa, Alimentos, etc.)
- **Productos distribuidos** en cada categoria
- **Ventas de muestra** distribuidas en diferentes años (2018-2024)
- **Fechas realistas** para simular operacion real

## Ademas se incluye un script de consulta que devuelve la categoria de la ultima venta

## 🤝 Contribucion

Este proyecto fue desarrollado como una demostracion de habilidades tecnicas, siguiendo las mejores practicas de desarrollo .NET y diseno de bases de datos.

---

**Desarrollado por**: Jeremias de Leon  
**Email**: jeredeleon@yahoo.com  
**Tecnologia**: ASP.NET Core 8 con Entity Framework Core
