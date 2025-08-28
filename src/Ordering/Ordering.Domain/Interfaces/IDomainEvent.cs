
using MediatR;

namespace  Ordering.Domain.Interfaces;
public interface IDomainEvent : INotification
{
    public Guid EventId => Guid.NewGuid();

    public DateTime CreatedAt => DateTime.Now;
}  