# Proyecto Blazor - Gestión de Dispositivos IoT (Asignar Nombre)

Este proyecto es una aplicación web desarrollada en .NET 8 con Blazor, Estara diseñada para gestionar dispositivos IoT de manera eficiente. La aplicación permite registrar, monitorear y administrar dispositivos IoT en tiempo real.

## Tecnologías utilizadas

- **.NET 8** (Blazor WebAssembly)
- **Entity Framework Core** (para la gestión de la base de datos)
- **API REST con ASP.NET Core** (backend)
- **MySQL** (como base de datos, configurable según necesidad)
- **Autenticación y Autorización con Identity**
- **MQTT / WebSockets** (para comunicación con dispositivos IoT)

## Requisitos previos

Antes de instalar y ejecutar el proyecto, asegúrate de contar con lo siguiente:

- **.NET SDK 8.0 o superior**: [Descargar aquí](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- **Node.js y npm** (si se usa algún paquete frontend adicional): [Descargar aquí](https://nodejs.org/)
- **Servidor de base de datos** (SQLite, SQL Server u otro compatible con EF Core)

## Instalación

1. Clona el repositorio:

   ```sh
   git clone https://github.com/Carlos-1803/RiegoWeb.git
   cd RiegoWeb
   ```

2. Restaura las dependencias:

   ```sh
   dotnet restore
   ```

3. Configura la base de datos:

   - Edita el archivo `appsettings.json` para configurar la cadena de conexión.
   - Aplica las migraciones de Entity Framework Core:
     ```sh
     dotnet ef database update
     ```

4. Ejecuta la API (backend):

   ```sh
   cd Server
   dotnet run
   ```

5. Ejecuta el frontend BlazorWASM:
   ```sh
   cd Client
   dotnet run
   ```

## Uso

- Accede a la web en `http://localhost:puerto` o el puerto configurado.
- Registra dispositivos IoT y monitorea su estado en tiempo real.
- Configura y envía comandos a los dispositivos.

## Contribuciones

Se tiene que contribuir TODOS nadie puede quedarse sin trabajar

---

_¡Inico de proyecto de gestion de Iot!_ 🚀

## Estructura del Backend

