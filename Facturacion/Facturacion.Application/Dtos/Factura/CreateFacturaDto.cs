namespace Facturacion.Application.Dtos.Factura
{
    public class CreateFacturaDto
    {
        public int ClienteId { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal Total { get; set; }
    }
}
