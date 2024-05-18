using domain = Fiap.Ingresso.Pagamento.API.Domain;

namespace Fiap.Ingresso.Pagamento.API.Infra;

public interface IPagamentoRepository
{
    Task GravaPagamento(domain.Pagamento pagamento);
    Task<Domain.Pagamento> GetPagamentoById(Guid id);
}
