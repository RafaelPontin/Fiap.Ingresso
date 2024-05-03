using Fiap.Ingresso.Ingresso.API.Data;
using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Ingresso.API.Infra;

public class VendaRepository : IVendaRepository
{
    protected readonly IngressoContext _dbContext;
    protected readonly DbSet<Domain.Ingresso> _dbSet;

    public VendaRepository(IngressoContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<Domain.Ingresso>();
    }

    public async Task VenderIngresso(Domain.Ingresso ingresso, Domain.Venda venda)
    {
        _dbContext.Ingressos.Update(ingresso);
        await _dbContext.Vendas.AddAsync(ingresso.Vendas[0]);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Domain.Venda?> BuscaUsuarioPorIngresso(Guid usuarioId, Guid ingressoId)
    {
        return await _dbContext.Vendas.FirstOrDefaultAsync(x => x.UsuarioId == usuarioId && x.IngressoId == ingressoId);
    }

    public async Task<IEnumerable<Domain.Venda>> ObterHistoricoUsuario(Guid usuarioId)
    {
        return await _dbContext.Vendas.Where(x => x.UsuarioId == usuarioId).ToListAsync();
    }
}
