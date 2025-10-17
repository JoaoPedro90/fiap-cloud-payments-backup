namespace PaymentsService.Models.Events
{

    public abstract class EventBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public string AggregateId { get; set; }
        public string Tipo => GetType().Name;
    }


}
