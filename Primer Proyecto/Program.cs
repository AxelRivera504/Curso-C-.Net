// See https://aka.ms/new-console-template for more information
#region Clase 1 - Tipos de datos
string mensaje = "Hola mundo";
string mensaje2 = "";
mensaje2 = string.Empty;

Console.WriteLine($"este es el tamaño del texto: {mensaje.Length}");
#endregion

#region Clase 2 - Tipos de datos Pt2
/*///////////////////////////
 Tipos de datos
///////////////////////////*/

// ENTEROS — números sin punto decimal
int edad = 25;              // el más común, de -2 billones a 2 billones
long poblacionMundo = 8000000000L;  // cuando int no alcanza
byte semaforo = 2;          // solo 0-255, ideal para valores pequeños

// DECIMALES — números con punto
double precio = 19.99;      // el más usado para decimales
float temperatura = 36.6f;  // menos precisión, más rápido
decimal sueldo = 5500.00m;  // SIEMPRE usar para dinero (más preciso)

// TEXTO
string nombre = "NetVerk";          // cadena de caracteres
char inicial = 'N';                 // UN solo carácter (comillas simples!)

// LÓGICO
bool estaActivo = true;     // solo true o false
bool tieneDeuda = false;

// VAR — C# adivina el tipo (sigue siendo tipado!)
var ciudad = "San José";    // C# sabe que es string
var numero = 42;            // C# sabe que es int

// NULLABLE — una variable que puede ser nula
int? nota1 = null;           // el ? le dice "puede no tener valor"

// Recorrer una lista con índice
List<string> nombres1 = new List<string> { "Ana", "Luis", "Sofía" };

// ================================================
// IF / ELSE IF / ELSE — Decisiones en cascada
// ================================================

int notaCondicion = 85;

// Solo if — si no cumple, no hace nada
if (notaCondicion >= 70)
{
    Console.WriteLine("Aprobaste ✅");
}

// If con else — una cosa u otra
if (notaCondicion >= 70)
{
    Console.WriteLine("Aprobaste ✅");
}
else
{
    Console.WriteLine("Reprobaste ❌");
}

// If / else if / else — múltiples condiciones
// Se evalúan de arriba hacia abajo, la primera que
// cumpla se ejecuta y las demás se ignoran
if (notaCondicion >= 90)
{
    Console.WriteLine("Excelente 🏆");
}
else if (notaCondicion >= 80)
{
    Console.WriteLine("Muy Bueno 👍");
}
else if (notaCondicion >= 70)
{
    Console.WriteLine("Aprobado ✅");
}
else
{
    Console.WriteLine("Reprobado ❌");
}
// Con notaCondicion = 85 → imprime "Muy Bueno 👍"

int a = 10, b = 5;

// ── COMPARACIÓN ────────────────────────────────
Console.WriteLine(a == b);   // false — ¿son iguales?
Console.WriteLine(a != b);   // true  — ¿son diferentes?
Console.WriteLine(a > b);    // true  — ¿a es mayor?
Console.WriteLine(a < b);    // false — ¿a es menor?
Console.WriteLine(a >= 10);  // true  — ¿mayor o igual?
Console.WriteLine(a <= 10);  // true  — ¿menor o igual?

// ── LÓGICOS ────────────────────────────────────
bool tieneEdad = true;
bool tieneDinero = false;
bool tienePermiso = true;

// AND (&&) — AMBAS condiciones deben ser true
if (tieneEdad && tieneDinero)
    Console.WriteLine("Puede comprar");        // NO entra, tieneDinero es false

// OR (||) — Al menos UNA debe ser true
if (tieneEdad || tieneDinero)
    Console.WriteLine("Cumple algo al menos"); // SÍ entra, tieneEdad es true

// NOT (!) — Invierte el valor
if (!tieneDinero)
    Console.WriteLine("No tiene dinero 💸");   // SÍ entra

// Combinados — paréntesis para controlar el orden
if ((tieneEdad && tienePermiso) || tieneDinero)
    Console.WriteLine("Puede entrar");         // SÍ entra

//Operador Ternario

int edadTernario = 20;

// If/else normal
string resultado;
if (edadTernario >= 18)
    resultado = "Mayor de edad";
else
    resultado = "Menor de edad";

// Ternario — exactamente lo mismo en una línea
string resultado2 = edadTernario >= 18 ? "Mayor de edad" : "Menor de edad";

Console.WriteLine(resultado2); // Mayor de edadTernario

// Muy usado para mostrar texto condicional
double nota = 75.5;
string estado = nota >= 70 ? "✅ Aprobado" : "❌ Reprobado";
string etiqueta = nota >= 90 ? "Excelente" : "Normal";
bool aprobado = nota >= 70 ? true : false;
// Este último se simplifica así:
bool aprobado2 = nota >= 70; // mismo resultado

Console.WriteLine($"Estado: {estado}");

// Ternario anidado (úsalo con moderación, puede confundir)
string clasificacion = nota >= 90 ? "Excelente"
                     : nota >= 80 ? "Muy bueno"
                     : nota >= 70 ? "Aprobado"
                     : "Reprobado";

Console.WriteLine($"Clasificación: {clasificacion}");


/*///////////////////////////
 Ciclos
///////////////////////////*/
///

//For, While, DoWhile y Foreach


//////////////////////////////////
//FOR
//////////////////////////////////

// Estructura: for (inicio; condición; paso)
//              ↑           ↑           ↑
//         empieza en 0  mientras i<5  súmale 1 cada vez

for (int i = 0; i <= 5; i++)
{
    Console.WriteLine($"Vuelta número: {i}");
}
// Imprime: 0, 1, 2, 3, 4

//Limpiar la consola
Console.Clear();

// Ejemplo real: tabla de multiplicar del 7
Console.WriteLine("=== Tabla del 7 ===");
for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"7 x {i} = {7 * i}");
}

// Recorrer una lista con índice
//Ana, Luis, Sofia
// 0    1     2
List<string> nombres = new List<string> { "Ana", "Luis", "Sofía" };
Console.WriteLine($"La cantidad en la lista es de: {nombres.Count()}");
for (int i = 0; i < nombres.Count; i++)
{
    Console.WriteLine($"Posición {i}: {nombres[i]}");
}

List<string> frutas = new List<string> { "manzana", "banano", "mango" };

//////////////////////////////////
//FOREACH
//////////////////////////////////

// foreach se lee: "para cada fruta en frutas..."
foreach (string fruta in frutas)
{
    Console.WriteLine($"Fruta: {fruta.ToUpper()}");
}

// Con objetos — el más común en APIs reales
List<Estudiante> estudiantes = new List<Estudiante>
{
    new Estudiante { Nombre = "Ana",   Nota = 95 },
    new Estudiante { Nombre = "Luis",  Nota = 62 },
    new Estudiante { Nombre = "Sofía", Nota = 88 },
};

foreach (var estudiante in estudiantes)
{
    string estadoNota = estudiante.Nota >= 70 ? "Aprobado ✅" : "Reprobado ❌";
    Console.WriteLine($"{estudiante.Nombre}: {estudiante.Nota} — {estado}");
}


//////////////////////////////////
//WHILE
//////////////////////////////////

// Mientras la condición sea true, sigue
int intentos = 0;
while (intentos < 3)
{
    Console.WriteLine($"Intento #{intentos + 1}");
    intentos++;
}

// Ejemplo clásico: leer input del usuario hasta que sea válido
string password = "";
while (password != "NetVerk2026")
{
    Console.Write("Ingresa la contraseña: ");
    //Console.ReadLine();
    password = Console.ReadLine();

    if (password != "NetVerk2026")
        Console.WriteLine("❌ Contraseña incorrecta, intenta de nuevo.");
}
Console.WriteLine("✅ Acceso concedido!");

// Contador regresivo
int cuenta = 5;
while (cuenta > 0)
{
    Console.WriteLine($"Lanzando en {cuenta}...");
    cuenta--;
}
Console.WriteLine("🚀 ¡Despegue!");


//////////////////////////////////
//WHILE
//////////////////////////////////
///
// La diferencia: el bloque corre SIEMPRE al menos una vez
int numero1;
do
{
    
    Console.Write("Ingresa un número entre 1 y 10: ");
    numero1 = int.Parse(Console.ReadLine());

    if (numero1 < 1 || numero1 > 10)
        Console.WriteLine("❌ Fuera de rango, intenta de nuevo.");

} while (numero1 < 1 || numero1 > 10);

Console.WriteLine($"✅ Elegiste: {numero1}");


//////////////////////////////////
//BREAK y CONTINUE
//////////////////////////////////

// BREAK — sale del ciclo inmediatamente
Console.WriteLine("=== BREAK: buscar el primer número mayor a 5 ===");
List<int> numeros = new List<int> { 2, 4, 1, 7, 3, 9, 6 };

foreach (var n in numeros)
{
    if (n > 5)
    {
        Console.WriteLine($"Encontré el primero mayor a 5: {n}");
        break; // ← sale del foreach aquí
    }
    Console.WriteLine($"  {n} no es mayor a 5, sigo buscando...");
}

// CONTINUE — salta esta vuelta, pero sigue el ciclo
Console.WriteLine("\n=== CONTINUE: imprimir solo los pares ===");
for (int i = 1; i <= 10; i++)
{
    if (i % 2 != 0)
        continue; // ← salta los impares

    Console.WriteLine($"{i} es par ✓");
}

//////////////////////////////////
//LINQ
//////////////////////////////////

List<int> numerosLinQ = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Forma antigua — con ciclo
int sumaManual = 0;
foreach (var n in numerosLinQ)
{
    if (n % 2 == 0)
        sumaManual += n;
}
Console.WriteLine($"Suma pares (ciclo): {sumaManual}");

// Forma moderna — con LINQ
int sumaLinq = numerosLinQ.Where(n => n % 2 == 0).Sum();
Console.WriteLine($"Suma pares (LINQ) : {sumaLinq}");

// Ambas dan el mismo resultado: 30
// ¿Cuál prefieren? En APIs usaremos LINQ,
// pero hay casos donde el ciclo es más claro.


// Dada esta lista de precios:
List<decimal> precios = new List<decimal>
{
    1500, 800, 3200, 500, 12000, 250, 4500, 900
};

// var precisOrdenados = precios.OrderByDescending(n => n).ToList();
//foreach (var item in precisOrdenados)
//{
//    Console.WriteLine(item);
//}

//Ejercicio 1
// Usando ciclos (NO LINQ todavía), encuentra:
// 1. La suma total de todos los precios
// 2. El precio más alto
// 3. Cuántos precios son mayores a 1000
// 4. Imprime solo los precios menores a 1000 con un mensaje "económico"
// 5. Investiguen y practiquen como hacerlo con LinQ


//Ejercicio 2
/*
Crea las siguientes variables:
- Tu nombre(string)
- Tu edad(int)
- Tu nota promedio de la universidad (double)
- Si estás activo en el curso (bool)
- El costo de tu carrera por semestre (decimal)

Luego imprime en consola una "tarjeta de estudiante" así:

=============================
   TARJETA DE ESTUDIANTE
=============================
Nombre  : Ana García
Edad    : 20 años
Promedio: 88.5
Estado  : Activo
Costo   : ₡185,000.00 por semestre
=============================
*/

//Ejercicio 3
//public class Producto
//{
//    public string Nombre { get; set; }
//    public string Categoria { get; set; }
//    public decimal Precio { get; set; }
//    public int Stock { get; set; }
//    public bool Disponible { get; set; }
//}

//List<Producto> productos = new List<Producto>
//{
//    new Producto { Nombre = "Laptop Dell",     Categoria = "Electrónica", Precio = 850000m,  Stock = 5,  Disponible = true  },
//    new Producto { Nombre = "Mouse Logitech",  Categoria = "Electrónica", Precio = 15000m,   Stock = 20, Disponible = true  },
//    new Producto { Nombre = "Silla Ergonómica",Categoria = "Muebles",     Precio = 120000m,  Stock = 3,  Disponible = true  },
//    new Producto { Nombre = "Monitor LG",      Categoria = "Electrónica", Precio = 320000m,  Stock = 0,  Disponible = false },
//    new Producto { Nombre = "Escritorio",      Categoria = "Muebles",     Precio = 95000m,   Stock = 7,  Disponible = true  },
//    new Producto { Nombre = "Teclado Mecánico",Categoria = "Electrónica", Precio = 45000m,   Stock = 12, Disponible = true  },
//    new Producto { Nombre = "Lámpara LED",     Categoria = "Iluminación", Precio = 8500m,    Stock = 0,  Disponible = false },
//};

//// COMPLETA ESTOS USANDO LINQ:

//// 1. Lista de productos disponibles (Disponible == true)
//var disponibles = /* TU CÓDIGO AQUÍ */;

//// 2. Productos de Electrónica ordenados por precio de menor a mayor
//// Order by
//var electronicaOrdenada = /* TU CÓDIGO AQUÍ */;

//// 3. El producto más caro
//var masCaro = /* TU CÓDIGO AQUÍ */;

//// 4. Precio promedio de todos los productos disponibles
//var precioPromedio = /* TU CÓDIGO AQUÍ */;

//// 5. Nombres de todos los productos sin stock (Stock == 0)
//var sinStock = /* TU CÓDIGO AQUÍ */;

//// 6. ¿Cuántos productos hay por categoría?
//var porCategoria = /* TU CÓDIGO AQUÍ */;


//Clase estudiante
public class Estudiante
{
    public string Nombre { get; set; }
    public int Nota { get; set; }
}
#endregion

