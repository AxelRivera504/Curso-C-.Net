using Facturacion.Application.Dtos.FacturaDetalle;

namespace Facturacion.Application.Interfaces
{
    public interface IFacturaDetalleService
    {
        Task<List<FacturaDetalleDto>> GetByFacturaIdAsync(int facturaId);
        Task<FacturaDetalleDto> GetByIdAsync(int id);
        Task<FacturaDetalleDto> CreateAsync(int facturaId, CreateFacturaDetalleDto dto);
        Task<FacturaDetalleDto> UpdateAsync(int id, UpdateFacturaDetalleDto dto);
        Task DeleteAsync(int id);
    }
}
