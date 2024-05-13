using Fiap.Ingresso.Evento.API.DTOs;
using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Evento.API.Services.Contracts;

public interface IEventoService
{
    Task<ResponseResult<bool>> CadastraEvento(CadastraEventoDto dto);
    Task<ResponseResult<bool>> AlterarEvento(AlterarEventoDTO dto);
    Task<ResponseResult<IList<EventoDTO>>> ListarEvento();
    Task<ResponseResult<Guid>> CancelaEvento(Guid id);
    Task<ResponseResult<Domain.Evento>> GetById(Guid Id);
}
