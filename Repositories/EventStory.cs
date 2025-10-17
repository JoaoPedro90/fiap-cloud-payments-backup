using PaymentsService.Models.Events;

namespace PaymentsService.Repositories
{
    public class EventStore : IEventStore
    {
        private readonly List<EventBase> _eventos = new();

        public void SalvarEvento(EventBase evento)
            => _eventos.Add(evento);

        public IEnumerable<EventBase> ObterEventos(string aggregateId)
            => _eventos.Where(e => e.AggregateId == aggregateId);
    }
}
