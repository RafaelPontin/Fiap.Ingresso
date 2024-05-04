using Fiap.Ingresso.Ingresso.API.Data;
using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Ingresso.API.Infra;

public class IngressoRepository : IIngressoRepository
{
    protected readonly IngressoContext _dbContext;
    protected readonly DbSet<Domain.IngressosDoEvento> _dbSet;

    public IngressoRepository(IngressoContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<Domain.IngressosDoEvento>();
    }

    public async Task ComprarIngressos(Domain.IngressosDoEvento ingressosDoEvento, IEnumerable<Domain.Ingresso> ingressos)
    {
        _dbContext.IngressosDosEventos.Update(ingressosDoEvento);
        foreach (var ingresso in ingressos)
        {
            await _dbContext.Ingressos.AddAsync(ingresso);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Ingresso>> ObterHistoricoDeIngressosPorUsuario(Guid usuarioId)
    {
        return await _dbContext.Ingressos.Where(x => x.UsuarioId == usuarioId).ToListAsync();
    }
}
