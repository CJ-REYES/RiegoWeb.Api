using Microsoft.EntityFrameworkCore;
using RiegoWeb.Api.Data; // Asegúrate de importar este namespace para RandomDataHub
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la conexión a MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Registrar RandomDataHub en el contenedor de dependencias
builder.Services.AddScoped<RandomDataHub>();  // Registrar RandomDataHub aquí

// Configuración de CORS (con WebSockets habilitados)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "https://localhost:5173") // ⚠ Ajusta esto según tu frontend
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials(); // Permite credenciales (necesario para SignalR)
    });
});

// Agrega controladores (para API)
builder.Services.AddControllers();
builder.Services.AddSignalR(); // Necesario para SignalR

// Configura Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura el pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilita CORS antes de los controladores
app.UseCors("AllowAll");

// Habilita enrutamiento de controladores y SignalR
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<RandomDataHub>("/randomDataHub"); // ✅ SignalR aquí
});

app.Run();