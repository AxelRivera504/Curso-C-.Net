using Facturacion.Application.Dtos.FacturaDetalle;

namespace Facturacion.Application.Dtos.Factura
{
    public class CreateFacturaDto
    {
        public int ClienteId { get; set; }
        public DateTime FechaFactura { get; set; }
        public List<CreateFacturaDetalleDto> Detalles { get; set; } = new();
    }
}
