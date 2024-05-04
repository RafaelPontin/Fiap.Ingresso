
namespace Fiap.Ingresso.Ingresso.API.Infra.Contratos;

public interface IIngressosDoEventoRepository
{
    Task CadastraIngressosDoEvento(Domain.IngressosDoEvento ingressosDoEvento);
    Task<Domain.IngressosDoEvento?> ObterIngressosDoEventoPorEvento(Guid eventoId);
    Task<Domain.IngressosDoEvento?> ObterIngressosDoEventoPorId(Guid ingressosDoEventoId);
    Task<IEnumerable<Domain.IngressosDoEvento?>> ObterIngressosDoEventoDisponiveis();
}
