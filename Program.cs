using Microsoft.EntityFrameworkCore;
using RiegoWeb.Api.Data;
using Microsoft.AspNetCore.SignalR;
using RiegoWeb.Api.Hubs;  // Asegúrate de usar el espacio de nombres correcto
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer; // <-- Asegúrate de agregar esto

var builder = WebApplication.CreateBuilder(args);

// Configuración de la conexión a MySQL con manejo de errores
try
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<MyDbContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
               .LogTo(Console.WriteLine, LogLevel.Information) // Habilita logs de EF
    );
}
catch (Exception ex)
{
    Console.WriteLine($"Error al conectar a la BD: {ex.Message}");
}

// Registrar RandomDataHub en el contenedor de dependencias
builder.Services.AddScoped<RandomDataHub>();

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithOrigins("http://localhost:5149", "https://localhost:5149")
              .AllowCredentials();
    });
});

// Agregar servicios
builder.Services.AddScoped<IAuthService, AuthService>();

// Configurar JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "tu_issuer",
            ValidAudience = "tu_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tu_clave_secreta"))
        };
    });

// Agrega controladores y SignalR
builder.Services.AddControllers();
builder.Services.AddSignalR();

// Configura Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware global para capturar errores y evitar error 500 sin logs
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error interno del servidor: {ex.Message}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Error interno del servidor");
    }
});

// Configura el pipeline de middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Habilita CORS antes de los controladores
app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
// Habilita enrutamiento de controladores y SignalR
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapHub<RandomDataHub>("/randomDataHub");

app.Run();
