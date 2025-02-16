using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using RiegoWeb.Api.Data; // Asegúrate de que este using esté presente

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Configura el DbContext para MySQL
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Agrega soporte para controladores (necesario para una Web API)
builder.Services.AddControllers();

// Configura Swagger para documentar la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura el pipeline de solicitudes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilita el enrutamiento de controladores
app.MapControllers();

// Habilitar CORS
app.UseCors("AllowAll");

app.Run();
