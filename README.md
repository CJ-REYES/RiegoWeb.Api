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

- El backend está organizado de la siguiente manera:

1. Controladores

- MyModulosControllers: Gestiona las operaciones CRUD para los dispositivos IoT pertenecientes a los Usuarios.

- ModuloControllers: Recibe datos de los dispositivos IoT (Arduino) y los almacena en la base de datos.

- UsuariosController: Gestiona la autenticación y autorización de usuarios.

2. Modelos
- MyModulo: Representa un dispositivo IoT pertenecientes al Usuario.

- Modulos: Representa los datos enviados por los dispositivos IoT.

- Usuario: Representa un usuario del sistema.

3. DbContext
- MyDbContext: Configura la conexión a la base de datos y define las tablas.

4. Migraciones
- Las migraciones de Entity Framework Core se utilizan para crear y actualizar la base de datos.
46. se cambio la estructura del los `Modelos`. para que funcione el proyecto necesitas hacer lo siguente
-  Borrar la base datos
```sh
   MySql -u root
   show database
   drop database  RiegoWebApidb
   ```
- Crear la base datos nuevamente
```sh
   Create Database RiegoWebApi
   ```
- Actualizar la base datos
```sh
  dotnet ef database update
   ```
47. se creo el modelo `MyModuloRequest.cs` el  cual se utilizo para simplifacar el Json del controllers `MyModulosControllers`

48. se creo un nueva carpeta  llamada services donde esta  `RandomData.cs` para generar datos aleatorios.

50. para generar datos aleatoreos se  modofico el controllers de modulos para  generar datos random,donde se encuentra el metodo post llamado generar o referenciado con `Api/Modulos/Generar`.

51. Modifique la base de datos, cree una nueva migracion para que funcione la generacion de tokens de autentificacion
```sh
  dotnet ef database update
   ```


52. Hubo modificaciones en la base de datos, se cambio el modelo `Moddulos` para que pueda añadir los registros de datos del arduino asia la api, agregaano fecha y id del modulo `IdModuloIot`.

```sh
dotnet ef database update
```

53. Al intentar soluconar el detalle de guardar un historial con los datos de los modulos con fecha para que se muestre en la grafica, por la cual subi 2 actualizacions con una logica un tanto incorrecta.

- por la cual retrocedi una vercion y cambie de rama para subir una solucion para esto. `Frima: Pendertuga  "un CRAK" ESA FUE MI RODILLA`

54. SE CAMBIO LA LOGICA DEL PROYECTO, SE IMPLEMTO EL MODELO `HistoorialDeModulos.cs`, cada vez que el `Arduino` cree o actualice un registro en `Moddulo`, el controlador `ModulosControllers.cs` guarda los datos tanto en `Modulos` como en `HistorialDeModulos`. para esto se modifico la base de datos, para que te funcione el proyecto al traer los datos no olvide de hacer un.
```sh
  dotnet ef database update
   ```

