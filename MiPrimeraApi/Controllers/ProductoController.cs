using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiPrimeraApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MiPrimeraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private static List<Producto> _productos = new()
        {
            new Producto { Id=1, Nombre="Laptop Dell",      Categoria="Electrónica", Precio=850000m, Stock=5  },
            new Producto { Id=2, Nombre="Mouse Logitech",   Categoria="Electrónica", Precio=15000m,  Stock=20 },
            new Producto { Id=3, Nombre="Silla Ergonómica", Categoria="Muebles",     Precio=120000m, Stock=3  },
            new Producto { Id=4, Nombre="Monitor LG",       Categoria="Electrónica", Precio=320000m, Stock=0  },
            new Producto { Id=5, Nombre="Escritorio",       Categoria="Muebles",     Precio=95000m,  Stock=7  },
        };
        private static int _nextId = 6;


        // ──────────────────────────────────────────────────────
        // GET api/Producto
        // Devuelve todos los productos
        // ──────────────────────────────────────────────────────
        [HttpGet]
        public IActionResult GetAll()
        {
            // ⚠️ MALA PRÁCTICA: devolvemos la entidad directamente
            // Fase 3: se devuelve un DTO, no la entidad de BD
            return Ok(_productos);
        }

        // ──────────────────────────────────────────────────────
        // GET api/producto/2
        // Devuelve un producto por ID
        // ──────────────────────────────────────────────────────
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // ⚠️ MALA PRÁCTICA: lógica de búsqueda en el controller
            // Fase 3: esto va en el Repository
            var producto = _productos.FirstOrDefault(x => x.Id == id);

            if (producto is null)
                return NotFound(new { mensaje = $"El Producto con id {id} no se encuentra entre nuestros productos." });

            return Ok(producto);
        }

        [HttpGet("disponibles")]
        public IActionResult GetDisponibles()
        {
            var productos = _productos.Where(c => c.Stock > 0).ToList();
            return Ok(productos);
        }

        [HttpGet("categoria/{categoria}")]
        public IActionResult GeyByCategory(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria)) return BadRequest(new { mensaje = "Categoria requerido" });
            var resultado = _productos.Where(c => c.Categoria.Contains(categoria, StringComparison.OrdinalIgnoreCase)).ToList();
            
            return Ok(resultado);
        }

        // ──────────────────────────────────────────────────────
        // POST api/Producto
        // Crear un nuevo producto
        // ──────────────────────────────────────────────────────
        [HttpPost]
        public IActionResult Create([FromBody] Producto producto)
        {
            // ⚠️ MALA PRÁCTICA: validaciones manuales y básicas
            // Fase 3: se usa FluentValidation
            if (producto == null)
                return BadRequest();

            if (string.IsNullOrEmpty(producto.Nombre))
                return BadRequest(new { mensaje = "El nombre del producto es requerido" });

            if (producto.Precio < 0)
                return BadRequest(new { mensaje = "El producto no cuenta con un precio" });

            if (producto.Stock < 0)
                return BadRequest(new { mensaje = "El stock no puede ser negativo" });

            producto.Id = _nextId++;

            _productos.Add(producto);

            return Ok(producto);
            //return CreatedAtAction("GetById", new { id = cliente.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Producto productoActualizado)
        {
            if (productoActualizado is null)
                return BadRequest(new { mensaje = "Datos inválidos" });

            var producto = _productos.FirstOrDefault(c => c.Id == id);

            if (producto is null)
                return BadRequest(new { mensaje = $"El producto con ID {id} no existe" });

            //Mala práctica: actualizamos los campos uno por uno
            //Fase 3 AutoMapper
            producto.Nombre = productoActualizado.Nombre;
            producto.Categoria = productoActualizado.Categoria;
            producto.Precio = productoActualizado.Precio;
            producto.Stock = productoActualizado.Stock;

            return Ok(producto);
        }

        [HttpPatch("{id}/stock")]
        public IActionResult ActualizarStock(int id, [FromQuery] int cantidad)
        {
            var producto = _productos.FirstOrDefault(c => c.Id == id);

            if (producto is null)
                return BadRequest(new { mensaje = $"El producto con ID {id} no existe" });

            if (producto.Stock + cantidad < 0)
                return BadRequest(new { mensaje = $"El stock no puede dejarse en negativo" });

            producto.Stock += cantidad;

            string resultados = cantidad >= 0 ? $"Se agregaron {cantidad} unidades" : $"Se removieron {cantidad} unidades";

            return Ok( new { mensaje = resultados, stockActual = producto.Stock, producto});
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var producto = _productos.FirstOrDefault(c => c.Id == id);

            if (producto is null)
                return BadRequest(new { mensaje = $"El producto con ID {id} no existe" });

            _productos.Remove(producto);

            return NoContent();
        }
    }
}
