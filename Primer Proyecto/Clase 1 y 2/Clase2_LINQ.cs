using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer_Proyecto.Clase_1
{
    // ============================================================
    //  NetVerk — Clase 2 (continuación)
    //  Tema: LINQ y Expresiones Lambda
    // ============================================================
    public static class Clase2_LINQ
    {
        public static void Ejecutar()
        {
            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine("  CLASE 2 — LINQ y Lambda");
            Console.WriteLine("══════════════════════════════════════\n");

            EjemploCicloVsLinq();
            EjercicioPrecios();
            EjercicioProductos();
            EjercicioEstudiantesCompleto();
        }

        // ── CICLO vs LINQ ────────────────────────────────────────
        static void EjemploCicloVsLinq()
        {
            Console.WriteLine("── Ciclo tradicional vs LINQ ─────────");

            List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Forma antigua — ciclo manual
            int sumaManual = 0;
            foreach (var n in numeros)
            {
                if (n % 2 == 0)
                    sumaManual += n;
            }

            // Forma moderna — LINQ en una línea
            int sumaLinq = numeros.Where(n => n % 2 == 0).Sum();

            Console.WriteLine($"  Suma pares (ciclo) : {sumaManual}");
            Console.WriteLine($"  Suma pares (LINQ)  : {sumaLinq}");
            Console.WriteLine($"  ¿Son iguales?      : {sumaManual == sumaLinq}\n");
        }

        // ── EJERCICIO PRECIOS ────────────────────────────────────
        static void EjercicioPrecios()
        {
            Console.WriteLine("── Ejercicio: Lista de precios ───────");

            List<decimal> precios = new List<decimal>
        {
            1500, 800, 3200, 500, 12000, 250, 4500, 900
        };

            // ── Con ciclos ──────────────────────────
            decimal sumaTotal = 0;
            decimal precioMaximo = precios[0];
            int cantidadAltos = 0;

            foreach (var precio in precios)
            {
                sumaTotal += precio;
                if (precio > precioMaximo) precioMaximo = precio;
                if (precio > 1000) cantidadAltos++;
            }

            Console.WriteLine("  Con ciclos:");
            Console.WriteLine($"    Suma total      : ₡{sumaTotal:N0}");
            Console.WriteLine($"    Precio máximo   : ₡{precioMaximo:N0}");
            Console.WriteLine($"    Mayores a ₡1000 : {cantidadAltos}");
            Console.Write("    Económicos      : ");
            foreach (var p in precios)
                if (p < 1000) Console.Write($"₡{p} ");
            Console.WriteLine();

            // ── Con LINQ ────────────────────────────
            Console.WriteLine("  Con LINQ:");
            Console.WriteLine($"    Suma total      : ₡{precios.Sum():N0}");
            Console.WriteLine($"    Precio máximo   : ₡{precios.Max():N0}");
            Console.WriteLine($"    Mayores a ₡1000 : {precios.Count(p => p > 1000)}");
            Console.Write("    Económicos      : ");
            precios.Where(p => p < 1000)
                   .ToList()
                   .ForEach(p => Console.Write($"₡{p} "));
            Console.WriteLine("\n");
        }

        // ── EJERCICIO PRODUCTOS ──────────────────────────────────
        static void EjercicioProductos()
        {
            Console.WriteLine("── Ejercicio: Productos con LINQ ─────");

            List<Producto> productos = new List<Producto>
        {
            new Producto { Nombre="Laptop Dell",      Categoria="Electrónica", Precio=850000m,  Stock=5,  Disponible=true  },
            new Producto { Nombre="Mouse Logitech",   Categoria="Electrónica", Precio=15000m,   Stock=20, Disponible=true  },
            new Producto { Nombre="Silla Ergonómica", Categoria="Muebles",     Precio=120000m,  Stock=3,  Disponible=true  },
            new Producto { Nombre="Monitor LG",       Categoria="Electrónica", Precio=320000m,  Stock=0,  Disponible=false },
            new Producto { Nombre="Escritorio",       Categoria="Muebles",     Precio=95000m,   Stock=7,  Disponible=true  },
            new Producto { Nombre="Teclado Mecánico", Categoria="Electrónica", Precio=45000m,   Stock=12, Disponible=true  },
            new Producto { Nombre="Lámpara LED",      Categoria="Iluminación", Precio=8500m,    Stock=0,  Disponible=false },
        };

            // 1. Disponibles
            var disponibles = productos.Where(p => p.Disponible).ToList();
            Console.WriteLine($"  1. Disponibles          : {disponibles.Count} productos");

            // 2. Electrónica ordenada por precio
            var electronicaOrdenada = productos
                .Where(p => p.Categoria == "Electrónica")
                .OrderBy(p => p.Precio)
                .ToList();
            Console.WriteLine("  2. Electrónica (por precio):");
            electronicaOrdenada.ForEach(p => Console.WriteLine($"     {p.Nombre,-22} ₡{p.Precio:N0}"));

            // 3. El más caro
            var masCaro = productos.OrderByDescending(p => p.Precio).First();
            Console.WriteLine($"  3. Más caro             : {masCaro.Nombre} — ₡{masCaro.Precio:N0}");

            // 4. Precio promedio disponibles
            var promedio = productos.Where(p => p.Disponible).Average(p => p.Precio);
            Console.WriteLine($"  4. Promedio disponibles : ₡{promedio:N0}");

            // 5. Sin stock
            var sinStock = productos.Where(p => p.Stock == 0).Select(p => p.Nombre).ToList();
            Console.WriteLine($"  5. Sin stock            : {string.Join(", ", sinStock)}");

            // 6. Por categoría
            var porCategoria = productos
                .GroupBy(p => p.Categoria)
                .Select(g => new { Categoria = g.Key, Total = g.Count() });
            Console.WriteLine("  6. Por categoría:");
            foreach (var c in porCategoria)
                Console.WriteLine($"     {c.Categoria,-14}: {c.Total} productos");

            Console.WriteLine();
        }

        // ── EJERCICIO ESTUDIANTES COMPLETO ───────────────────────
        static void EjercicioEstudiantesCompleto()
        {
            Console.WriteLine("── Ejercicio: Estudiantes con LINQ ───");

            List<Estudiante> estudiantes = new List<Estudiante>
        {
            new Estudiante { Nombre = "Ana López",    Nota = 95 },
            new Estudiante { Nombre = "Luis Mora",    Nota = 62 },
            new Estudiante { Nombre = "Sofía Vargas", Nota = 88 },
            new Estudiante { Nombre = "Carlos Ruiz",  Nota = 71 },
            new Estudiante { Nombre = "Valeria Cruz", Nota = 97 },
        };

            // Lambda reutilizable
            Func<int, string> clasificar = nota => nota switch
            {
                >= 90 => "Excelente 🏆",
                >= 80 => "Muy Bueno 👍",
                >= 70 => "Aprobado  ✅",
                _ => "Reprobado ❌"
            };

            Console.WriteLine("  Reporte:");
            foreach (var e in estudiantes.OrderByDescending(e => e.Nota))
                Console.WriteLine($"  {e.Nombre,-16} | {e.Nota,3} | {clasificar(e.Nota)}");

            var mejor = estudiantes.OrderByDescending(e => e.Nota).First();
            double prom = estudiantes.Average(e => e.Nota);
            int aprobados = estudiantes.Count(e => e.Nota >= 70);

            Console.WriteLine($"\n  🏆 Mejor   : {mejor.Nombre} ({mejor.Nota})");
            Console.WriteLine($"  📊 Promedio: {prom:F1}");
            Console.WriteLine($"  ✅ Aprobados: {aprobados} de {estudiantes.Count}\n");
        }
    }

    // ── Modelos compartidos ──────────────────────────────────────
    // (úsalos en todos los archivos de clase)

    public class Estudiante
    {
        public string Nombre { get; set; }
        public int Nota { get; set; }
    }

    public class Producto
    {
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Disponible { get; set; }
    }
}
