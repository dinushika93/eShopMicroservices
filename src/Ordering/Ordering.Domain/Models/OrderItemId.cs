namespace Ordering.Domain.Models;

public class OrderItemId
{
    public Guid Value {get;}

    private OrderItemId(Guid value)
    {
       Value = value;

    }

    public static OrderItemId Of(Guid value)
    {
        if(value == Guid.Empty)
             throw new ArgumentException("productId cannot be empty.");

        return new OrderItemId(value);
    }
    
    
}