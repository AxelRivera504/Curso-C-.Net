using Facturacion.Application.Dtos.Factura;
using Facturacion.Domain.Entities;

namespace Facturacion.Application.Extensions
{
    public static class FacturaMappingExtensions
    {
        public static FacturaDto ToDto(this Factura f) => new FacturaDto()
        {
            Id = f.Id,
            ClienteId = f.ClienteId,
            ClienteNombre = f.Cliente?.Nombre,
            FechaFactura = f.FechaFactura,
            Total = f.Total,
            Estado = f.Estado,
            Detalles = f.Detalles?.Select(d => d.ToDto()).ToList() ?? new(),
        };

        public static Factura ToEntity(this CreateFacturaDto f) => new Factura()
        {
            ClienteId = f.ClienteId,
            FechaFactura = f.FechaFactura,
            Detalles = f.Detalles.Select(d => d.ToEntity()).ToList(),
        };

        public static Factura ToEntity(this UpdateFacturaDto f) => new Factura()
        {
            ClienteId = f.ClienteId,
            FechaFactura = f.FechaFactura,
            Estado = f.Estado,
        };
    }
}
