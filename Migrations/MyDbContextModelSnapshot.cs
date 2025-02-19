﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RiegoWeb.Api.Data;

#nullable disable

namespace RiegoWeb.Api.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("RiegoWeb.Api.Models.Modulos", b =>
                {
                    b.Property<int>("Id_Modulos")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_Modulos"));

                    b.Property<string>("Humedad")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LuzNivel")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Temperatura")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id_Modulos");

                    b.ToTable("Modulos");
                });

            modelBuilder.Entity("RiegoWeb.Api.Models.MyModulos", b =>
                {
                    b.Property<int>("IdMyModulo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdMyModulo"));

                    b.Property<int>("Id_Modulo")
                        .HasColumnType("int");

                    b.Property<int>("Id_User")
                        .HasColumnType("int");

                    b.HasKey("IdMyModulo");

                    b.HasIndex("Id_Modulo");

                    b.HasIndex("Id_User");

                    b.ToTable("MyModulos");
                });

            modelBuilder.Entity("RiegoWeb.Api.Models.User", b =>
                {
                    b.Property<int>("Id_User")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id_User"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id_User");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RiegoWeb.Api.Models.MyModulos", b =>
                {
                    b.HasOne("RiegoWeb.Api.Models.Modulos", "Modulo")
                        .WithMany()
                        .HasForeignKey("Id_Modulo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RiegoWeb.Api.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("Id_User")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Modulo");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
