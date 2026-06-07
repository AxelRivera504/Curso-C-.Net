using Facturacion.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Infrastructure.Context
{
    public class FacturacionContext : DbContext
    {
        public FacturacionContext(DbContextOptions<FacturacionContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factura>(entity =>
            {
                entity.ToTable("Facturas");
                entity.Property(x => x.Total).HasColumnType("decimal(18,2)");
                entity.Property(x => x.Estado).HasMaxLength(20).HasDefaultValue("pendiente");

                entity.HasOne(x => x.Cliente)
                    .WithMany()
                    .HasForeignKey(x => x.ClienteId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FacturaDetalle>(entity =>
            {
                entity.ToTable("FacturaDetalles");
                entity.Property(x => x.PrecioUnitario).HasColumnType("decimal(18,2)");
                entity.Property(x => x.Subtotal).HasColumnType("decimal(18,2)");

                entity.HasOne(x => x.Factura)
                    .WithMany(f => f.Detalles)
                    .HasForeignKey(x => x.FacturaId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Producto)
                    .WithMany()
                    .HasForeignKey(x => x.ProductoId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
