namespace Fiap.Ingresso.Ingresso.API.Infra.Contratos;

public interface IVendaRepository
{
    Task VenderIngresso(Domain.Ingresso ingresso, Domain.Venda venda);
    Task<Domain.Venda?> BuscaUsuarioPorIngresso(Guid usuarioId, Guid ingressoId);
    Task<IEnumerable<Domain.Venda>> ObterHistoricoUsuario(Guid usuarioId);
}
