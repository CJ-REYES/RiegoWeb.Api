using Microsoft.EntityFrameworkCore;
using RiegoWeb.Api.Models;

namespace RiegoWeb.Api.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Modulos> Modulos { get; set; }
        public DbSet<LecturaModulo> LecturaModulo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones
            modelBuilder.Entity<Modulos>()
                .HasOne(m => m.User)            // Un módulo pertenece a un usuario
                .WithMany(u => u.Modulos)       // Un usuario tiene muchos módulos
                .HasForeignKey(m => m.Id_User); // Clave foránea en Modulos

            modelBuilder.Entity<LecturaModulo>()
                .HasOne(l => l.Modulo)           // Una lectura pertenece a un módulo
                .WithMany(m => m.Lecturas)      // Un módulo tiene muchas lecturas
                .HasForeignKey(l => l.Id_Modulo);// Clave foránea en LecturaModulo

            modelBuilder.Entity<LecturaModulo>()
            .Property(l => l.Id)
            .ValueGeneratedOnAdd(); // Asegura AUTO_INCREMENT

        }
    }
}