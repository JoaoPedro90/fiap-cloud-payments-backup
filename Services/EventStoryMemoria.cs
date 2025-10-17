using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using PaymentsService.Data;
using PaymentsService.Models;
using PaymentsService.Models.Events;
using PaymentsService.Repositories;

namespace PaymentsService.Services
{
    public class EventStoreMemoria : IEventStoreService
    {
        private readonly EventStoreContext _context;

        public EventStoreMemoria(EventStoreContext context)
        {
            _context = context;
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

            await _context.StoredEvents.AddAsync(storedEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventBase>> ObterEventosAsync(string aggregateId)
        {
            var eventos = await _context.StoredEvents
                .Where(e => e.AggregateId == aggregateId)
                .OrderBy(e => e.Data)
                .ToListAsync();

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
