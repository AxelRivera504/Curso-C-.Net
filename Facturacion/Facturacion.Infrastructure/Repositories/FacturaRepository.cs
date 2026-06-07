using Facturacion.Domain.Entities;
using Facturacion.Domain.Interfaces;
using Facturacion.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Infrastructure.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly FacturacionContext _context;

        public FacturaRepository(FacturacionContext context)
        {
            _context = context;
        }

        public async Task<List<Factura>> GetAllAsync()
            => await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Detalles).ThenInclude(d => d.Producto)
                .ToListAsync();

        public async Task<Factura> GetByIdAsync(int id)
            => await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Detalles).ThenInclude(d => d.Producto)
                .FirstOrDefaultAsync(f => f.Id == id);

        public async Task<List<Factura>> GetByClienteIdAsync(int clienteId)
            => await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Detalles).ThenInclude(d => d.Producto)
                .Where(f => f.ClienteId == clienteId)
                .ToListAsync();

        public async Task<List<Factura>> GetByEstadoAsync(string estado)
            => await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.Detalles).ThenInclude(d => d.Producto)
                .Where(f => f.Estado.ToLower() == estado.ToLower())
                .ToListAsync();

        public async Task<Factura> CreateAsync(Factura factura)
        {
            await _context.Facturas.AddAsync(factura);
            await _context.SaveChangesAsync();
            return factura;
        }

        public async Task<Factura> UpdateAsync(int id, Factura factura)
        {
            var facturaExistente = await GetByIdAsync(id);
            if (facturaExistente is not null)
            {
                facturaExistente.ClienteId = factura.ClienteId;
                facturaExistente.FechaFactura = factura.FechaFactura;
                facturaExistente.Estado = factura.Estado;
                await _context.SaveChangesAsync();
            }
            return facturaExistente;
        }

        public async Task<Factura> UpdateEstadoAsync(int id, string estado)
        {
            var factura = await GetByIdAsync(id);
            if (factura is not null)
            {
                factura.Estado = estado;
                await _context.SaveChangesAsync();
            }
            return factura;
        }

        public async Task RecalcularTotalAsync(int id, decimal total)
        {
            var factura = await _context.Facturas.FirstOrDefaultAsync(f => f.Id == id);
            if (factura is not null)
            {
                factura.Total = total;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var factura = await GetByIdAsync(id);
            if (factura is not null)
            {
                _context.Facturas.Remove(factura);
                await _context.SaveChangesAsync();
            }
        }
    }
}
