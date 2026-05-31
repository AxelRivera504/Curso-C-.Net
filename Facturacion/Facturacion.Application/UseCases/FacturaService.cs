using Facturacion.Application.Dtos.Factura;
using Facturacion.Application.Extensions;
using Facturacion.Application.Interfaces;
using Facturacion.Domain.Interfaces;

namespace Facturacion.Application.UseCases
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;
        public FacturaService(IFacturaRepository facturaRepository)
        {
            _facturaRepository = facturaRepository;
        }

        public async Task<List<FacturaDto>> GetAllAsync()
        {
            var facturas = await _facturaRepository.GetAllAsync();
            return facturas.Select(f => f.ToDto()).ToList();
        }

        public async Task<FacturaDto> GetByIdAsync(int id)
        {
            var factura = await _facturaRepository.GetByIdAsync(id);
            return factura.ToDto();
        }

        public async Task<List<FacturaDto>> GetByClienteIdAsync(int clienteId)
        {
            var facturas = await _facturaRepository.GetByClienteIdAsync(clienteId);
            return facturas.Select(f => f.ToDto()).ToList();
        }

        public async Task<List<FacturaDto>> GetByEstadoAsync(string estado)
        {
            var facturas = await _facturaRepository.GetByEstadoAsync(estado);
            return facturas.Select(f => f.ToDto()).ToList();
        }

        public async Task<FacturaDto> CreateAsync(CreateFacturaDto facturaDto)
        {
            var factura = await _facturaRepository.CreateAsync(facturaDto.ToEntity());
            return factura.ToDto();
        }

        public async Task<FacturaDto> UpdateAsync(int id, UpdateFacturaDto facturaDto)
        {
            var factura = await _facturaRepository.UpdateAsync(id, facturaDto.ToEntity());
            return factura.ToDto();
        }

        public async Task<FacturaDto> UpdateEstadoAsync(int id, string estado)
        {
            var factura = await _facturaRepository.UpdateEstadoAsync(id, estado);
            return factura.ToDto();
        }

        public async Task DeleteAsync(int id)
        {
            await _facturaRepository.DeleteAsync(id);
        }
    }
}
