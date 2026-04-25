using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Models;

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

        // ──────────────────────────────────────────────────────
        // GET api/Cliente
        // Devuelve todos los clientes
        // ──────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult GetAll()
        {
            // ⚠️ MALA PRÁCTICA: devolvemos la entidad directamente
            // Fase 3: se devuelve un DTO, no la entidad de BD
            return Ok(_clientes);
        }

        // ──────────────────────────────────────────────────────
        // GET api/cliente/2
        // Devuelve un cliente por ID
        // ──────────────────────────────────────────────────────
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // ⚠️ MALA PRÁCTICA: lógica de búsqueda en el controller
            // Fase 3: esto va en el Repository
            var cliente = _clientes.FirstOrDefault(x => x.Id == id);

            if (cliente is null)
                return NotFound(new { mensaje = $"El cliente con id {id} no se encuentra entre nuestros clientes."} );

            return Ok(cliente);
        }

        // ──────────────────────────────────────────────────────
        // POST api/cliente
        // Crear un nuevo cliente
        // ──────────────────────────────────────────────────────
        [HttpPost]
        public IActionResult Create([FromBody] Cliente cliente)
        {
            // ⚠️ MALA PRÁCTICA: validaciones manuales y básicas
            // Fase 3: se usa FluentValidation
            if (cliente == null)
                return BadRequest();

            if(string.IsNullOrEmpty(cliente.Nombre))
                return BadRequest(new { mensaje = "El nombre del cliente es requerido" });

            if (string.IsNullOrEmpty(cliente.Email))
                return BadRequest(new { mensaje = "El email del cliente es requerido" });

            cliente.Id = _nextId++;
            cliente.Activo = true;

            _clientes.Add(cliente);
            //return Ok(cliente);
            return CreatedAtAction("GetById", new { id = cliente.Id });
        }

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
