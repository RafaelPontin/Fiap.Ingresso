using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Ingresso.API.Services.Contratos;

public interface IVendaService
{
    Task<ResponseResult<bool>> VenderIngresso(Guid ingressoId, Guid usuarioId);
    Task<ResponseResult<IEnumerable<Domain.Venda>>> ObterHistoricoUsuario(Guid usuarioId);
}
