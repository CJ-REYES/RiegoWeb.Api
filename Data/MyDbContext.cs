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
         
          public DbSet<LecturaModulo> LecturaModulo { get; set; }

       
       }}