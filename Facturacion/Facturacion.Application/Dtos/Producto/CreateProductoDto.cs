namespace Facturacion.Application.Dtos.Producto
{
    public class CreateProductoDto
    {
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
