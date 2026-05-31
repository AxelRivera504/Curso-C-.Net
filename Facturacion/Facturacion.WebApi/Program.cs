using Facturacion.Application.Interfaces;
using Facturacion.Application.UseCases;
using Facturacion.Application.Validators.Cliente;
using Facturacion.Domain.Interfaces;
using Facturacion.Infrastructure.Context;
using Facturacion.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(c =>
//    {
//        c.SwaggerDoc("V1", new() { 
//            Title = "Facturación Api - Clean Architecture",
//            Version = "V1",
//            Description = "NetVerk - Curso C# .Net"
//        });

//    }
//);
builder.Services.AddSwaggerGen();

//Conexion a base de datos
builder.Services.AddDbContext<FacturacionContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Repositories
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

//Services
builder.Services.AddScoped<IClienteService, ClienteService>();
//builder.Services.AddScoped<IProductoService, ProductoService>();
//builder.Services.AddScoped<IFacturaService, FacturaService>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateClienteValidatorDto>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


//Comando para migraciones
/*
 Les comparto los comandos para ejecutar migraciones.

--Añadir migración indicando el proyecto de infrastructure ya sea Local, Client o Admin.
add-migration IsActiveClient -Project Local.Infrastructure -StartupProject Local.WebApi

--Ejecutar la migración al proyecto indicando el proyecto de infrastructure ya sea Local, Client o Admin.
Update-Database -Project Local.Infrastructure -StartupProject Local.WebApi
 
--Remover la ultima migración creada en el proyecto indicando el proyecto de infrastructure ya sea Local, Client o Admin.
remove-migration -Project Local.Infrastructure -StartupProject Local.WebApi
 
--Recuerden siempre colocar en -StartupProject el proyecto WebApi donde necesiten ejecutar la migración

 */