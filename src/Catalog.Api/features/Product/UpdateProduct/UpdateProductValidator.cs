
using FluentValidation;

namespace Catalog.Api.features.Product.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
  public UpdateProductValidator()
  {
    RuleFor(cmd => cmd.ProductDto.Id).NotEmpty().WithMessage("Id cannot be empty");
    RuleFor(cmd => cmd.ProductDto.Name).NotEmpty().WithMessage("Name cannot be empty");
    RuleFor(cmd => cmd.ProductDto.Price).GreaterThan(0).WithMessage("Price should be greater than 0");
  }

}