using Facturacion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Facturacion.Infrastructure.Configs
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // -1. Nombre de la tabla
            builder.ToTable("Clientes");
            //le decimos a EF: "La clase cliente se va a convertir en una
            //tabla llamada Clientes en SQL Server
            //Sino se indicada esta prop tomará el nombre del DbSet.

            // -2. Llave primaria
            builder.HasKey(x => x.Id);
            //El campo id es la primary key de nuestra tabla de clientes
            //En Sql esto crearia algo asi: Id INT NOT NULL PRIMERY KEY

            // -3. Campo Nombre
            builder.Property(x => x.Nombre)
                .IsRequired() //El campo nombre no puede ser null
                .HasMaxLength(100);//El maximo de caracteres de este campo es de 100

            // -4. Campo Email
            builder.Property(x => x.Email)
                .IsRequired() //El campo nombre no puede ser null
                .HasMaxLength(150);//El maximo de caracteres de este campo es de 150

            // -5. Campo Telefono
            builder.Property(x => x.Email)
                .HasMaxLength(20);//El maximo de caracteres de este campo es de 20

            // -6. Campo Activo
            builder.Property(x => x.Activo)
                .IsRequired()
                .HasDefaultValue(true);//El maximo de caracteres de este campo es de 20
        }
    }

    /*
     CREATE TABLE Clientes (
        Id        INT           NOT NULL IDENTITY PRIMARY KEY,
        Nombre    NVARCHAR(100) NOT NULL,
        Email     NVARCHAR(150) NOT NULL,
        Telefono  NVARCHAR(20)  NULL,
        Activo    BIT           NOT NULL DEFAULT (1)
    )
    */
}
