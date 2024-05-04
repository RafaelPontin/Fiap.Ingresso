using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Ingresso.API.Services.Contratos;

public interface IIngressoService
{
    Task<ResponseResult<bool>> ComprarIngresso(Guid ingressoId, Guid usuarioId, int quantidade);
    Task<ResponseResult<IEnumerable<Domain.Ingresso>>> ObterHistoricoDeIngressosPorUsuario(Guid usuarioId);
}
