namespace Ordering.Domain.Models;

public record OrderId
{
    public Guid Value { get; }

     private OrderId() { } // For EF Core
     
    private OrderId(Guid value) => Value = value;
    public static OrderId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new ArgumentException("OrderId cannot be empty.");
        }

        return new OrderId(value);
    }
}