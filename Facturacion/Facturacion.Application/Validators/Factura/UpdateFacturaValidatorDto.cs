using Facturacion.Application.Dtos.Factura;
using FluentValidation;

namespace Facturacion.Application.Validators.Factura
{
    public class UpdateFacturaValidatorDto : AbstractValidator<UpdateFacturaDto>
    {
        private static readonly string[] EstadosValidos = { "pendiente", "pagada", "anulada" };

        public UpdateFacturaValidatorDto()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El id de la factura es requerido");

            RuleFor(x => x.ClienteId)
                .GreaterThan(0).WithMessage("El cliente es requerido");

            RuleFor(x => x.FechaFactura)
                .NotEmpty().WithMessage("La fecha de la factura es requerida");

            RuleFor(x => x.Estado)
                .NotEmpty().WithMessage("El estado de la factura es requerido")
                .Must(e => EstadosValidos.Contains(e?.ToLower()))
                .WithMessage("El estado debe ser: pendiente, pagada o anulada");
        }
    }
}
