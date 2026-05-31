using Facturacion.Application.Dtos.Cliente;
using Facturacion.Domain.Entities;

namespace Facturacion.Application.Extensions
{
    public static class ClienteMappingExtensions
    {
        public static ClienteDto ToDto(this Cliente c) => new ClienteDto()
        {
            Id = c.Id,
            Nombre = c.Nombre,
            Email = c.Email,
            Telefono = c.Telefono,
            Activo = c.Activo,
        };

        public static Cliente ToEntity(this CreateClienteDto c) => new Cliente()
        {
            Nombre = c.Nombre,
            Email = c.Email,
            Telefono = c.Telefono,
        };

        public static Cliente ToEntity(this UpdateClienteDto c) => new Cliente()
        {
            Nombre = c.Nombre,
            Email = c.Email,
            Telefono = c.Telefono,
        };
    }
}
