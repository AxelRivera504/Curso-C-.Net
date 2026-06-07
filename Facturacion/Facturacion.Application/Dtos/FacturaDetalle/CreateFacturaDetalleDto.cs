namespace Facturacion.Application.Dtos.FacturaDetalle
{
    public class CreateFacturaDetalleDto
    {
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
