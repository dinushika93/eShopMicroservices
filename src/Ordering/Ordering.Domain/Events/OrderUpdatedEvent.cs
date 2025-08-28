using Ordering.Domain.Interfaces;
using Ordering.Domain.Models;

namespace Ordering.Domain.Events;

public record OrderUpdatedEvent(Order order) : IDomainEvent;