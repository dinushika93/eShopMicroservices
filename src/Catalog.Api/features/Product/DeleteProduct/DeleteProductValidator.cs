using FluentValidation;

namespace Catalog.Api.features.Product.DeleteProduct;

public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>{

    public DeleteProductValidator()
    {
        RuleFor(cmd => cmd.id).NotEmpty().WithMessage("Id cannot be empty");
    }

}