using Facturacion.Application.Dtos.FacturaDetalle;
using Facturacion.Domain.Entities;

namespace Facturacion.Application.Extensions
{
    public static class FacturaDetalleMappingExtensions
    {
        public static FacturaDetalleDto ToDto(this FacturaDetalle d) => new FacturaDetalleDto()
        {
            Id = d.Id,
            FacturaId = d.FacturaId,
            ProductoId = d.ProductoId,
            ProductoNombre = d.Producto?.Nombre,
            Cantidad = d.Cantidad,
            PrecioUnitario = d.PrecioUnitario,
            Subtotal = d.Subtotal,
        };

        public static FacturaDetalle ToEntity(this CreateFacturaDetalleDto d) => new FacturaDetalle()
        {
            ProductoId = d.ProductoId,
            Cantidad = d.Cantidad,
            PrecioUnitario = d.PrecioUnitario,
            Subtotal = d.Cantidad * d.PrecioUnitario,
        };

        public static FacturaDetalle ToEntity(this UpdateFacturaDetalleDto d) => new FacturaDetalle()
        {
            ProductoId = d.ProductoId,
            Cantidad = d.Cantidad,
            PrecioUnitario = d.PrecioUnitario,
            Subtotal = d.Cantidad * d.PrecioUnitario,
        };
    }
}
