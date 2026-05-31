using Facturacion.Domain.Entities;
using Facturacion.Domain.Interfaces;
using Facturacion.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly FacturacionContext _facturacionContext;

        public ClienteRepository(FacturacionContext facturacionContext)
        {
            _facturacionContext = facturacionContext;
        }

        public async Task CreateAsync(Cliente cliente)
        {
            await _facturacionContext.Clientes.AddAsync(cliente);
            await _facturacionContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cliente = await GetByIdAsync(id);
            if (cliente is not null)
            {
                _facturacionContext.Clientes.Remove(cliente);
                await _facturacionContext.SaveChangesAsync();
            }
        }

        public async Task<List<Cliente>> GetActivosAsync()
        => await _facturacionContext.Clientes.Where(c => c.Activo).ToListAsync();

        public async Task<List<Cliente>> GetAllAsync()
            => await _facturacionContext.Clientes.ToListAsync();

        public async Task<Cliente?> GetByIdAsync(int id)
        => await _facturacionContext.Clientes.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<List<Cliente>> SearchAsync(string nombre)
        => await _facturacionContext.Clientes.Where(c => c.Nombre.Contains(nombre)).ToListAsync();

        public async Task ToggleStatus(int id)
        {
            var cliente = await GetByIdAsync(id);
            if (cliente is not null)
            {
                cliente.Activo = !cliente.Activo;
                _facturacionContext.Clientes.Update(cliente);
                await _facturacionContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Cliente cliente)
        {
            var clienteBuscado = await GetByIdAsync(id);
            if (clienteBuscado is not null)
            {
                clienteBuscado.Nombre = cliente.Nombre;
                clienteBuscado.Telefono = cliente.Telefono;
                clienteBuscado.Email = cliente.Email;
                await _facturacionContext.SaveChangesAsync();
            }
        }
    }
}
