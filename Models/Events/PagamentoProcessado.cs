namespace PaymentsService.Models.Events
{
    public class PagamentoProcessado : EventBase
    {
        public string Status { get; set; }
    }
}
