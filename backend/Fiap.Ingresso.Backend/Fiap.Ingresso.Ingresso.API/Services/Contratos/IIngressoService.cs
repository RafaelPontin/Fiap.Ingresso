using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Ingresso.API.Services.Contratos;

public interface IIngressoService
{
    Task<ResponseResult<bool>> CadastraIngresso(CadastrarIngressoDto dto);
    Task<ResponseResult<IEnumerable<Domain.Ingresso>>> BuscarIngressosDisponiveis();
    Task<ResponseResult<Domain.Ingresso>> BuscarIngressosPorEvento(Guid eventoId);
}
