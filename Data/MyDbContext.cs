using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RiegoWeb.Api.Models;



namespace RiegoWeb.Api.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Modulos> Modulo { get; set; }
        public DbSet<MyModulos> MyModulo { get; set; }
        public object Modulos { get; internal set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{

    modelBuilder.Entity<User>()
          .HasKey(u=>u.Id_User);
    modelBuilder.Entity<Modulos>()
          .HasKey(u=>u.Id_Modulos);
   modelBuilder.Entity<MyModulos>()
          .HasKey(u=>u.IdMyModulo);


    // Relación entre MyModulo y User
    modelBuilder.Entity<MyModulos>()
        .HasOne(m => m.User) // MyModulo tiene un User
        .WithMany(u => u.MyModulos) // User tiene muchos MyModulo
        .HasForeignKey(m => m.Id_User) ;// Clave foránea en MyModulo
       
    // Relación entre MyModulo y Modulos
    modelBuilder.Entity<MyModulos>()

        .HasOne(m => m.Modulo) // MyModulo tiene un Modulo
        .WithMany(mod => mod.MyModulos) // Modulos tiene muchos MyModulo
        .HasForeignKey(m => m.Id_Modulo);



        
}
}

}