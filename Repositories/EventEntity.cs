namespace PaymentsService.Repositories
{
    public class EventEntity
    {
        public Guid Id { get; set; }
        public string AggregateId { get; set; }
        public string TipoEvento { get; set; }
        public string Dados { get; set; } // JSON do evento
        public DateTime Timestamp { get; set; }
    }
}
