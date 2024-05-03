using Fiap.Ingresso.Ingresso.API.Domain;
using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Ingresso.API.Services;

public class IngressoService : IIngressoService
{
    private readonly IIngressoRepository _repository;

    public IngressoService(IIngressoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseResult<bool>> CadastraIngresso(CadastrarIngressoDto dto)
    {
        try
        {
            var ingresso = new Domain.Ingresso(dto.EventoId, dto.Total, dto.Disponiveis, dto.Preco, dto.DataFim);

            if (ingresso.Erros.Any())
            {
                return new ResponseResult<bool>()
                {
                    Erros = ingresso.Erros,
                    Data = false,
                    Status = 400
                };
            }

            await _repository.CadastraIngresso(ingresso);

            return new ResponseResult<bool>() { Data = true, Status = 201 };

        }
        catch (Exception ex)
        {
            return new ResponseResult<bool>() { Data = false, Status = 500 };
        }
    }

    public async Task<ResponseResult<IEnumerable<Domain.Ingresso>>> BuscarIngressosDisponiveis()
    {
        try
        {
            var ingressos = await _repository.ObterIngressosDisponiveis();

            if (ingressos == null || !ingressos.Any())
            {
                return new ResponseResult<IEnumerable<Domain.Ingresso>>()
                {
                    Erros = new List<string>() { "Nenhum Ingresso Disponível" },
                    Data = null,
                    Status = 404
                };
            }

            return new ResponseResult<IEnumerable<Domain.Ingresso>>() { Data = ingressos, Status = 200 };

        }
        catch (Exception ex)
        {
            return new ResponseResult<IEnumerable<Domain.Ingresso>>() { Data = null, Status = 500 };
        }
    }

    public async Task<ResponseResult<Domain.Ingresso>> BuscarIngressosPorEvento(Guid eventoId)
    {
        try
        {
            var ingresso = await _repository.ObterIngressosPorEvento(eventoId);

            if (ingresso == null)
            {
                return new ResponseResult<Domain.Ingresso>()
                {
                    Erros = new List<string>() { "Nenhum Ingresso Encontrado" },
                    Data = null,
                    Status = 404
                };
            }

            return new ResponseResult<Domain.Ingresso>() { Data = ingresso, Status = 200 };

        }
        catch (Exception ex)
        {
            return new ResponseResult<Domain.Ingresso>() { Data = null, Status = 500 };
        }
    }
}
