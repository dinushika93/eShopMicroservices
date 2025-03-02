using FluentValidation;

namespace Catalog.Api.features.Product.CreateProduct;

public class CreateProducValidator : AbstractValidator<CreateProductCommand>
{
   public CreateProducValidator()
   {
    RuleFor(cmd => cmd.ProductCreateDto.Name).NotEmpty().WithMessage("Name cannot be empty");
    RuleFor(cmd => cmd.ProductCreateDto.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
   }
}