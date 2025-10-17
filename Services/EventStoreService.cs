using PaymentsService.Models;
using PaymentsService.Models.Events;
using PaymentsService.Repositories;
using System.Text.Json;

namespace PaymentsService.Services
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreRepository _repository;

        public EventStoreService(IEventStoreRepository repository)
        {
            _repository = repository;
        }

        public async Task SalvarEventoAsync(EventBase evento)
        {
            var storedEvent = new StoredEvent
            {
                Id = evento.Id,
                AggregateId = evento.AggregateId,
                Tipo = evento.Tipo,
                Dados = JsonSerializer.Serialize(evento),
                Data = evento.Data
            };

            await _repository.AddAsync(storedEvent);
        }

        public async Task<IEnumerable<EventBase>> ObterEventosAsync(string aggregateId)
        {
            var eventos = await _repository.GetByAggregateIdAsync(aggregateId);
            var lista = new List<EventBase>();

            foreach (var e in eventos)
            {
                var tipoCompleto = typeof(EventBase).Assembly.GetTypes()
                    .FirstOrDefault(t => t.Name == e.Tipo);

                if (tipoCompleto != null)
                {
                    var evento = (EventBase)JsonSerializer.Deserialize(e.Dados, tipoCompleto);
                    lista.Add(evento);
                }
            }

            return lista;
        }
    }
}
