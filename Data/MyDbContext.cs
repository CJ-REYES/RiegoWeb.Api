using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RiegoWeb.Api.Models;

namespace RiegoWeb.Api.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Modulos> Modulos { get; set; }
        public DbSet<MyModulos> MyModulos { get; set; }
        public DbSet<HistoriaDeModulos> HistoriaDeModulos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la tabla User
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id_User);

            // Configuración de la tabla HistorialDeModulos
            modelBuilder.Entity<HistoriaDeModulos>()
                .HasKey(h => h.Id_HistoriaDeModulos); // Aquí eliminamos el paréntesis extra

            // Configuración de la tabla Modulo
            modelBuilder.Entity<Modulos>()
                .HasKey(m => m.Id_Modulos);

            // Configuración de la tabla MyModulo
            modelBuilder.Entity<MyModulos>()
                .HasKey(mm => mm.IdMyModulo);

            // Relación MyModulo -> User (Muchos a Uno)
            modelBuilder.Entity<MyModulos>()
                .HasOne(mm => mm.User)
                .WithMany()
                .HasForeignKey(mm => mm.Id_User)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación MyModulo -> Modulo (Muchos a Uno)
            modelBuilder.Entity<MyModulos>()
                .HasOne(mm => mm.Modulo)
                .WithMany()
                .HasForeignKey(mm => mm.Id_Modulo)
                .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<HistoriaDeModulos>()
    .HasOne(h => h.Modulo)  // Relación con Modulos
    .WithMany()  // Un Modulo puede tener muchas HistoriasDeModulos
    .HasForeignKey(h => h.Id_Modulos)  // Clave foránea en HistoriaDeModulos
    .OnDelete(DeleteBehavior.Cascade);  // El comportamiento al eliminar


            base.OnModelCreating(modelBuilder);
        }
    }
}
