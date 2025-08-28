using System.Security.Cryptography.X509Certificates;

namespace Ordering.Domain.Models;
public class CustomerId
{
    public Guid Value { get; }

    private CustomerId(Guid value)
    {
        Value = value;
    }

    public static CustomerId Of(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentException("CustomerId cannot be empty.");
        }
        return new CustomerId(value);
    }
}