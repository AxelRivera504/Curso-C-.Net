// See https://aka.ms/new-console-template for more information
using Primer_Proyecto.Clase_1;
using Primer_Proyecto.Clase_3;
// ============================================================
//  NetVerk — Curso C# .NET
//  Program.cs — Punto de entrada principal
//  Aquí solo llamamos a cada clase de ejercicios
// ============================================================

// ── Descomenta la clase que quieras ejecutar ──────────────

// Clase1_TiposDatos.Ejecutar();
// Clase2_CondicionalesCiclos.Ejecutar();
// Clase2_LINQ.Ejecutar();
// Clase3_POO.Ejecutar();

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║     NetVerk — Selecciona una clase       ║");
Console.WriteLine("╠══════════════════════════════════════════╣");
Console.WriteLine("║  1 → Clase 1: Tipos de Datos             ║");
Console.WriteLine("║  2 → Clase 2: Condicionales y Ciclos     ║");
Console.WriteLine("║  3 → Clase 2: LINQ y Lambda              ║");
Console.WriteLine("║  4 → Clase 3: POO y Patrones             ║");
Console.WriteLine("╚══════════════════════════════════════════╝");
Console.Write("\nElige una opción: ");

string opcion = Console.ReadLine();

switch (opcion)
{
    case "1": Clase1_TiposDatos.Ejecutar(); break;
    case "2": Clase2_CondicionalesCiclos.Ejecutar(); break;
    case "3": Clase2_LINQ.Ejecutar(); break;
    case "4": Clase3_POO.Ejecutar(); break;
    default: Console.WriteLine("Opción no válida."); break;
}


