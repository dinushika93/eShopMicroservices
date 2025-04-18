using FluentValidation;

public class SaveBasketValidator : AbstractValidator<Cart>
{
 public SaveBasketValidator()
 {
    RuleFor(x=>x.UserName).NotEmpty().WithMessage("UserName is required");
    RuleFor(x=>x.Items).NotNull().WithMessage("Cart cannot be empty");
 }
} 