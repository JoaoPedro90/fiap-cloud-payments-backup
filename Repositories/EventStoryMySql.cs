using PaymentsService.Models.Events;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PaymentsService.Models;

namespace PaymentsService.Repositories
{

    public class EventStoreMySql : IEventStore
    {
        private readonly EventStoreContextRepository _context;

        public EventStoreMySql(EventStoreContextRepository context)
        {
            _context = context;
        }

        public void SalvarEvento(StoredEvent evento)
        {
            var entity = new StoredEvent
            {
                Id = evento.Id,
                AggregateId = evento.AggregateId,
                TipoEvento = evento.GetType().Name,
                Dados = System.Text.Json.JsonSerializer.Serialize(evento),
                Timestamp = evento.Timestamp
            };

            _context.StoredEvents.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<StoredEvent> ObterEventos(string aggregateId)
        {
            var entities = _context.StoredEvents
                .Where(e => e.AggregateId == aggregateId)
                .OrderBy(e => e.Timestamp)
                .ToList();

            var eventos = new List<StoredEvent>();

            foreach (var entity in entities)
            {
                var tipo = Type.GetType($"PaymentsService.Domain.Events.{entity.TipoEvento}");
                if (tipo != null)
                {
                    var evento = (StoredEvent?)System.Text.Json.JsonSerializer.Deserialize(entity.Dados, tipo);
                    if (evento != null)
                        eventos.Add(evento);
                }
            }

            return eventos;
        }

        
    }
}
