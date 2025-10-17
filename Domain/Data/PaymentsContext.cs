using Microsoft.EntityFrameworkCore;
using PaymentsService.Models;

namespace PaymentsService.Domain.Data
{
    public class PaymentsContext : DbContext
    {
        public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ignorar eventos do domínio
            modelBuilder.Ignore<PaymentsService.Models.Events.EventBase>();
            modelBuilder.Ignore<PaymentsService.Models.Events.PagamentoCriado>();
            modelBuilder.Ignore<PaymentsService.Models.Events.PagamentoProcessado>();

            // Configurar decimal corretamente
            modelBuilder.Entity<PaymentsService.Models.Transacao>()
                .Property(t => t.Valor)
                .HasPrecision(10, 2);
        }
        public DbSet<Transacao> Pagamentos { get; set; }
    }
}
