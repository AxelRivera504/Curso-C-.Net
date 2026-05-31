using Facturacion.Application.Dtos.Cliente;
using Facturacion.Application.Extensions;
using Facturacion.Application.Interfaces;
using Facturacion.Application.Validators.Cliente;
using Facturacion.Domain.Interfaces;
using FluentValidation;

namespace Facturacion.Application.UseCases
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<List<ClienteDto>> GetAllAsync()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            return clientes.Select(c => c.ToDto()).ToList();
        }

        public async Task<ClienteDto?> GetByIdAsync(int id)
        {
            var cliente = await _clienteRepository.GetByIdAsync(id);
            return cliente.ToDto();
        }

        public async Task<List<ClienteDto>> GetActivosAsync()
        {
            var clientes = await _clienteRepository.GetActivosAsync();
            return clientes.Select(c => c.ToDto()).ToList();
        }

        public async Task<List<ClienteDto>> SearchAsync(string nombre)
        {
            var clientes = await _clienteRepository.SearchAsync(nombre);
            return clientes.Select(c => c.ToDto()).ToList();
        }


        public async Task CreateAsync(CreateClienteDto clienteDto)
        {
            var validator = new CreateClienteValidatorDto();
            var result = validator.Validate(clienteDto);
            if (!result.IsValid)
                throw new Exception(string.Join(",", result.Errors.Select(e => e.ErrorMessage)));

            await _clienteRepository.CreateAsync(clienteDto.ToEntity());
        }

        public async Task UpdateAsync(int id, UpdateClienteDto clienteDto)
        {
            var validator = new UpdateClienteValidatorDto();
            var result = validator.Validate(clienteDto);
            if (!result.IsValid)
                throw new Exception(string.Join(",", result.Errors.Select(e => e.ErrorMessage)));

            var cliente = await _clienteRepository.GetByIdAsync(id);
            if (cliente is null)
                throw new ArgumentNullException("Cliente no encontrado para actualizar");

            await _clienteRepository.UpdateAsync(id ,clienteDto.ToEntity());
        }

        public async Task DeleteAsync(int id)
        {
            await _clienteRepository.DeleteAsync(id);
        }

        public async Task ToggleStatus(int id)
        {
            await _clienteRepository.ToggleStatus(id);
        }
    }
}
