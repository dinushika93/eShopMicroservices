namespace Ordering.Domain.Models;

public class    OrderId
{
    public Guid Value {get;}

    private OrderId(Guid value)
    {
       Value = value;

    }

    public static OrderId Of(Guid value)
    {
        if(value == Guid.Empty)
             throw new ArgumentException("orderId cannot be empty.");

        return new OrderId(value);
    }
    
    
}