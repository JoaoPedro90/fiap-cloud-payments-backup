using Microsoft.EntityFrameworkCore;
using PaymentsService.Models;

namespace PaymentsService.Data
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(DbContextOptions<EventStoreContext> options)
            : base(options)
        {
        }

        public DbSet<StoredEvent> StoredEvents { get; set; }
    }
}
