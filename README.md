# ControlEquipos

Sistema web desarrollado para registrar y administrar equipos tecnológicos mediante operaciones CRUD. Fue creado como proyecto de inducción utilizando ASP.NET Core MVC.

## Funcionalidades

- Registrar equipos.
- Consultar el listado de equipos.
- Ver los detalles de un equipo.
- Editar equipos existentes.
- Eliminar equipos con confirmación previa.
- Validar campos obligatorios y límites de longitud.
- Evitar seriales duplicados mediante validación en la aplicación y un índice único en SQL Server.
- Controlar el estado del equipo con los valores permitidos:
  - Disponible.
  - En uso.
  - Mantenimiento.
- Mostrar mensajes de confirmación después de registrar, actualizar o eliminar.
- Adaptar la interfaz a dispositivos móviles y de escritorio mediante Bootstrap.

## Tecnologías

- C#
- .NET 10
- ASP.NET Core MVC
- Entity Framework Core 10.0.11
- SQL Server
- Razor
- Bootstrap
- Visual Studio 2026
- SQL Server Management Studio

## Arquitectura

El proyecto utiliza el patrón Modelo-Vista-Controlador (MVC):

- **Models:** representan los datos y sus validaciones.
- **Views:** construyen la interfaz con Razor y Bootstrap.
- **Controllers:** coordinan la interfaz, el modelo y el acceso a la base de datos.
- **Data:** contiene `ControlEquiposContext` y la configuración de Entity Framework Core.

## Modelo Equipo

| Campo | Descripción y validación |
| --- | --- |
| `Id` | Identificador numérico del equipo. |
| `Nombre` | Nombre del equipo. Máximo 100 caracteres. |
| `Tipo` | Tipo de equipo. Máximo 50 caracteres. |
| `Marca` | Marca del equipo. Máximo 50 caracteres. |
| `Modelo` | Modelo del equipo. Máximo 100 caracteres. |
| `Serial` | Serial del equipo. Máximo 100 caracteres y valor único. |
| `Estado` | Estado actual del equipo. Máximo 30 caracteres. |

Los estados permitidos son:

- Disponible
- En uso
- Mantenimiento

## Base de datos

El proyecto utiliza la base de datos `ControlEquiposDB` y la tabla `Equipos`. Entity Framework Core administra el esquema mediante migraciones.

Migraciones actuales:

- `20260812152613_Inicial`
- `20260813151650_ValidacionesIntegridadEquipo`

El índice `IX_Equipos_Serial` es único y protege la integridad del serial en SQL Server.

## Requisitos

- Windows
- Visual Studio 2026
- .NET SDK 10
- SQL Server Express
- SQL Server Management Studio

Durante el desarrollo se utilizó la instancia `localhost\SQLEXPRESS`. Si el entorno utiliza una instancia diferente, se debe ajustar la cadena de conexión antes de ejecutar el proyecto.

## Configuración de conexión

La cadena de conexión para desarrollo local está configurada en `appsettings.Development.json` y utiliza autenticación integrada de Windows.

Ejemplo basado en el proyecto:

```text
Server=localhost\SQLEXPRESS;
Database=ControlEquiposDB;
Trusted_Connection=True;
TrustServerCertificate=True;
```

El ejemplo no contiene contraseñas y está destinado únicamente al entorno de desarrollo local.

## Ejecución

1. Clonar el repositorio.
2. Abrir `ControlEquipos.slnx` con Visual Studio.
3. Confirmar que SQL Server Express esté ejecutándose.
4. Revisar la cadena de conexión en `appsettings.Development.json`.
5. Restaurar los paquetes NuGet si Visual Studio lo requiere.
6. Aplicar las migraciones desde Package Manager Console:

   ```powershell
   Update-Database
   ```

7. Ejecutar el proyecto desde Visual Studio.

## Estructura general

```text
ControlEquipos/
├── ControlEquipos.slnx
├── README.md
└── ControlEquipos/
    ├── Controllers/
    ├── Data/
    ├── Migrations/
    ├── Models/
    ├── Views/
    ├── wwwroot/
    ├── Program.cs
    ├── appsettings.json
    └── appsettings.Development.json
```

## Estado del proyecto

- CRUD completo.
- Validaciones implementadas.
- Integridad del serial verificada en la aplicación y en SQL Server.
- Interfaz responsive verificada.
- Pruebas funcionales finales: 18 de 18 aprobadas.
- Compilación final verificada con 0 errores y 0 advertencias.
