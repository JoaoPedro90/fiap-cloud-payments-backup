using PaymentsService.Domain.Data;
using PaymentsService.DTOs;
using PaymentsService.Models;
using PaymentsService.Models.Events;
using PaymentsService.Repositories;
using Stripe;
using System.Globalization;

namespace PaymentsService.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly PaymentsContext _paymentsContext;
        private readonly IEventStoreService _eventStoreService;

        public PagamentoService(PaymentsContext paymentsContext, IEventStoreService eventStoreService)
        {
            _paymentsContext = paymentsContext;
            _eventStoreService = eventStoreService;
        }

        public async Task<Transacao> ProcessarPagamentoAsync(PagamentoRequestDto pagamento)
        {
            // 🔹 Criação do pagamento na Stripe
            var options = new ChargeCreateOptions
            {
                Amount = (long)(pagamento.Valor * 100),
                Currency = "usd",
                Source = "tok_visa", // Token de teste
                Description = $"Pagamento do jogo {pagamento.JogoId} para usuário {pagamento.UsuarioId}"
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);

            var status = charge.Status == "succeeded" ? "Aprovado" : "Recusado";

            // 🔹 Criação da transação
            var transacao = new Transacao
            {
                UsuarioId = pagamento.UsuarioId,
                JogoId = pagamento.JogoId,
                Valor = pagamento.Valor,
                MetodoPagamento = "Stripe",
                Status = status,
                Data = DateTime.UtcNow
            };

            // 🔹 Salva a transação no banco de dados
            //System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            _paymentsContext.Pagamentos.Add(transacao);
            await _paymentsContext.SaveChangesAsync();

            // 🔹 Criação e persistência do evento
            var evento = new PagamentoCriado
            {
                AggregateId = transacao.Id.ToString(),
                Valor = transacao.Valor,
                Data = DateTime.UtcNow
            };

            await _eventStoreService.SalvarEventoAsync(evento);

            return transacao;
        }
    }
}
