namespace Fiap.Ingresso.Ingresso.API.Infra.Contratos;

public interface IIngressoRepository
{
    Task ComprarIngressos(Domain.IngressosDoEvento ingressosDoEvento, IEnumerable<Domain.Ingresso> ingressos);
    Task<IEnumerable<Domain.Ingresso>> ObterHistoricoDeIngressosPorUsuario(Guid usuarioId);
}
