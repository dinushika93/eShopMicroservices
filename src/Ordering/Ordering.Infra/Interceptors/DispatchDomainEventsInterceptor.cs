using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Interfaces;


public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvents(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private async Task DispatchDomainEvents(DbContext dbContext)
    {
        if (dbContext == null) return;

        var entities = dbContext.ChangeTracker
            .Entries<IEntity>()
            .Where(a => a.Entity.DomainEvents.Any())
            .Select(a => a.Entity).ToList();

        var domainEvents = entities.SelectMany(e => e.DomainEvents).ToList();

        entities.ToList().ForEach(e => e.RemoveDomainEvents());
        
        foreach (var e in domainEvents)
        {
            await mediator.Publish(e);
        }

    }
}