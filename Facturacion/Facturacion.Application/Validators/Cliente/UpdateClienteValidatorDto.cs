using Facturacion.Application.Dtos.Cliente;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Application.Validators.Cliente
{
    internal class UpdateClienteValidatorDto : AbstractValidator<UpdateClienteDto>
    {
        public UpdateClienteValidatorDto()
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
