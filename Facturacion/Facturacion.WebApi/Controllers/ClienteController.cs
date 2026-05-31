using Facturacion.Application.Dtos.Cliente;
using Facturacion.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        => Ok(await _clienteService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        => Ok(await _clienteService.GetByIdAsync(id));

        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string nombre)
        => Ok(await _clienteService.SearchAsync(nombre));

        [HttpGet("activos")]
        public async Task<IActionResult> GetActivos()
        => Ok(await _clienteService.GetActivosAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClienteDto createClienteDto)
        {
            await _clienteService.CreateAsync(createClienteDto);
            return StatusCode(201);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateClienteDto updateClienteDto)
        {
            await _clienteService.UpdateAsync(updateClienteDto.Id, updateClienteDto);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ToggleActive(int id)
        {
            await _clienteService.ToggleStatus(id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        { 
            await _clienteService.DeleteAsync(id);
            return NoContent();
        }
    }
}
