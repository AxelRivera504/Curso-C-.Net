using Facturacion.Application.Dtos.Factura;

namespace Facturacion.Application.Interfaces
{
    public interface IFacturaService
    {
        Task<List<FacturaDto>> GetAllAsync();
        Task<FacturaDto> GetByIdAsync(int id);
        Task<List<FacturaDto>> GetByClienteIdAsync(int clienteId);
        Task<List<FacturaDto>> GetByEstadoAsync(string estado);
        Task<FacturaDto> CreateAsync(CreateFacturaDto facturaDto);
        Task<FacturaDto> UpdateAsync(int id, UpdateFacturaDto facturaDto);
        Task UpdateEstadoAsync(int id, string estado);
        Task DeleteAsync(int id);
    }
}
