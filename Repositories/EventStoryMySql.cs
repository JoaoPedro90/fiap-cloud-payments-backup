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

        public void SalvarEvento(EventBase evento)
        {
            var entity = new EventEntity
            {
                Id = evento.Id,
                AggregateId = evento.AggregateId,
                TipoEvento = evento.GetType().Name,
                Dados = System.Text.Json.JsonSerializer.Serialize(evento),
                //Timestamp = evento.Timestamp
            };

            _context.Eventos.Add(entity);
            _context.SaveChanges();
        }

        public IEnumerable<EventBase> ObterEventos(string aggregateId)
        {
            var entities = _context.Eventos
                .Where(e => e.AggregateId == aggregateId)
                .OrderBy(e => e.Timestamp)
                .ToList();

            var eventos = new List<EventBase>();

            foreach (var entity in entities)
            {
                var tipo = Type.GetType($"PaymentsService.Domain.Events.{entity.TipoEvento}");
                if (tipo != null)
                {
                    var evento = (EventBase?)System.Text.Json.JsonSerializer.Deserialize(entity.Dados, tipo);
                    if (evento != null)
                        eventos.Add(evento);
                }
            }

            return eventos;
        }

        
    }
}
