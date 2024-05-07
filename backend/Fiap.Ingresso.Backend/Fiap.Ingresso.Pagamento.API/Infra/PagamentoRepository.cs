
using Fiap.Ingresso.Pagamento.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Pagamento.API.Infra;
public class PagamentoRepository : IPagamentoRepository
{

    protected readonly PagamentoContext _dbContext;
    protected readonly DbSet<Domain.Pagamento> _dbSet;

    public PagamentoRepository(PagamentoContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<Domain.Pagamento>();
    }

    public async Task GravaPagamento(Domain.Pagamento pagamento)
    {
        _dbContext.Pagamentos.Add(pagamento);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Domain.Pagamento> GetPagamentoById(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(p => p.Id == id);
    }
}
