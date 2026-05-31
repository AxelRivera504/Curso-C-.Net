using Facturacion.Application.Dtos.Producto;
using FluentValidation;

namespace Facturacion.Application.Validators.Producto
{
    public class UpdateProductoValidatorDto : AbstractValidator<UpdateProductoDto>
    {
        public UpdateProductoValidatorDto()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre del producto es requerido")
                .MaximumLength(150).WithMessage("El nombre del producto no puede superar los 150 caracteres");

            RuleFor(x => x.Categoria)
                .NotEmpty().WithMessage("La categoría del producto es requerida")
                .MaximumLength(100).WithMessage("La categoría no puede superar los 100 caracteres");

            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El precio del producto debe ser mayor a 0");

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock del producto no puede ser negativo");
        }
    }
}
