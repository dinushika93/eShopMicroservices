
using Basket.Api.Dtos;
using FluentValidation;

namespace Basket.Api.features.Basket.DeleteBasket;

public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketDto>
{
   public CheckoutBasketValidator()
   {
     RuleFor(x=>x.UserName).NotEmpty().WithMessage("UserName is required.");
   }
}