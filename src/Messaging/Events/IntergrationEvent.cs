namespace Messaging.Events;

public class IntergrationEvent
{
    public Guid id => Guid.NewGuid();
    public DateTime CreatedAt => DateTime.Now;
    public string type => GetType().AssemblyQualifiedName;

}
