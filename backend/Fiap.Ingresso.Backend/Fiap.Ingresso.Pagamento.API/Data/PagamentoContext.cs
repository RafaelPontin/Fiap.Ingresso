using domain = Fiap.Ingresso.Pagamento.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Pagamento.API.Data;
public class PagamentoContext : DbContext
{
    public DbSet<domain.Pagamento> Pagamentos { get; set; }
    public PagamentoContext(DbContextOptions<PagamentoContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagamentoContext).Assembly);
    }

}
