using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primer_Proyecto.Clase_1
{
    // ============================================================
    //  NetVerk — Clase 2
    //  Tema: Condicionales y Ciclos
    // ============================================================

    public static class Clase2_CondicionalesCiclos
    {
        public static void Ejecutar()
        {
            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine("  CLASE 2 — Condicionales y Ciclos");
            Console.WriteLine("══════════════════════════════════════\n");

            EjemploCondicionales();
            EjemploOperadoresLogicos();
            EjemploTernario();
            EjemploFor();
            EjemploForeach();
            EjemploWhile();
            EjemploDoWhile();
            EjemploBreakContinue();
        }

        // ── IF / ELSE IF / ELSE ──────────────────────────────────
        static void EjemploCondicionales()
        {
            Console.WriteLine("── if / else if / else ──────────────");

            int nota = 85;

            // Solo if
            if (nota >= 70)
                Console.WriteLine("Aprobaste ✅");

            // If / else if / else en cascada
            if (nota >= 90)
                Console.WriteLine("Excelente 🏆");
            else if (nota >= 80)
                Console.WriteLine("Muy Bueno 👍");
            else if (nota >= 70)
                Console.WriteLine("Aprobado ✅");
            else
                Console.WriteLine("Reprobado ❌");

            // Switch expression (C# 8+)
            string nivel = nota switch
            {
                >= 90 => "Excelente",
                >= 80 => "Muy Bueno",
                >= 70 => "Aprobado",
                _ => "Reprobado"
            };
            Console.WriteLine($"Switch expression → {nivel}\n");
        }

        // ── OPERADORES LÓGICOS ───────────────────────────────────
        static void EjemploOperadoresLogicos()
        {
            Console.WriteLine("── Operadores lógicos ───────────────");

            int a = 10, b = 5;
            bool tieneEdad = true;
            bool tieneDinero = false;
            bool tienePermiso = true;

            Console.WriteLine($"a == b  → {a == b}");
            Console.WriteLine($"a != b  → {a != b}");
            Console.WriteLine($"a > b   → {a > b}");
            Console.WriteLine($"a < b   → {a < b}");
            Console.WriteLine($"a >= 10 → {a >= 10}");
            Console.WriteLine($"a <= 10 → {a <= 10}");

            // AND — AMBAS deben ser true
            if (tieneEdad && tieneDinero)
                Console.WriteLine("Puede comprar (&&)");

            // OR — al menos UNA debe ser true
            if (tieneEdad || tieneDinero)
                Console.WriteLine("Cumple algo al menos (||)");

            // NOT — invierte el valor
            if (!tieneDinero)
                Console.WriteLine("No tiene dinero 💸 (!)");

            // Combinados
            if ((tieneEdad && tienePermiso) || tieneDinero)
                Console.WriteLine("Puede entrar (combinado)\n");
        }

        // ── OPERADOR TERNARIO ────────────────────────────────────
        static void EjemploTernario()
        {
            Console.WriteLine("── Operador ternario ? : ─────────────");

            int edad = 20;
            double nota = 75.5;

            string mayorMinor = edad >= 18 ? "Mayor de edad" : "Menor de edad";
            string estado = nota >= 70 ? "✅ Aprobado" : "❌ Reprobado";
            string etiqueta = nota >= 90 ? "Excelente" : "Normal";

            Console.WriteLine($"edad {edad}  → {mayorMinor}");
            Console.WriteLine($"nota {nota} → {estado}");
            Console.WriteLine($"etiqueta    → {etiqueta}");

            // Ternario anidado (con moderación)
            string clasificacion = nota >= 90 ? "Excelente"
                                 : nota >= 80 ? "Muy bueno"
                                 : nota >= 70 ? "Aprobado"
                                 : "Reprobado";
            Console.WriteLine($"Clasificación anidada → {clasificacion}\n");
        }

        // ── FOR ─────────────────────────────────────────────────
        static void EjemploFor()
        {
            Console.WriteLine("── for ───────────────────────────────");

            // Conteo básico
            for (int i = 0; i <= 5; i++)
                Console.Write($"{i} ");
            Console.WriteLine();

            // Tabla de multiplicar del 7
            Console.WriteLine("Tabla del 7:");
            for (int i = 1; i <= 10; i++)
                Console.WriteLine($"  7 x {i,2} = {7 * i}");

            // Recorrer lista con índice
            List<string> nombres = new List<string> { "Ana", "Luis", "Sofía" };
            Console.WriteLine($"Lista tiene {nombres.Count} elementos:");
            for (int i = 0; i < nombres.Count; i++)
                Console.WriteLine($"  [{i}] → {nombres[i]}");

            Console.WriteLine();
        }

        // ── FOREACH ─────────────────────────────────────────────
        static void EjemploForeach()
        {
            Console.WriteLine("── foreach ───────────────────────────");

            List<string> frutas = new List<string> { "manzana", "banano", "mango" };

            foreach (string fruta in frutas)
                Console.WriteLine($"  Fruta: {fruta.ToUpper()}");

            // Con objetos
            List<Estudiante> estudiantes = new List<Estudiante>
        {
            new Estudiante { Nombre = "Ana",   Nota = 95 },
            new Estudiante { Nombre = "Luis",  Nota = 62 },
            new Estudiante { Nombre = "Sofía", Nota = 88 },
        };

            Console.WriteLine("Estudiantes:");
            foreach (var e in estudiantes)
            {
                string estadoNota = e.Nota >= 70 ? "Aprobado ✅" : "Reprobado ❌";
                Console.WriteLine($"  {e.Nombre,-8}: {e.Nota} → {estadoNota}");
            }

            Console.WriteLine();
        }

        // ── WHILE ────────────────────────────────────────────────
        static void EjemploWhile()
        {
            Console.WriteLine("── while ─────────────────────────────");

            // Conteo simple con while
            int intentos = 0;
            while (intentos < 3)
            {
                Console.WriteLine($"  Intento #{intentos + 1}");
                intentos++;
            }

            // Contador regresivo
            int cuenta = 5;
            while (cuenta > 0)
            {
                Console.WriteLine($"  Lanzando en {cuenta}...");
                cuenta--;
            }
            Console.WriteLine("  🚀 ¡Despegue!\n");

            // Nota: el ejemplo de Console.ReadLine() para contraseña
            // está comentado para no bloquear el flujo del programa.
            // En un proyecto real funciona perfectamente:
            //
            // string password = "";
            // while (password != "NetVerk2026")
            // {
            //     Console.Write("Contraseña: ");
            //     password = Console.ReadLine();
            //     if (password != "NetVerk2026")
            //         Console.WriteLine("❌ Incorrecta, intenta de nuevo.");
            // }
            // Console.WriteLine("✅ Acceso concedido!");
        }

        // ── DO WHILE ─────────────────────────────────────────────
        static void EjemploDoWhile()
        {
            Console.WriteLine("── do while ──────────────────────────");
            Console.WriteLine("  (se ejecuta al menos una vez)");

            // Simulamos la entrada directamente para no bloquear
            // En clase real usar Console.ReadLine() como está comentado
            int numero = 5; // simulamos que el usuario ingresó 5

            Console.WriteLine($"  Número ingresado (simulado): {numero}");

            if (numero >= 1 && numero <= 10)
                Console.WriteLine($"  ✅ Válido: {numero} está entre 1 y 10\n");

            // Versión interactiva (descomentar para usar):
            // int numero;
            // do
            // {
            //     Console.Write("  Ingresa un número entre 1 y 10: ");
            //     numero = int.Parse(Console.ReadLine());
            //     if (numero < 1 || numero > 10)
            //         Console.WriteLine("  ❌ Fuera de rango.");
            // } while (numero < 1 || numero > 10);
            // Console.WriteLine($"  ✅ Elegiste: {numero}");
        }

        // ── BREAK y CONTINUE ─────────────────────────────────────
        static void EjemploBreakContinue()
        {
            Console.WriteLine("── break y continue ──────────────────");

            List<int> numeros = new List<int> { 2, 4, 1, 7, 3, 9, 6 };

            // BREAK — sale del ciclo al encontrar el primero > 5
            Console.WriteLine("BREAK — primer número > 5:");
            foreach (var n in numeros)
            {
                if (n > 5)
                {
                    Console.WriteLine($"  Encontré: {n} → saliendo del ciclo");
                    break;
                }
                Console.WriteLine($"  {n} no es > 5, continúo...");
            }

            // CONTINUE — salta los impares
            Console.WriteLine("\nCONTINUE — solo los pares del 1 al 10:");
            for (int i = 1; i <= 10; i++)
            {
                if (i % 2 != 0) continue;
                Console.Write($"  {i}");
            }
            Console.WriteLine("\n");
        }
    }
}
