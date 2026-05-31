namespace Facturacion.Domain.Entities
{
    public class Factura
    {
        public int Id { get; set; }
        //FK hacia la tabla de clientes
        public int ClienteId { get; set; }
        //propiedad de navegación - EF Core la usa para los joins
        public Cliente Cliente { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = "pendiente"; // Pendiente, Pagada, Anulada
    }
}
