using Facturacion.Application.Dtos.Cliente;
using FluentValidation;

namespace Facturacion.Application.Validators.Cliente
{
    public class CreateClienteValidatorDto : AbstractValidator<CreateClienteDto>
    {
        public CreateClienteValidatorDto()
        {
            RuleFor(x => x.Nombre)
                 .NotEmpty().WithMessage("El nombre del cliente es requerido")
                 .MaximumLength(100).WithMessage("El nombre del cliente no puede superar los 100 caracteres");

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage("El email del cliente es requerido")
                 .EmailAddress().WithMessage("El email del cliente es incorrecto");

            RuleFor(x => x.Telefono)
                  .NotEmpty().WithMessage("El telefono del cliente es requerido")
                  .MaximumLength(40).WithMessage("El telefono del cliente no puede superar los 40 caracteres");
        }
    }
}
