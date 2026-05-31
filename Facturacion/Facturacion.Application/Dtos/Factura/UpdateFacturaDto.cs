namespace Facturacion.Application.Dtos.Factura
{
    public class UpdateFacturaDto
    {
        public int ClienteId { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
    }
}
