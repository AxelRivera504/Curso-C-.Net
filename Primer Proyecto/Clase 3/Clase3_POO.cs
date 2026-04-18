using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer_Proyecto.Clase_3
{
    // ============================================================
    //  NetVerk — Clase 3
    //  Tema: POO, Interfaces y Patrones
    // ============================================================

    public class Clase3_POO
    {
        public static void Ejecutar()
        {
            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine("  CLASE 3 — POO y Patrones");
            Console.WriteLine("══════════════════════════════════════\n");

            Demo_Encapsulamiento();
            Demo_Herencia();
            Demo_Polimorfismo();
            Demo_Interfaces();
            Demo_Singleton();
            //Demo_Builder();
            //Demo_Lambda();
        }

        // ══════════════════════════════════════════════════════
        // PILAR 1 — ENCAPSULAMIENTO
        // ══════════════════════════════════════════════════════
        static void Demo_Encapsulamiento()
        {
            Console.WriteLine("── 1. Encapsulamiento ────────────────");

            var cuenta = new CuentaBancaria("CR-001", 100_000m);
            cuenta.Depositar(50_000m);

            try
            {
                cuenta.Retirar(200_000m); // debe fallar
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"  ❌ Controlado: {ex.Message}");
            }

            cuenta.Retirar(30_000m);
            cuenta.MostrarHistorial();
            Console.WriteLine();
        }

        // ══════════════════════════════════════════════════════
        // PILAR 2 — HERENCIA + POLIMORFISMO
        // ══════════════════════════════════════════════════════
        static void Demo_Herencia()
        {
            Console.WriteLine("── 2. Herencia ───────────────────────");

            List<Empleado> planilla = new List<Empleado>
            {
                new EmpleadoPorHoras(1, "Ana López",    "101", 160, 3_500m),
                new EmpleadoConBono (2, "Luis Mora",    "202", 400_000m, 50_000m),
                new EmpleadoPorHoras(3, "Sofía Vargas", "303", 120, 4_000m),
            };

            planilla.ForEach(e => e.MostrarInfo());

            decimal total = planilla.Sum(e => e.CalcularSalario());
            Console.WriteLine($"  💰 Total planilla: ₡{total:N2}\n");
        }

        // ══════════════════════════════════════════════════════
        // PILAR 3 & 4 — ABSTRACCIÓN + POLIMORFISMO
        // ══════════════════════════════════════════════════════
        static void Demo_Polimorfismo()
        {
            Console.WriteLine("── 3 & 4. Abstracción + Polimorfismo ─");

            List<Figura> figuras = new List<Figura>
            {
                new Circulo   ("Rojo",  5),
                new Rectangulo("Azul",  8, 4),
                new Triangulo ("Verde", 6, 4, 5, 5, 6),
            };

            Console.WriteLine($"  {"Figura",-12} {"Color",-8} {"Área",8} {"Perímetro",10}");
            Console.WriteLine($"  {"──────────────────────────────────────────"}");
            figuras.ForEach(f => f.MostrarDatos());

            var masGrande = figuras.OrderByDescending(f => f.CalcularArea()).First();
            Console.WriteLine($"\n  🏆 Más grande: {masGrande.GetType().Name} — área: {masGrande.CalcularArea():F2}\n");
        }

        // ══════════════════════════════════════════════════════
        // INTERFACES
        // ══════════════════════════════════════════════════════
        static void Demo_Interfaces()
        {
            Console.WriteLine("── Interfaces ────────────────────────");

            var repo = new ProductoRepo();
            repo.Agregar(new ProductoPOO { Id = 1, Nombre = "Laptop", Precio = 850_000m });
            repo.Agregar(new ProductoPOO { Id = 2, Nombre = "Mouse", Precio = 15_000m });
            repo.Agregar(new ProductoPOO { Id = 3, Nombre = "Monitor", Precio = 320_000m });

            Console.WriteLine("  Todos los productos:");
            repo.ObtenerTodos()
                .ForEach(p => Console.WriteLine($"  [{p.Id}] {p.Nombre,-12} ₡{p.Precio:N0}"));

            var uno = repo.ObtenerPorId(2);
            Console.WriteLine($"\n  Buscar ID=2 → {uno.Nombre}");
            Console.WriteLine($"\n  CSV:\n  {repo.ExportarCSV().Replace("\n", "\n  ")}\n");
        }

        // ══════════════════════════════════════════════════════
        // PATRÓN SINGLETON
        // ══════════════════════════════════════════════════════
        static void Demo_Singleton()
        {
            Console.WriteLine("── Singleton ─────────────────────────");

            var cfg1 = ConfiguracionApp.Instancia;
            var cfg2 = ConfiguracionApp.Instancia;

            Console.WriteLine($"  ¿cfg1 == cfg2? {cfg1 == cfg2}  ← misma instancia");
            Console.WriteLine($"  App    : {cfg1.AppName}");
            Console.WriteLine($"  Debug  : {cfg1.ModoDebug}");
            Console.WriteLine($"  Conn   : {cfg1.ConnectionString}\n");
        }

        // ── Singleton ────────────────────────────────────────────────
        public sealed class ConfiguracionApp
        {
            private static ConfiguracionApp _instancia;
            private static readonly object _lock = new object();

            public string ConnectionString { get; } = "Server=localhost;Database=NetVerkDB;";
            public string AppName { get; } = "NetVerk API";
            public bool ModoDebug { get; } = true;

            private ConfiguracionApp() { }

            public static ConfiguracionApp Instancia
            {
                get
                {
                    if (_instancia == null)
                        lock (_lock)
                            if (_instancia == null)
                                _instancia = new ConfiguracionApp();
                    return _instancia;
                }
            }
        }

        // ── Interfaces ───────────────────────────────────────────────
        public interface IRepositorio<T>
        {
            void Agregar(T entidad);
            void Eliminar(int id);
            T ObtenerPorId(int id);
            List<T> ObtenerTodos();
        }

        public interface IExportable
        {
            string ExportarCSV();
        }

        public class ProductoPOO
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
        }

        public class ProductoRepo : IRepositorio<ProductoPOO>, IExportable
        {
            private List<ProductoPOO> _db = new();

            public void Agregar(ProductoPOO p)
            {
                _db.Add(p);
                Console.WriteLine($"  ✅ '{p.Nombre}' agregado.");
            }

            public void Eliminar(int id)
            {
                var p = _db.FirstOrDefault(x => x.Id == id)
                    ?? throw new KeyNotFoundException($"ID {id} no existe.");
                _db.Remove(p);
            }

            public ProductoPOO ObtenerPorId(int id) =>
                _db.FirstOrDefault(p => p.Id == id)
                ?? throw new KeyNotFoundException($"ID {id} no encontrado.");

            public List<ProductoPOO> ObtenerTodos() => _db;

            public string ExportarCSV()
            {
                var filas = _db.Select(p => $"{p.Id},{p.Nombre},{p.Precio}");
                return "Id,Nombre,Precio\n" + string.Join("\n", filas);
            }
        }

        #region Clases
        public class CuentaBancaria
        {
            private decimal _saldo;
            private string _numero;
            private List<string> _historial = new();

            public decimal Saldo => _saldo;
            public string Numero => _numero;

            public CuentaBancaria(string numero, decimal saldoInicial)
            {
                _numero = numero;
                _saldo = saldoInicial;
                _historial.Add($"[INICIO] Cuenta creada con ₡{saldoInicial:N2}");
            }

            public void Depositar(decimal monto)
            {
                if (monto <= 0) throw new ArgumentException("Monto debe ser mayor a cero.");
                _saldo += monto;
                _historial.Add($"[+] ₡{monto:N2} | Saldo: ₡{_saldo:N2}");
                Console.WriteLine($"  ✅ Depósito OK. Saldo: ₡{_saldo:N2}");
            }

            public void Retirar(decimal monto)
            {
                if (monto <= 0) throw new ArgumentException("Monto debe ser mayor a cero.");
                if (monto > _saldo) throw new InvalidOperationException("Saldo insuficiente.");
                _saldo -= monto;
                _historial.Add($"[-] ₡{monto:N2} | Saldo: ₡{_saldo:N2}");
                Console.WriteLine($"  ✅ Retiro OK. Saldo: ₡{_saldo:N2}");
            }

            public void MostrarHistorial()
            {
                Console.WriteLine($"  📋 Historial {_numero}:");
                _historial.ForEach(h => Console.WriteLine($"     {h}"));
            }
        }

        // ── Herencia ─────────────────────────────────────────────────
        public class Empleado
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Cedula { get; set; }
            public decimal SalarioBase { get; set; }

            public Empleado(int id, string nombre, string cedula, decimal salarioBase)
            {
                Id = id;
                Nombre = nombre;
                Cedula = cedula;
                SalarioBase = salarioBase;
            }

            public virtual decimal CalcularSalario() => SalarioBase;

            public virtual void MostrarInfo()
            {
                Console.WriteLine($"  👤 {Nombre,-16} | {GetType().Name,-20} | ₡{CalcularSalario():N2}");
            }
        }

        public class EmpleadoPorHoras : Empleado
        {
            public int Horas { get; set; }
            public decimal Precio { get; set; }

            public EmpleadoPorHoras(int id, string nombre, string cedula, int horas, decimal precio)
                : base(id, nombre, cedula, 0)
            {
                Horas = horas;
                Precio = precio;
            }

            public override decimal CalcularSalario() => Horas * Precio;
        }

        public class EmpleadoConBono : Empleado
        {
            public decimal Bono { get; set; }

            public EmpleadoConBono(int id, string nombre, string cedula, decimal salario, decimal bono)
                : base(id, nombre, cedula, salario)
            {
                Bono = bono;
            }

            public override decimal CalcularSalario() => SalarioBase + Bono;
        }

        // ── Abstracción + Polimorfismo ───────────────────────────────
        public abstract class Figura
        {
            public string Color { get; set; }
            public Figura(string color) => Color = color;

            public abstract double CalcularArea();
            public abstract double CalcularPerimetro();

            public void MostrarDatos()
            {
                Console.WriteLine($"  {GetType().Name,-12} {Color,-8} " +
                                  $"{CalcularArea(),8:F2} {CalcularPerimetro(),10:F2}");
            }
        }

        public class Circulo : Figura
        {
            public double Radio { get; set; }
            public Circulo(string color, double radio) : base(color) => Radio = radio;
            public override double CalcularArea() => Math.PI * Radio * Radio;
            public override double CalcularPerimetro() => 2 * Math.PI * Radio;
        }

        public class Rectangulo : Figura
        {
            public double Ancho { get; set; }
            public double Alto { get; set; }
            public Rectangulo(string color, double ancho, double alto) : base(color)
            { Ancho = ancho; Alto = alto; }
            public override double CalcularArea() => Ancho * Alto;
            public override double CalcularPerimetro() => 2 * (Ancho + Alto);
        }

        public class Triangulo : Figura
        {
            public double Base, Altura, L1, L2, L3;
            public Triangulo(string color, double b, double h, double l1, double l2, double l3)
                : base(color) { Base = b; Altura = h; L1 = l1; L2 = l2; L3 = l3; }
            public override double CalcularArea() => (Base * Altura) / 2;
            public override double CalcularPerimetro() => L1 + L2 + L3;
        }

        #endregion
    }
}
