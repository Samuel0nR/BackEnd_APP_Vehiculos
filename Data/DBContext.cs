using System;
using System.Collections.Generic;
using API_VehiclesAPP.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_VehiclesAPP.Data
{
    public partial class DBContext : DbContext
    {
        public DBContext() { }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }


        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Marca> Marcas { get; set; }
        public virtual DbSet<AutosDet> AutosDets { get; set; }

        public virtual DbSet<BicicletasDet> BicicletasDets { get; set; }

        public virtual DbSet<Modelo> Modelos { get; set; }

        public virtual DbSet<MotosDet> MotosDets { get; set; }

        public virtual DbSet<SubTipoVehiculo> SubTipoVehiculos { get; set; }

        public virtual DbSet<TipoVehiculo> TipoVehiculos { get; set; }

        public virtual DbSet<UserVehicle> UserVehicles { get; set; }

        public virtual DbSet<Vehiculo> Vehiculos { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AI");


            modelBuilder.Entity<AutosDet>(entity =>
            {
                entity.HasKey(e => e.VehicleId).HasName("PK__AutosDet__476B5492F0F84515");

                entity.ToTable("AutosDet");

                entity.Property(e => e.VehicleId).ValueGeneratedNever();
                entity.Property(e => e.Combustible)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Frenos)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.Llanta)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.Motor)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Transmision)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.Vehicle).WithOne(p => p.AutosDet)
                    .HasForeignKey<AutosDet>(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AutosDet_Vehiculos");
            });

            modelBuilder.Entity<BicicletasDet>(entity =>
            {
                entity.HasKey(e => e.VehicleId).HasName("PK_Biciletas");

                entity.ToTable("BicicletasDet");

                entity.Property(e => e.VehicleId).ValueGeneratedNever();
                entity.Property(e => e.Aro)
                    .HasMaxLength(5)
                    .IsUnicode(false);
                entity.Property(e => e.Cambios).HasDefaultValue(false);
                entity.Property(e => e.Frenos)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.MaterialMarco)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Suspension)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.TipoBicicleta)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Vehicle).WithOne(p => p.BicicletasDet)
                    .HasForeignKey<BicicletasDet>(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BicisDet_Vehiculos");
            });
            
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.ClienteId).HasName("PK__Clients__71ABD087693568FB");

                entity.Property(e => e.ClienteId).ValueGeneratedNever();
                entity.Property(e => e.Apellido).HasMaxLength(100);
                entity.Property(e => e.Direccion).HasMaxLength(100);
                entity.Property(e => e.Nombre).HasMaxLength(100);
                entity.Property(e => e.Rut).HasMaxLength(20);
                entity.Property(e => e.Telefono).HasMaxLength(20);

                entity.HasOne(d => d.User)
                      .WithOne(p => p.Clients)
                      .HasForeignKey<Client>(d => d.UserId)
                      .OnDelete(DeleteBehavior.ClientSetNull)
                      .HasConstraintName("FK_Clients_User");

                entity.HasIndex(d => d.UserId)
                      .IsUnique();

            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.MarcaId).HasName("PK__Marcas__D5B1CD8BF9818C9E");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Modelo>(entity =>
            {
                entity.HasKey(e => e.ModeloId).HasName("PK__Modelos__FA60529A3704A977");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.TipoVehiculoNavigation).WithMany(p => p.Modelos)
                    .HasForeignKey(d => d.TipoVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TipoVehiculo_Modelo");
            });

            modelBuilder.Entity<MotosDet>(entity =>
            {
                entity.HasKey(e => e.VehicleId);

                entity.ToTable("MotosDet");

                entity.Property(e => e.VehicleId).ValueGeneratedNever();
                entity.Property(e => e.Frenos)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.TipoEnfriamiento)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.Transmision)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Vehicle).WithOne(p => p.MotosDet)
                    .HasForeignKey<MotosDet>(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MotosDet_Vehiculos");
            });

            modelBuilder.Entity<SubTipoVehiculo>(entity =>
            {
                entity.HasKey(e => e.SubTipoId).HasName("PK__SubTipoV__3B583252343D2680");

                entity.ToTable("SubTipoVehiculo");

                entity.Property(e => e.SubTipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Tipo).WithMany(p => p.SubTipoVehiculos)
                    .HasForeignKey(d => d.TipoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tipo_SubTipo");
            });

            modelBuilder.Entity<TipoVehiculo>(entity =>
            {
                entity.HasKey(e => e.TipoId).HasName("PK__TipoVehi__97099EB78E8D7074");

                entity.ToTable("TipoVehiculo");

                entity.Property(e => e.Tipo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ_Users_Email").IsUnique();

                entity.HasIndex(e => e.Username).IsUnique();

                entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
                entity.Property(e => e.Email).HasMaxLength(150);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.PasswordHash).HasMaxLength(255);
                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasDefaultValue("User");
                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<UserVehicle>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.VehicleId });

                entity.Property(e => e.AliasVehiculo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Vehicle).WithMany(p => p.UserVehicles)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vehicle_UserVehicle");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.VehicleId).HasName("PK__Vehiculo__476B5492C71BB634");

                entity.HasIndex(e => e.Patente, "UQ_Vehiculos_Patente").IsUnique();

                entity.Property(e => e.VehicleId).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Color)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.Patente)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Modelo).WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.ModeloId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Modelo_Vehiculo");

                entity.HasOne(d => d.SubTipo).WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.SubTipoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubTipo_Vehiculo");

                entity.HasOne(d => d.Tipo).WithMany(p => p.Vehiculos)
                    .HasForeignKey(d => d.TipoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tipo_Vehiculo");
            });


            OnModelCreatingPartial(modelBuilder);

        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
