using Fiap.Ingresso.Ingresso.API.Data;
using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Ingresso.API.Infra;

public class IngressoRepository : IIngressoRepository
{
    protected readonly IngressoContext _dbContext;
    protected readonly DbSet<Domain.Ingresso> _dbSet;

    public IngressoRepository(IngressoContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<Domain.Ingresso>();
    }

    public async Task CadastraIngresso(Domain.Ingresso ingresso)
    {
        await _dbContext.Ingressos.AddAsync(ingresso);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Domain.Ingresso?> ObterIngressosPorEvento(Guid eventoId)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.EventoId == eventoId);
    }

    public async Task<Domain.Ingresso?> ObterIngressoPorId(Guid ingressoId)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == ingressoId);
    }

    public async Task<IEnumerable<Domain.Ingresso?>> ObterIngressosDisponiveis()
    {
        return await _dbSet.Where(x => x.Disponiveis > 0 && x.Ativo == true && x.DataFim >= DateTime.Now).ToListAsync();
    }
}