using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Ingresso.API.Services.Contratos;

public interface IIngressosDoEventoService
{
    Task<ResponseResult<bool>> CadastraIngressosDoEvento(CadastrarIngressosDoEventoDto dto);
    Task<ResponseResult<IEnumerable<Domain.IngressosDoEvento>>> ObterIngressosDoEventoDisponiveis();
    Task<ResponseResult<Domain.IngressosDoEvento>> ObterIngressosDoEventoPorEvento(Guid eventoId);
    Task<ResponseResult<Domain.IngressosDoEvento>> ObterIngressosDoEventoPorId(Guid ingressoId);
}
