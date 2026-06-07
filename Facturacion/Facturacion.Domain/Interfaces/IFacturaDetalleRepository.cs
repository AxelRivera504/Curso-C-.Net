using Facturacion.Domain.Entities;

namespace Facturacion.Domain.Interfaces
{
    public interface IFacturaDetalleRepository
    {
        Task<List<FacturaDetalle>> GetByFacturaIdAsync(int facturaId);
        Task<FacturaDetalle?> GetByIdAsync(int id);
        Task<FacturaDetalle> CreateAsync(FacturaDetalle detalle);
        Task<FacturaDetalle> UpdateAsync(int id, FacturaDetalle detalle);
        Task DeleteAsync(int id);
    }
}
