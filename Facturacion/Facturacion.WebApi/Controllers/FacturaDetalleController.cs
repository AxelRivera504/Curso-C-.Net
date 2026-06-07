using Facturacion.Application.Dtos.FacturaDetalle;
using Facturacion.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Facturacion.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaDetalleController : ControllerBase
    {
        private readonly IFacturaDetalleService _facturaDetalleService;

        public FacturaDetalleController(IFacturaDetalleService facturaDetalleService)
        {
            _facturaDetalleService = facturaDetalleService;
        }

        [HttpGet("factura/{facturaId}")]
        public async Task<IActionResult> GetByFactura(int facturaId)
            => Ok(await _facturaDetalleService.GetByFacturaIdAsync(facturaId));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _facturaDetalleService.GetByIdAsync(id));

        [HttpPost("{facturaId}")]
        public async Task<IActionResult> Create(int facturaId, [FromBody] CreateFacturaDetalleDto createDetalleDto)
        {
            var detalle = await _facturaDetalleService.CreateAsync(facturaId, createDetalleDto);
            return StatusCode(201, detalle);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateFacturaDetalleDto updateDetalleDto)
        {
            await _facturaDetalleService.UpdateAsync(updateDetalleDto.Id, updateDetalleDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _facturaDetalleService.DeleteAsync(id);
            return NoContent();
        }
    }
}
