using Facturacion.Application.Dtos.FacturaDetalle;
using Facturacion.Application.Extensions;
using Facturacion.Application.Interfaces;
using Facturacion.Domain.Interfaces;

namespace Facturacion.Application.UseCases
{
    public class FacturaDetalleService : IFacturaDetalleService
    {
        private readonly IFacturaDetalleRepository _detalleRepository;
        private readonly IFacturaRepository _facturaRepository;

        public FacturaDetalleService(IFacturaDetalleRepository detalleRepository, IFacturaRepository facturaRepository)
        {
            _detalleRepository = detalleRepository;
            _facturaRepository = facturaRepository;
        }

        public async Task<List<FacturaDetalleDto>> GetByFacturaIdAsync(int facturaId)
        {
            var detalles = await _detalleRepository.GetByFacturaIdAsync(facturaId);
            return detalles.Select(d => d.ToDto()).ToList();
        }

        public async Task<FacturaDetalleDto> GetByIdAsync(int id)
        {
            var detalle = await _detalleRepository.GetByIdAsync(id);
            return detalle.ToDto();
        }

        public async Task<FacturaDetalleDto> CreateAsync(int facturaId, CreateFacturaDetalleDto dto)
        {
            var detalle = dto.ToEntity();
            detalle.FacturaId = facturaId;
            var creado = await _detalleRepository.CreateAsync(detalle);
            await RecalcularTotalAsync(facturaId);
            return creado.ToDto();
        }

        public async Task<FacturaDetalleDto> UpdateAsync(int id, UpdateFacturaDetalleDto dto)
        {
            var detalleExistente = await _detalleRepository.GetByIdAsync(id);
            var actualizado = await _detalleRepository.UpdateAsync(id, dto.ToEntity());
            await RecalcularTotalAsync(detalleExistente.FacturaId);
            return actualizado.ToDto();
        }

        public async Task DeleteAsync(int id)
        {
            var detalle = await _detalleRepository.GetByIdAsync(id);
            if (detalle is null) return;
            int facturaId = detalle.FacturaId;
            await _detalleRepository.DeleteAsync(id);
            await RecalcularTotalAsync(facturaId);
        }

        private async Task RecalcularTotalAsync(int facturaId)
        {
            var detalles = await _detalleRepository.GetByFacturaIdAsync(facturaId);
            var total = detalles.Sum(d => d.Subtotal);
            await _facturaRepository.RecalcularTotalAsync(facturaId, total);
        }
    }
}
