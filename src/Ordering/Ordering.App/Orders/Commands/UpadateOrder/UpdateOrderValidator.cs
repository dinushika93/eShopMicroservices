using FluentValidation;

namespace Ordering.App.Orders.Commands.UpdateOrder;
 
public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {

        RuleFor(x => x.order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        RuleFor(x => x.order.OrderItems).NotEmpty().WithMessage("OrderItems should not be empty");
	}
}