
using FluentValidation;
using FluentValidation.Results;

namespace Basket.Api.features.Basket.DeleteBasket;

public class DeleteBasketValidator : AbstractValidator<Cart>
{
   public DeleteBasketValidator()
   {
     RuleFor(x=>x.UserName).NotEmpty().WithMessage("UserName is required.");
   }
}