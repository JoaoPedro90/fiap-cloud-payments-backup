using PaymentsService.Models;

namespace PaymentsService.Repositories
{
    public interface IEventStoreRepository
    {
        Task AddAsync(StoredEvent evento);
        Task<List<StoredEvent>> GetByAggregateIdAsync(string aggregateId);
    }
}
