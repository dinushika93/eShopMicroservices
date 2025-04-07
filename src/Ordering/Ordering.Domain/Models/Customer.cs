namespace Ordering.Domain.Models;
public class Customer
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = default!;
    public string Email { get; private set; } = default!;
}