using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiPrimeraApi.Infrastructure;
using MiPrimeraApi.Models;
using System.Threading.Tasks;

namespace MiPrimeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        // ⚠️ MALA PRÁCTICA intencional — datos en memoria estática
        // Clase 5: esto se reemplaza por EF Core + SQL Server
        // Clase 7-11: esto se reemplaza por Repository + Clean Architecture
        private static List<Cliente> _clientes = new()
        {
            new Cliente { Id=1, Nombre="Ana García",   Email="ana@mail.com",   Telefono="8888-1111", Activo=true  },
            new Cliente { Id=2, Nombre="Luis Mora",    Email="luis@mail.com",  Telefono="8888-2222", Activo=true  },
            new Cliente { Id=3, Nombre="Sofía Vargas", Email="sofia@mail.com", Telefono="8888-3333", Activo=false },
        };
        private static int _nextId = 4;

        //Persistencia de datos
        private readonly FacturacionContext _facturacionContext;

        public ClienteController(FacturacionContext facturacionContext)
        {
            _facturacionContext = facturacionContext;
        }

        // ──────────────────────────────────────────────────────
        // GET api/Cliente
        // Devuelve todos los clientes
        // ──────────────────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // ⚠️ MALA PRÁCTICA: devolvemos la entidad directamente
            // Fase 3: se devuelve un DTO, no la entidad de BD
            //return Ok(_clientes);

            List<Cliente>clientes = await _facturacionContext.Clientes.ToListAsync();
            //select * from Cliente

            return Ok(clientes);
        }

        // ──────────────────────────────────────────────────────
        // GET api/cliente/2
        // Devuelve un cliente por ID
        // ──────────────────────────────────────────────────────
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            // ⚠️ MALA PRÁCTICA: lógica de búsqueda en el controller
            // Fase 3: esto va en el Repository
            //var cliente = _clientes.FirstOrDefault(x => x.Id == id);

            //if (cliente is null)
            //    return NotFound(new { mensaje = $"El cliente con id {id} no se encuentra entre nuestros clientes."} );

            Cliente? cliente = await _facturacionContext.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            //select * from cliente where Id == id

            if (cliente is null)
                return NotFound(new { mensaje = $"El cliente con id {id} no se encuentra entre nuestros clientes."} );

            return Ok(cliente);
        }

        // ──────────────────────────────────────────────────────
        // GET api/cliente/buscar?nombre=ana
        // Buscar clientes por nombre
        // ──────────────────────────────────────────────────────
        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest(new { mensaje = "Debe ingresar un nombre para buscar." });

            // EF Core traduce Contains a: WHERE Nombre LIKE '%ana%'
            List<Cliente> resultado = await _facturacionContext.Clientes
                .Where(c => c.Nombre.Contains(nombre))
                .ToListAsync();

            return Ok(resultado);
        }

        // ──────────────────────────────────────────────────────
        // GET api/cliente/activos
        // Devuelve solo los clientes activos
        // ──────────────────────────────────────────────────────
        [HttpGet("activos")]
        public async Task<IActionResult> GetActivos()
        {
            List<Cliente> activos = await _facturacionContext.Clientes
                .Where(c => c.Activo)
                .ToListAsync();

            return Ok(activos);
        }

        // ──────────────────────────────────────────────────────
        // POST api/cliente
        // Crear un nuevo cliente
        // ──────────────────────────────────────────────────────
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            if (cliente is null)
                return BadRequest(new { mensaje = "El cuerpo de la petición no puede estar vacío." });

            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                return BadRequest(new { mensaje = "El nombre del cliente es requerido." });

            if (string.IsNullOrWhiteSpace(cliente.Email))
                return BadRequest(new { mensaje = "El email del cliente es requerido." });

            // Verificar email duplicado en la BD
            bool emailExiste = await _facturacionContext.Clientes
                .AnyAsync(c => c.Email == cliente.Email);

            if (emailExiste)
                return Conflict(new { mensaje = $"Ya existe un cliente con el email '{cliente.Email}'." });

            cliente.Activo = true;

            await _facturacionContext.Clientes.AddAsync(cliente);
            await _facturacionContext.SaveChangesAsync(); // INSERT INTO Clientes...

            return StatusCode(201, cliente);
        }

        // ──────────────────────────────────────────────────────
        // PUT api/cliente/5
        // Actualizar un cliente completo
        // ──────────────────────────────────────────────────────
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente clienteActualizado)
        {
            if (clienteActualizado is null)
                return BadRequest(new { mensaje = "Datos inválidos." });

            Cliente? cliente = await _facturacionContext.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente is null)
                return NotFound(new { mensaje = $"El cliente con ID {id} no fue encontrado." });

            // ⚠️ MALA PRÁCTICA: campo a campo
            // Fase 3: AutoMapper hace esto en una línea
            cliente.Nombre = clienteActualizado.Nombre;
            cliente.Email = clienteActualizado.Email;
            cliente.Telefono = clienteActualizado.Telefono;
            cliente.Activo = clienteActualizado.Activo;

            await _facturacionContext.SaveChangesAsync(); // UPDATE Clientes SET...

            return Ok(cliente);
        }

        // ──────────────────────────────────────────────────────
        // PATCH api/cliente/5
        // Toggle activo / inactivo
        // ──────────────────────────────────────────────────────
        [HttpPatch("{id}")]
        public async Task<IActionResult> ToggleActivo(int id)
        {
            Cliente? cliente = await _facturacionContext.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente is null)
                return NotFound(new { mensaje = $"El cliente con ID {id} no fue encontrado." });

            cliente.Activo = !cliente.Activo;

            await _facturacionContext.SaveChangesAsync();

            string estadoCliente = cliente.Activo ? "activado" : "desactivado";

            return Ok(new { mensaje = $"Cliente {estadoCliente} correctamente.", cliente });
        }

        // ──────────────────────────────────────────────────────
        // DELETE api/cliente/5
        // Eliminar un cliente
        // ──────────────────────────────────────────────────────
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Cliente? cliente = await _facturacionContext.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);

            if (cliente is null)
                return NotFound(new { mensaje = $"El cliente con ID {id} no fue encontrado." });

            _facturacionContext.Clientes.Remove(cliente);
            await _facturacionContext.SaveChangesAsync(); // DELETE FROM Clientes WHERE Id = @id

            return NoContent(); // 204 — éxito sin body
        }


        //// ──────────────────────────────────────────────────────
        //// POST api/cliente
        //// Crear un nuevo cliente
        //// ──────────────────────────────────────────────────────
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] Cliente cliente)
        //{
        //    // ⚠️ MALA PRÁCTICA: validaciones manuales y básicas
        //    // Fase 3: se usa FluentValidation
        //    if (cliente == null)
        //        return BadRequest();

        //    if(string.IsNullOrEmpty(cliente.Nombre))
        //        return BadRequest(new { mensaje = "El nombre del cliente es requerido" });

        //    if (string.IsNullOrEmpty(cliente.Email))
        //        return BadRequest(new { mensaje = "El email del cliente es requerido" });

        //    //cliente.Id = _nextId++;
        //    //_clientes.Add(cliente);
        //    cliente.Activo = true;

        //    await _facturacionContext.Clientes.AddAsync(cliente);
        //    await _facturacionContext.SaveChangesAsync();

        //    return Ok(cliente);
        //    //return CreatedAtAction("GetById", new { id = cliente.Id });
        //}

        //[HttpGet("buscar")]
        //public IActionResult Buscar([FromQuery] string nombre)
        //{
        //    if (string.IsNullOrWhiteSpace(nombre)) return BadRequest(new { mensaje = "Nombre requerido" });
        //    var resultado = _clientes.Where(c => c.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();
        //    return Ok(resultado);
        //}

        //[HttpGet("activos")]
        //public IActionResult GetActivos()
        //{
        //    var activos = _clientes.Where(c => c.Activo).ToList();
        //    return Ok(activos);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] Cliente clienteActualizado)
        //{
        //    if (clienteActualizado is null)
        //        return BadRequest(new { mensaje = "Datos inválidos" });

        //    var cliente = _clientes.FirstOrDefault(c => c.Id == id);    

        //    if (cliente is null)
        //        return BadRequest(new { mensaje = $"El cliente con ID {id} no existe" });

        //    //Mala práctica: actualizamos los campos uno por uno
        //    //Fase 3 AutoMapper
        //    cliente.Email = clienteActualizado.Email;
        //    cliente.Activo = clienteActualizado.Activo;
        //    cliente.Nombre = clienteActualizado.Nombre;
        //    cliente.Telefono = clienteActualizado.Telefono;

        //    return Ok(cliente);
        //}

        //[HttpPatch("{id}")]
        //public IActionResult ToggleActivo(int id)
        //{
        //    var cliente = _clientes.FirstOrDefault(c => c.Id == id);

        //    if (cliente is null)
        //        return BadRequest(new { mensaje = $"El cliente con ID {id} no existe" });

        //    cliente.Activo = !cliente.Activo;

        //    string estadoCliente = cliente.Activo ? "activado" : "desactivado";

        //    return Ok(new { mensaje = $"Cliente {estadoCliente} correctamente", cliente});
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    var cliente = _clientes.FirstOrDefault(c => c.Id == id);

        //    if (cliente is null)
        //        return BadRequest(new { mensaje = $"El cliente con ID {id} no existe" });

        //    _clientes.Remove(cliente);

        //    return NoContent(); 
        //}

        //Tarea
        //Crear un endpoint con metodo http get donde busquen la información del cliente por su nombre.
        //Crear un endpoint con metodo http get donde traigan los clientes que esten activos

        // ──────────────────────────────────────────────────────
        // PUT api/cliente/2
        // Actualizar un cliente completo
        // ──────────────────────────────────────────────────────
        //[HttpPut("{id}")]

        // ──────────────────────────────────────────────────────
        // PATCH api/cliente/2/activar
        // Activar o desactivar un cliente (toggle)
        // ──────────────────────────────────────────────────────
        //[HttpPatch("{id}/activar")]

        // ──────────────────────────────────────────────────────
        // DELETE api/cliente/2
        // Eliminar un cliente
        // ──────────────────────────────────────────────────────
        //[HttpDelete("{id}")]
        //return NoContent(); // 204 — éxito sin body


    }
}
