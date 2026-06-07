using Facturacion.Application.Dtos.FacturaDetalle;
using FluentValidation;

namespace Facturacion.Application.Validators.FacturaDetalle
{
    public class UpdateFacturaDetalleValidatorDto : AbstractValidator<UpdateFacturaDetalleDto>
    {
        public UpdateFacturaDetalleValidatorDto()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El id del detalle es requerido");

            RuleFor(x => x.ProductoId)
                .GreaterThan(0).WithMessage("El producto es requerido");

            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0");

            RuleFor(x => x.PrecioUnitario)
                .GreaterThan(0).WithMessage("El precio unitario debe ser mayor a 0");
        }
    }
}
