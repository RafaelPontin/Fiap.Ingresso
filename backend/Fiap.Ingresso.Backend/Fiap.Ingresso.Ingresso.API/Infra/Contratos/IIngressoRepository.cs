
namespace Fiap.Ingresso.Ingresso.API.Infra.Contratos;

public interface IIngressoRepository
{
    Task CadastraIngresso(Domain.Ingresso ingresso);
    Task<Domain.Ingresso?> ObterIngressosPorEvento(Guid eventoId);
    Task<Domain.Ingresso?> ObterIngressoPorId(Guid ingressoId);
    Task<IEnumerable<Domain.Ingresso?>> ObterIngressosDisponiveis();
}
