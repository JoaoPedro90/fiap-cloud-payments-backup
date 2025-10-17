namespace PaymentsService.Models.Events
{
    public class PagamentoCriado : EventBase
    {
        public decimal Valor { get; set; }
    }
}
