using Facturacion.Domain.Entities;

namespace Facturacion.Domain.Interfaces
{
    public interface IFacturaRepository
    {
        Task<List<Factura>> GetAllAsync();
        Task<Factura> GetByIdAsync(int id);
        Task<List<Factura>> GetByClienteIdAsync(int clienteId);
        Task<List<Factura>> GetByEstadoAsync(string estado);
        Task<Factura> CreateAsync(Factura factura);
        Task<Factura> UpdateAsync(int id, Factura factura);
        Task<Factura> UpdateEstadoAsync(int id, string estado);
        Task DeleteAsync(int id);
    }
}
