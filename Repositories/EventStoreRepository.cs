using Microsoft.EntityFrameworkCore;
using PaymentsService.Models;

namespace PaymentsService.Repositories
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreContextRepository _context;

        public EventStoreRepository(EventStoreContextRepository context)
        {
            _context = context;
        }

        public async Task AddAsync(StoredEvent evento)
        {
            await _context.StoredEvents.AddAsync(evento);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StoredEvent>> GetByAggregateIdAsync(string aggregateId)
        {
            return await _context.StoredEvents
                .Where(e => e.AggregateId == aggregateId)
                .OrderBy(e => e.Data)
                .ToListAsync();
        }
    }
}
    