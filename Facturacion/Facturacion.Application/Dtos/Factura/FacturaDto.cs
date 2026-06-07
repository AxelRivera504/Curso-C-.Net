using Facturacion.Application.Dtos.FacturaDetalle;

namespace Facturacion.Application.Dtos.Factura
{
    public class FacturaDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNombre { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public List<FacturaDetalleDto> Detalles { get; set; } = new();
    }
}
