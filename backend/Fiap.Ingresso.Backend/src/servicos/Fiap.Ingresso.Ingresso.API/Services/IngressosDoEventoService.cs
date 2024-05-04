using Fiap.Ingresso.Ingresso.API.Domain;
using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Ingresso.API.Services;

public class IngressosDoEventoService : IIngressosDoEventoService
{
    private readonly IIngressosDoEventoRepository _repository;

    public IngressosDoEventoService(IIngressosDoEventoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseResult<bool>> CadastraIngressosDoEvento(CadastrarIngressosDoEventoDto dto)
    {
        try
        {
            var ingresso = new Domain.IngressosDoEvento(dto.EventoId, dto.Total, dto.Disponiveis, dto.Preco, dto.DataFim);

            if (ingresso.Erros.Any())
            {
                return new ResponseResult<bool>()
                {
                    Erros = ingresso.Erros,
                    Data = false,
                    Status = 400
                };
            }

            await _repository.CadastraIngressosDoEvento(ingresso);

            return new ResponseResult<bool>() { Data = true, Status = 201 };

        }
        catch (Exception ex)
        {
            return new ResponseResult<bool>() { Data = false, Status = 500 };
        }
    }

    public async Task<ResponseResult<IEnumerable<Domain.IngressosDoEvento>>> ObterIngressosDoEventoDisponiveis()
    {
        try
        {
            var ingressos = await _repository.ObterIngressosDoEventoDisponiveis();

            if (ingressos == null || !ingressos.Any())
            {
                return new ResponseResult<IEnumerable<Domain.IngressosDoEvento>>()
                {
                    Erros = new List<string>() { "Nenhum Ingresso Disponível" },
                    Data = null,
                    Status = 404
                };
            }

            return new ResponseResult<IEnumerable<Domain.IngressosDoEvento>>() { Data = ingressos, Status = 200 };

        }
        catch (Exception ex)
        {
            return new ResponseResult<IEnumerable<Domain.IngressosDoEvento>>() { Data = null, Status = 500 };
        }
    }

    public async Task<ResponseResult<Domain.IngressosDoEvento>> ObterIngressosDoEventoPorEvento(Guid eventoId)
    {
        try
        {
            var ingresso = await _repository.ObterIngressosDoEventoPorEvento(eventoId);

            if (ingresso == null)
            {
                return new ResponseResult<Domain.IngressosDoEvento>()
                {
                    Erros = new List<string>() { "Nenhum Ingresso Encontrado" },
                    Data = null,
                    Status = 404
                };
            }

            return new ResponseResult<Domain.IngressosDoEvento>() { Data = ingresso, Status = 200 };

        }
        catch (Exception ex)
        {
            return new ResponseResult<Domain.IngressosDoEvento>() { Data = null, Status = 500 };
        }
    }

    public async Task<ResponseResult<Domain.IngressosDoEvento>> ObterIngressosDoEventoPorId(Guid ingressoId)
    {
        try
        {
            var ingresso = await _repository.ObterIngressosDoEventoPorId(ingressoId);

            if (ingresso == null)
            {
                return new ResponseResult<Domain.IngressosDoEvento>()
                {
                    Erros = new List<string>() { "Nenhum Ingresso Encontrado" },
                    Data = null,
                    Status = 404
                };
            }

            return new ResponseResult<Domain.IngressosDoEvento>() { Data = ingresso, Status = 200 };

        }
        catch (Exception ex)
        {
            return new ResponseResult<Domain.IngressosDoEvento>() { Data = null, Status = 500 };
        }
    }
}
