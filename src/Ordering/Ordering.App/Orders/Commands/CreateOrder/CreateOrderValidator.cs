using FluentValidation;

namespace Ordering.App.Orders.Commands.CreateOrder;
 
public class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderValidator()
    {

        RuleFor(x => x.order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        RuleFor(x => x.order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
	}
}