using Facturacion.Domain.Entities;

namespace Facturacion.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<List<Cliente>> GetActivosAsync();
        Task<List<Cliente>> SearchAsync(string nombre);
        Task CreateAsync(Cliente cliente);
        Task UpdateAsync(int id, Cliente cliente);
        Task ToggleStatus(int id);
        Task DeleteAsync(int id);
    }
}
