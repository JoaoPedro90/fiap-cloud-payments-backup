using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentsService.Models
{
    [Table("Eventos")]
    public class StoredEvent
    {
        public Guid Id { get; set; }
        public string AggregateId { get; set; }
        public string TipoEvento { get; set; }
        public string Dados { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
