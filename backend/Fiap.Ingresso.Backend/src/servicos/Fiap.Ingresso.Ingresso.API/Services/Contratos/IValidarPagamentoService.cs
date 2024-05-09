namespace Fiap.Ingresso.Ingresso.API.Services.Contratos;

public interface IValidarPagamentoService
{
    Task<bool> ValidarPagamento(Guid pagamento);
}
