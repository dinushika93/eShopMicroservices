namespace Ordering.Domain.Interfaces;

public interface IEntity<T> : IEntity
{

}

public interface IEntity
{
    public List<IDomainEvent> DomainEvents { get; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

    public void AddDomainEvents(IDomainEvent domainEvent);

    public void RemoveDomainEvents();
}