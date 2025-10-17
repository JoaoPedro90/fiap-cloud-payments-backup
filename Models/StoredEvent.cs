namespace PaymentsService.Models
{
    public class StoredEvent
    {
        public Guid Id { get; set; }
        public string AggregateId { get; set; }
        public string Tipo { get; set; }
        public string Dados { get; set; }
        public DateTime Data { get; set; }
    }
}
