using System;
using System.Collections.Generic;
using api_dotNet_vehicles.Models;
using API_VehiclesAPP.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_VehiclesAPP.Data
{
    public partial class DBContext : DbContext
    {
        public DBContext() { }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        //public virtual DbSet<CategoriaVModel> CategoriaVs { get; set; }
        //public virtual DbSet<TipoVModel> TipoVs { get; set; }
        //public virtual DbSet<BikesDetModel> BikesDet { get; set; }
        //public virtual DbSet<CarsDetModel> CarsDet { get; set; }
        //public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

                entity.HasIndex(e => e.Username, "UQ_Users_Username").IsUnique();

                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
                entity.Property(e => e.Email).HasMaxLength(150);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.PasswordHash).HasMaxLength(255);
                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasDefaultValue("User");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasDefaultValue(null);
            });


            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.ClienteId).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Apellido).HasMaxLength(100);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Rut).HasMaxLength(20);
                entity.Property(e => e.Telefono).HasMaxLength(20);
            });



            OnModelCreatingPartial(modelBuilder);

        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
