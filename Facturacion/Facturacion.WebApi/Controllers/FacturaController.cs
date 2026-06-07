using Facturacion.Application.Dtos.Factura;
using Facturacion.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IFacturaService _facturaService;

        public FacturaController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _facturaService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _facturaService.GetByIdAsync(id));

        [HttpGet("cliente/{clienteId}")]
        public async Task<IActionResult> GetByCliente(int clienteId)
            => Ok(await _facturaService.GetByClienteIdAsync(clienteId));

        [HttpGet("estado/{estado}")]
        public async Task<IActionResult> GetByEstado(string estado)
            => Ok(await _facturaService.GetByEstadoAsync(estado));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFacturaDto createFacturaDto)
        {
            var factura = await _facturaService.CreateAsync(createFacturaDto);
            return StatusCode(201, factura);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFacturaDto updateFacturaDto)
        {
            await _facturaService.UpdateAsync(updateFacturaDto.Id, updateFacturaDto);
            return NoContent();
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> UpdateEstado(int id, [FromQuery] string estado)
        {
            await _facturaService.UpdateEstadoAsync(id, estado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _facturaService.DeleteAsync(id);
            return NoContent();
        }
    }
}
