using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer_Proyecto.Clase_1
{
    // ============================================================
    //  NetVerk — Clase 1
    //  Tema: Tipos de Datos básicos en C#
    // ============================================================
    public static class Clase1_TiposDatos
    {
        public static void Ejecutar()
        {
            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine("  CLASE 1 — Tipos de Datos");
            Console.WriteLine("══════════════════════════════════════\n");

            // String y sus propiedades básicas
            string mensaje = "Hola mundo";
            string mensaje2 = "";
            mensaje2 = string.Empty;

            Console.WriteLine($"Mensaje         : {mensaje}");
            Console.WriteLine($"Tamaño del texto: {mensaje.Length}");
            Console.WriteLine($"Mensaje2 vacío  : '{mensaje2}'");

            Console.WriteLine("\n── Tipos numéricos ──────────────────");

            // Enteros
            int edad = 25;
            long poblacionMundo = 8_000_000_000L;
            byte semaforo = 2;

            Console.WriteLine($"int    edad           = {edad}");
            Console.WriteLine($"long   poblacionMundo = {poblacionMundo:N0}");
            Console.WriteLine($"byte   semaforo       = {semaforo}");

            // Decimales
            double precio = 19.99;
            float temperatura = 36.6f;
            decimal sueldo = 5_500.00m;

            Console.WriteLine($"\ndouble  precio       = {precio}");
            Console.WriteLine($"float   temperatura  = {temperatura}");
            Console.WriteLine($"decimal sueldo       = ₡{sueldo:N2}  ← usar para dinero");

            Console.WriteLine("\n── Texto y carácter ─────────────────");

            string nombre = "NetVerk";
            char inicial = 'N';

            Console.WriteLine($"string nombre  = \"{nombre}\"");
            Console.WriteLine($"char   inicial = '{inicial}'");

            Console.WriteLine("\n── Lógicos ──────────────────────────");

            bool estaActivo = true;
            bool tieneDeuda = false;

            Console.WriteLine($"bool estaActivo = {estaActivo}");
            Console.WriteLine($"bool tieneDeuda = {tieneDeuda}");

            Console.WriteLine("\n── var e inferencia de tipo ─────────");

            var ciudad = "San José";   // C# sabe que es string
            var numero = 42;           // C# sabe que es int

            Console.WriteLine($"var ciudad = \"{ciudad}\"  → tipo: {ciudad.GetType().Name}");
            Console.WriteLine($"var numero = {numero}           → tipo: {numero.GetType().Name}");

            Console.WriteLine("\n── Nullable (puede ser null) ────────");

            //int? nota1 = null;
            //Console.WriteLine($"int? nota1 = {nota1 ?? "null (sin valor aún)"}");
            //nota1 = 85;
            //Console.WriteLine($"int? nota1 = {nota1}  (ya tiene valor)");

            Console.WriteLine("\n✅ Clase 1 finalizada.\n");
        }
    }
}
