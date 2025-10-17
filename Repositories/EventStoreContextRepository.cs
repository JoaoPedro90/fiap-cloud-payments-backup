using Microsoft.EntityFrameworkCore;
using PaymentsService.Models;

namespace PaymentsService.Repositories
{
    public class EventStoreContextRepository : DbContext
    {
        public EventStoreContextRepository(DbContextOptions<EventStoreContextRepository> options) : base(options) { }

        public DbSet<EventEntity> Eventos { get; set; }

        public DbSet<StoredEvent> StoredEvents { get; set; }
    }
}
