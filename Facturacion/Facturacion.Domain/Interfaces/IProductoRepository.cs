using Facturacion.Domain.Entities;

namespace Facturacion.Domain.Interfaces
{
    public interface IProductoRepository
    {
        Task<List<Producto>> GetAllAsync();
        Task<Producto> GetByIdAsync(int id);
        Task<List<Producto>> SearchAsync(string nombre);
        Task<Producto> CreateAsync(Producto producto);
        Task<Producto> UpdateAsync(int id, Producto producto);
        Task DeleteAsync(int id);
    }
}
