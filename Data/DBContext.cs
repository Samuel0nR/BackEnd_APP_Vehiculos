using System;
using System.Collections.Generic;
using api_dotNet_vehicles.Models;
using Microsoft.EntityFrameworkCore;

namespace api_dotNet_vehicles.Data
{
    public partial class DBContext : DbContext
    {
        public DBContext() { }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public virtual DbSet<CategoriaVModel> CategoriaVs { get; set; }
        public virtual DbSet<TipoVModel> TipoVs { get; set; }
        public virtual DbSet<BikesDetModel> BikesDet { get; set; }
        public virtual DbSet<CarsDetModel> CarsDet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<CategoriaVModel>(entity =>
            {
                entity.HasKey(e => e.Codigo).HasName("PRIMARY");

                entity.ToTable("Categoria_V");

                entity.Property(e => e.Codigo).HasColumnName("cat_codigo");
                entity.Property(e => e.Categoria)
                    .HasMaxLength(50)
                    .HasColumnName("cat_descripcion");
            });

            modelBuilder.Entity<TipoVModel>(entity =>
            {
                entity.HasNoKey()
                      .ToTable("Tipo_V");

                entity.HasIndex(e => e.CatV, "cat_v");

                entity.Property(e => e.CatV)
                      .HasDefaultValueSql("'5'")
                      .HasColumnName("cat_v");

                entity.Property(e => e.Codigo)
                      .HasMaxLength(5)
                      .HasColumnName("tip_codigo");

                entity.Property(e => e.Tipo)
                      .HasMaxLength(50)
                      .HasColumnName("tip_descripcion");
            });

            modelBuilder.Entity<BikesDetModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");

                entity.ToTable("BIKES_DET");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CodTipo)
                      .HasMaxLength(4)
                      .HasColumnName("Cod_Tipo");

                entity.Property(e => e.CodVehi).HasColumnName("Cod_Vehi");
                entity.Property(e => e.Cuadro).HasMaxLength(50);
                entity.Property(e => e.Frenos).HasMaxLength(50);
                entity.Property(e => e.Llanta).HasMaxLength(80);
                entity.Property(e => e.Marca).HasMaxLength(80);
                entity.Property(e => e.Modelo).HasMaxLength(100);
                entity.Property(e => e.Peso).HasMaxLength(50);
                entity.Property(e => e.Suspension).HasMaxLength(50);
                entity.Property(e => e.Valvula).HasMaxLength(50);
            });

            modelBuilder.Entity<CarsDetModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PRIMARY");
                entity.ToTable("CARS_DET");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.CodVehi).HasColumnName("Cod_Vehi");
                entity.Property(e => e.Marca);
                entity.Property(e => e.Modelo);
                entity.Property(e => e.CodTipo)
                      .HasMaxLength(4)
                      .HasColumnName("Cod_Tipo");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
