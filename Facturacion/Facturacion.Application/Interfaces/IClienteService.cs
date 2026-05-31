using Facturacion.Application.Dtos.Cliente;

namespace Facturacion.Application.Interfaces
{
    //Contrato, que nos dice el Que podemos hacer en el service.
    public interface IClienteService
    {
        Task<List<ClienteDto>> GetAllAsync();
        Task<ClienteDto?> GetByIdAsync(int id);
        Task<List<ClienteDto>> GetActivosAsync();
        Task<List<ClienteDto>> SearchAsync(string nombre);
        Task CreateAsync(CreateClienteDto clienteDto);
        Task UpdateAsync(int id, UpdateClienteDto clienteDto);
        Task ToggleStatus(int id);
        Task DeleteAsync(int id);
    }
}
