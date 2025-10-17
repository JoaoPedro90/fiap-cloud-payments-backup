using PaymentsService.DTOs;
using PaymentsService.Models;

namespace PaymentsService.Services
{
    public interface IPagamentoService
    {
        Task<Transacao> ProcessarPagamentoAsync(PagamentoRequestDto pagamento);
    }
}
