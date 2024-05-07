using Fiap.Ingresso.Pagamento.API.Domain.Enum;
using Fiap.Ingresso.Pagamento.API.DTOs;
using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Pagamento.API.Services.Contracts;
public interface IPagamentoService
{
    Task<ResponseResult<Guid>> CadastraEvento(CadastrarPagamento pagamentoDto);
    Task<ResponseResult<EPagamento>> GetPagamentoById(Guid id);
    Task<ResponseResult<string>> GetLinhaDigitavel(Guid id);
}
