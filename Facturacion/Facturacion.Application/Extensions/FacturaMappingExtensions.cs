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
        };

        public static Factura ToEntity(this CreateFacturaDto f) => new Factura()
        {
            ClienteId = f.ClienteId,
            FechaFactura = f.FechaFactura,
            Total = f.Total,
        };

        public static Factura ToEntity(this UpdateFacturaDto f) => new Factura()
        {
            ClienteId = f.ClienteId,
            FechaFactura = f.FechaFactura,
            Total = f.Total,
            Estado = f.Estado,
        };
    }
}
