namespace Facturacion.Application.Dtos.Factura
{
    public class UpdateFacturaDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaFactura { get; set; }
        public string Estado { get; set; }
    }
}
