using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GestionClientes.Models
{
    public partial class controlGlobalContext : DbContext
    {
        public controlGlobalContext()
        {
        }

        public controlGlobalContext(DbContextOptions<controlGlobalContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<CuentaCorriente> CuentaCorrientes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
         // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
         //       optionsBuilder.UseSqlServer("server=DESKTOP-6KF5SL9\\SQLEXPRESS; database=controlGlobal; integrated security=true; TrustServerCertificate=Yes");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.HasIndex(e => e.Identificacion, "UQ__Cliente__D6F931E5E17F3A0F")
                    .IsUnique();

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Identificacion)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Saldo).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<CuentaCorriente>(entity =>
            {
                entity.HasKey(e => e.MovimientoId)
                    .HasName("PK__CuentaCo__BF923FCCC2B43E7C");

                entity.ToTable("CuentaCorriente");

                entity.Property(e => e.MovimientoId).HasColumnName("MovimientoID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Descripcion).HasColumnType("text");

                entity.Property(e => e.FhMovimiento).HasColumnType("datetime");

                entity.Property(e => e.Importe).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.CuentaCorrientes)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__CuentaCor__Clien__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
