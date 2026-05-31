using Facturacion.Application.Dtos.Producto;

namespace Facturacion.Application.Interfaces
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> GetAllAsync();
        Task<ProductoDto> GetByIdAsync(int id);
        Task<List<ProductoDto>> SearchAsync(string nombre);
        Task<ProductoDto> CreateAsync(CreateProductoDto productoDto);
        Task<ProductoDto> UpdateAsync(int id, UpdateProductoDto productoDto);
        Task DeleteAsync(int id);
    }
}
