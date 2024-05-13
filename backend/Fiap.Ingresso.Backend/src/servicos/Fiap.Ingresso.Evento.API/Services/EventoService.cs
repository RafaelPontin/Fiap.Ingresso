using Fiap.Ingresso.Evento.API.DTOs;
using Fiap.Ingresso.Evento.API.Infra;
using Fiap.Ingresso.Evento.API.Services.Contracts;
using Fiap.Ingresso.WebAPI.Core.Communication;
using System.Runtime.Intrinsics.Arm;

namespace Fiap.Ingresso.Evento.API.Services;

public class EventoService : IEventoService
{
    private readonly IEventoRepository _repository;

    public EventoService(IEventoRepository repository)
    {
        _repository = repository;        
    }

    public async Task<ResponseResult<bool>> CadastraEvento(CadastraEventoDto dto)
    {
        try
        {
            var evento = new Domain.Evento();

            evento.AdicionarEvento(
                                    dto.Nome,
                                    dto.DataInicio,
                                    dto.DataFim,
                                    dto.DataEvento,
                                    dto.PublicoMaximo,
                                    dto.Logradouro,
                                    dto.Numero,
                                    dto.Cidade,
                                    dto.Bairro,
                                    dto.Cep,
                                    dto.Valor
                                    );

            if (!string.IsNullOrWhiteSpace(dto.SiteEvento)) evento.SetUrlEvento(dto.SiteEvento);
            if (!string.IsNullOrWhiteSpace(dto.Descricao)) evento.SetDescricao(dto.Descricao);

            if (evento.Erros.Any())
            {
                return new ResponseResult<bool>()
                {
                    Erros = evento.Erros,
                    Data = false,
                    Status = 400
                };
            }

           await _repository.CadastraEvento(evento);
           
           return new ResponseResult<bool>() { Data = true,  Status = 201 };
           
        }
        catch(Exception ex)
        {
            return new ResponseResult<bool>() { Data = false, Status = 500 };
        }
    }

    public async Task<ResponseResult<bool>> AlterarEvento(AlterarEventoDTO dto)
    {
        try
        {
            var evento = new Domain.Evento();

            evento.AlterarEvento(
                                    dto.Id,
                                    dto.Nome,
                                    dto.DataInicio,
                                    dto.DataFim,
                                    dto.DataEvento,
                                    dto.PublicoMaximo,
                                    dto.Logradouro,
                                    dto.Numero,
                                    dto.Cidade,
                                    dto.Bairro,
                                    dto.Cep,
                                    dto.Valor,
                                    dto.Descricao,
                                    dto.SiteEvento
                                    );

            if (evento.Erros.Any())
            {
                return new ResponseResult<bool>()
                {
                    Erros = evento.Erros,
                    Data = false,
                    Status = 400
                };
            }

            await _repository.AlterarEvento(evento);

            return new ResponseResult<bool>() { Data = true, Status = 201 };

        }
        catch (Exception ex)
        {
            return new ResponseResult<bool>() { Data = false, Status = 500 };
        }
    }

    public async Task<ResponseResult<Guid>> CancelaEvento(Guid id)
    {
        var evento = await _repository.GetEventoById(id);

        if(evento is null)
        {
            return new ResponseResult<Guid>()
            {
                Data = Guid.Empty,
                Status = 404
            };
        }

        evento.SetAtivo(false);

        await _repository.AlterarEvento(evento);

        return new ResponseResult<Guid>()
        {
            Data = id,
            Status = 200
        };

    }

    public async Task<ResponseResult<IList<EventoDTO>>> ListarEvento()
    {
        try
        {
            var eventos = await _repository.GetEventos();
            
            IList<EventoDTO> listEventos = new List<EventoDTO>();   

            foreach(var evento in eventos) 
            {
                var eventosDto = new EventoDTO();
                eventosDto.ConvertToEventoDto(evento);
                listEventos.Add(eventosDto);
            }

            return new ResponseResult<IList<EventoDTO>>() { Status = 200, Data = listEventos };

        }
        catch (Exception ex)
        {
            return new ResponseResult<IList<EventoDTO>>() { Status = 500 };
        }
    }

    public async Task<ResponseResult<Domain.Evento>> GetById(Guid Id)
    {
        try
        {
            var eventos = await _repository.GetEventoById(Id);
            if (eventos is null) return  new ResponseResult<Domain.Evento>() { Status = 404 };
            return new ResponseResult<Domain.Evento>()
            {
                Status = 200,
                Data = eventos
            };
        }
        catch (Exception e)
        {
            return new ResponseResult<Domain.Evento>() { Status = 500 };
        }
    }
}
