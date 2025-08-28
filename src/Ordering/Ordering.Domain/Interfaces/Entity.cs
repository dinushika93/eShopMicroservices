using Ordering.Domain.Interfaces;

public abstract class Entity<T> : IEntity<T>
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public List<IDomainEvent> DomainEvents => _domainEvents;
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    
    public void AddDomainEvents(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public void RemoveDomainEvents()
    {
        _domainEvents.Clear();
    }
}