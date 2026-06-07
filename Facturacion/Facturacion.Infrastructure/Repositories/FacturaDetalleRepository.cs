using Facturacion.Domain.Entities;
using Facturacion.Domain.Interfaces;
using Facturacion.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Infrastructure.Repositories
{
    public class FacturaDetalleRepository : IFacturaDetalleRepository
    {
        private readonly FacturacionContext _context;

        public FacturaDetalleRepository(FacturacionContext context)
        {
            _context = context;
        }

        public async Task<List<FacturaDetalle>> GetByFacturaIdAsync(int facturaId)
            => await _context.FacturaDetalles
                .Include(d => d.Producto)
                .Where(d => d.FacturaId == facturaId)
                .ToListAsync();

        public async Task<FacturaDetalle?> GetByIdAsync(int id)
            => await _context.FacturaDetalles
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(d => d.Id == id);

        public async Task<FacturaDetalle> CreateAsync(FacturaDetalle detalle)
        {
            await _context.FacturaDetalles.AddAsync(detalle);
            await _context.SaveChangesAsync();
            return detalle;
        }

        public async Task<FacturaDetalle> UpdateAsync(int id, FacturaDetalle detalle)
        {
            var detalleExistente = await GetByIdAsync(id);
            if (detalleExistente is not null)
            {
                detalleExistente.ProductoId = detalle.ProductoId;
                detalleExistente.Cantidad = detalle.Cantidad;
                detalleExistente.PrecioUnitario = detalle.PrecioUnitario;
                detalleExistente.Subtotal = detalle.Subtotal;
                await _context.SaveChangesAsync();
            }
            return detalleExistente;
        }

        public async Task DeleteAsync(int id)
        {
            var detalle = await GetByIdAsync(id);
            if (detalle is not null)
            {
                _context.FacturaDetalles.Remove(detalle);
                await _context.SaveChangesAsync();
            }
        }
    }
}
