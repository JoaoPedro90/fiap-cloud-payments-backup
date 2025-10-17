namespace PaymentsService.Repositories;
using PaymentsService.Models.Events;

public interface IEventStoreService
{
    Task SalvarEventoAsync(EventBase evento);
    Task<IEnumerable<EventBase>> ObterEventosAsync(string aggregateId);
}
