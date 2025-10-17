using PaymentsService.Models;
using PaymentsService.Models.Events;

namespace PaymentsService.Repositories
{
    public interface IEventStore
    {
        void SalvarEvento(StoredEvent evento);
        IEnumerable<StoredEvent> ObterEventos(string aggregateId);
    }

}
