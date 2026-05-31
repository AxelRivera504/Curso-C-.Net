using Facturacion.Application.Dtos.Producto;
using Facturacion.Domain.Entities;

namespace Facturacion.Application.Extensions
{
    public static class ProductoMappingExtensions
    {
        public static ProductoDto ToDto(this Producto p) => new ProductoDto()
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Categoria = p.Categoria,
            Precio = p.Precio,
            Stock = p.Stock,
        };

        public static Producto ToEntity(this CreateProductoDto p) => new Producto()
        {
            Nombre = p.Nombre,
            Categoria = p.Categoria,
            Precio = p.Precio,
            Stock = p.Stock,
        };

        public static Producto ToEntity(this UpdateProductoDto p) => new Producto()
        {
            Nombre = p.Nombre,
            Categoria = p.Categoria,
            Precio = p.Precio,
            Stock = p.Stock,
        };
    }
}
