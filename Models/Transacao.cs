//using PaymentsService.Domain.Events;
using PaymentsService.Models.Events;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace PaymentsService.Models
{
    public class Transacao
    {
        public Guid Id { get; set; }
        public string UsuarioId { get; set; }
        public string JogoId { get; set; }
        public decimal Valor { get; set; }
        public string MetodoPagamento { get; set; }
        public string Status { get; set; }

        public DateTime Data { get; set; } = DateTime.UtcNow;


        [NotMapped]
        private readonly List<EventBase> _eventosPendentes = new();

        public IEnumerable<EventBase> EventosPendentes => _eventosPendentes;

        public static Transacao Criar(string usuarioId, string jogoId, decimal valor, string metodo)
        {
            var evento = new PagamentoCriado
            {
                AggregateId = Guid.NewGuid().ToString(),
                Valor = valor
            };

            var transacao = new Transacao();
            transacao.Aplicar(evento);
            return transacao;
        }

        public void Aplicar(EventBase evento)
        {
            switch (evento)
            {
                case PagamentoCriado e:
                    Id = Guid.Parse(e.AggregateId);
                    Valor = e.Valor;
                    Status = "Criado";
                    break;

                case PagamentoProcessado e:
                    Status = e.Status;
                    break;
            }

            _eventosPendentes.Add(evento);
        }
    }
}
