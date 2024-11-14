using System;
using System.Collections.Generic;
using api_dotNet_vehicles.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace api_dotNet_vehicles.Data;

public partial class DBContext : DbContext
{
    public DBContext()
    {

    }

    public DBContext(DbContextOptions<DBContext> options): base(options) { }

    public virtual DbSet<CategoriaVModel> CategoriaVs { get; set; }
    public virtual DbSet<TipoVModel> TipoVs { get; set; }
    public virtual DbSet<VehiculoModel> Vehiculos { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql("server=erxv1bzckceve5lh.cbetxkdyhwsb.us-east-1.rds.amazonaws.com;database=ld9orcu5leieosgc;user=bsmy9skd1sg22xl2;password=bm8q0wla0wj30r34", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));
        //warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.

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
            entity
                .HasNoKey()
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
            modelBuilder.Entity<TipoVModel>().HasNoKey();

        });

        modelBuilder.Entity<VehiculoModel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Vehiculo");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
