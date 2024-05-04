using Fiap.Ingresso.Ingresso.API.Data;
using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Ingresso.API.Infra;

public class IngressosDoEventoRepository : IIngressosDoEventoRepository
{
    protected readonly IngressoContext _dbContext;
    protected readonly DbSet<Domain.IngressosDoEvento> _dbSet;

    public IngressosDoEventoRepository(IngressoContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<Domain.IngressosDoEvento>();
    }

    public async Task CadastraIngressosDoEvento(Domain.IngressosDoEvento ingressosDoEvento)
    {
        await _dbContext.IngressosDosEventos.AddAsync(ingressosDoEvento);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Domain.IngressosDoEvento?> ObterIngressosDoEventoPorEvento(Guid eventoId)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.EventoId == eventoId);
    }

    public async Task<Domain.IngressosDoEvento?> ObterIngressosDoEventoPorId(Guid ingressosDoEventoId)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id == ingressosDoEventoId);
    }

    public async Task<IEnumerable<Domain.IngressosDoEvento?>> ObterIngressosDoEventoDisponiveis()
    {
        return await _dbSet.Where(x => x.Disponiveis > 0 && x.Ativo == true && x.DataFim >= DateTime.Now).ToListAsync();
    }
}