using Facturacion.Application.Dtos.Factura;
using FluentValidation;

namespace Facturacion.Application.Validators.Factura
{
    public class CreateFacturaValidatorDto : AbstractValidator<CreateFacturaDto>
    {
        public CreateFacturaValidatorDto()
        {
            RuleFor(x => x.ClienteId)
                .GreaterThan(0).WithMessage("El cliente es requerido");

            RuleFor(x => x.FechaFactura)
                .NotEmpty().WithMessage("La fecha de la factura es requerida");

            RuleFor(x => x.Total)
                .GreaterThan(0).WithMessage("El total de la factura debe ser mayor a 0");
        }
    }
}
