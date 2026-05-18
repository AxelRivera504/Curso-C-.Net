using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimeraApi.Infrastructure;
using MiPrimeraApi.Models;

namespace MiPrimeraApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FacturaController : ControllerBase
{
    private readonly FacturacionContext _facturacionContext;

    public FacturaController(FacturacionContext facturacionContext)
    {
        _facturacionContext = facturacionContext;
    }

    // ──────────────────────────────────────────────────────
    // GET api/factura
    // Devuelve todas las facturas con los datos del cliente
    // ──────────────────────────────────────────────────────
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        // Include() hace el JOIN con la tabla Clientes
        // SELECT * FROM Facturas f INNER JOIN Clientes c ON f.ClienteId = c.Id
        List<Factura> facturas = await _facturacionContext.Facturas
            .Include(f => f.Cliente)
            .ToListAsync();

        return Ok(facturas);
    }

    // ──────────────────────────────────────────────────────
    // GET api/factura/2
    // Devuelve una factura por ID con datos del cliente
    // ──────────────────────────────────────────────────────
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        Factura? factura = await _facturacionContext.Facturas
            .Include(f => f.Cliente)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (factura is null)
            return NotFound(new { mensaje = $"Factura con ID {id} no encontrada." });

        return Ok(factura);
    }

    // ──────────────────────────────────────────────────────
    // GET api/factura/cliente/1
    // Facturas de un cliente específico
    // ──────────────────────────────────────────────────────
    [HttpGet("cliente/{clienteId}")]
    public async Task<IActionResult> GetByCliente(int clienteId)
    {
        // Primero verificar que el cliente existe
        bool clienteExiste = await _facturacionContext.Clientes
            .AnyAsync(c => c.Id == clienteId);

        if (!clienteExiste)
            return NotFound(new { mensaje = $"El cliente con ID {clienteId} no existe." });

        List<Factura> facturas = await _facturacionContext.Facturas
            .Where(f => f.ClienteId == clienteId)
            .Include(f => f.Cliente)
            .ToListAsync();

        return Ok(facturas);
    }

    // ──────────────────────────────────────────────────────
    // GET api/factura/estado/pendiente
    // Facturas por estado: pendiente, pagada, anulada
    // ──────────────────────────────────────────────────────
    [HttpGet("estado/{estado}")]
    public async Task<IActionResult> GetByEstado(string estado)
    {
        string[] estadosValidos = { "pendiente", "pagada", "anulada" };

        if (!estadosValidos.Contains(estado.ToLower()))
            return BadRequest(new { mensaje = "Estado inválido. Use: pendiente, pagada o anulada." });

        List<Factura> facturas = await _facturacionContext.Facturas
            .Where(f => f.Estado == estado.ToLower())
            .Include(f => f.Cliente)
            .ToListAsync();

        return Ok(facturas);
    }

    // ──────────────────────────────────────────────────────
    // POST api/factura
    // Crear una nueva factura
    // ──────────────────────────────────────────────────────
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Factura factura)
    {
        if (factura is null)
            return BadRequest(new { mensaje = "El cuerpo de la petición no puede estar vacío." });

        if (factura.ClienteId <= 0)
            return BadRequest(new { mensaje = "El ClienteId es requerido y debe ser mayor a cero." });

        if (factura.Total <= 0)
            return BadRequest(new { mensaje = "El total de la factura debe ser mayor a cero." });

        // Verificar que el cliente existe
        Cliente? cliente = await _facturacionContext.Clientes
            .FirstOrDefaultAsync(c => c.Id == factura.ClienteId);

        if (cliente is null)
            return NotFound(new { mensaje = $"El cliente con ID {factura.ClienteId} no existe." });

        // Verificar que el cliente está activo
        if (!cliente.Activo)
            return BadRequest(new { mensaje = $"El cliente '{cliente.Nombre}' está inactivo. No se puede facturar." });

        factura.FechaFactura = DateTime.Now;
        factura.Estado = "pendiente";

        await _facturacionContext.Facturas.AddAsync(factura);
        await _facturacionContext.SaveChangesAsync();

        return StatusCode(201, factura);
    }

    // ──────────────────────────────────────────────────────
    // PATCH api/factura/2/pagar
    // Marcar factura como pagada
    // ──────────────────────────────────────────────────────
    [HttpPatch("{id}/pagar")]
    public async Task<IActionResult> Pagar(int id)
    {
        Factura? factura = await _facturacionContext.Facturas
            .FirstOrDefaultAsync(f => f.Id == id);

        if (factura is null)
            return NotFound(new { mensaje = $"Factura con ID {id} no encontrada." });

        if (factura.Estado == "pagada")
            return BadRequest(new { mensaje = "Esta factura ya fue pagada." });

        if (factura.Estado == "anulada")
            return BadRequest(new { mensaje = "No se puede pagar una factura anulada." });

        factura.Estado = "pagada";

        await _facturacionContext.SaveChangesAsync();

        return Ok(new { mensaje = "Factura marcada como pagada correctamente.", factura });
    }

    // ──────────────────────────────────────────────────────
    // PATCH api/factura/2/anular
    // Anular una factura
    // ──────────────────────────────────────────────────────
    [HttpPatch("{id}/anular")]
    public async Task<IActionResult> Anular(int id)
    {
        Factura? factura = await _facturacionContext.Facturas
            .FirstOrDefaultAsync(f => f.Id == id);

        if (factura is null)
            return NotFound(new { mensaje = $"Factura con ID {id} no encontrada." });

        if (factura.Estado == "pagada")
            return BadRequest(new { mensaje = "No se puede anular una factura ya pagada." });

        if (factura.Estado == "anulada")
            return BadRequest(new { mensaje = "Esta factura ya está anulada." });

        factura.Estado = "anulada";

        await _facturacionContext.SaveChangesAsync();

        return Ok(new { mensaje = "Factura anulada correctamente.", factura });
    }

    // ──────────────────────────────────────────────────────
    // DELETE api/factura/2
    // Solo se pueden eliminar facturas en estado "pendiente"
    // ──────────────────────────────────────────────────────
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Factura? factura = await _facturacionContext.Facturas
            .FirstOrDefaultAsync(f => f.Id == id);

        if (factura is null)
            return NotFound(new { mensaje = $"Factura con ID {id} no encontrada." });

        if (factura.Estado != "pendiente")
            return BadRequest(new
            {
                mensaje = $"Solo se pueden eliminar facturas en estado 'pendiente'. " +
                          $"Estado actual: '{factura.Estado}'."
            });

        _facturacionContext.Facturas.Remove(factura);
        await _facturacionContext.SaveChangesAsync();

        return NoContent(); // 204
    }

    // ──────────────────────────────────────────────────────
    // PUT api/factura/2
    // Actualizar el total de una factura (solo si está pendiente)
    // ──────────────────────────────────────────────────────
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Factura facturaActualizada)
    {
        if (facturaActualizada is null)
            return BadRequest(new { mensaje = "Datos inválidos." });

        Factura? factura = await _facturacionContext.Facturas
            .FirstOrDefaultAsync(f => f.Id == id);

        if (factura is null)
            return NotFound(new { mensaje = $"Factura con ID {id} no encontrada." });

        if (factura.Estado != "pendiente")
            return BadRequest(new { mensaje = "Solo se pueden editar facturas en estado 'pendiente'." });

        if (facturaActualizada.Total <= 0)
            return BadRequest(new { mensaje = "El total debe ser mayor a cero." });

        factura.Total = facturaActualizada.Total;

        await _facturacionContext.SaveChangesAsync();

        return Ok(factura);
    }
}
