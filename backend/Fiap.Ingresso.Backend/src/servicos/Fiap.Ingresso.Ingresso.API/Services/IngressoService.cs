using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Ingresso.API.Services;

public class IngressoService : IIngressoService
{
    private readonly IIngressoRepository _repository;
    private readonly IIngressosDoEventoRepository _ingressoRepository;

    public IngressoService(IIngressoRepository repository, IIngressosDoEventoRepository ingressoRepository)
    {
        _repository = repository;
        _ingressoRepository = ingressoRepository;
    }

    public async Task<ResponseResult<bool>> ComprarIngresso(Guid ingressoId, Guid usuarioId, int quantidade)
    {
        try
        {
            var ingresso = await _ingressoRepository.ObterIngressosDoEventoPorId(ingressoId);

            if (ingresso == null)
            {
                return new ResponseResult<bool>()
                {
                    Erros = new List<string>() { "Ingresso não encontrado" },
                    Data = false,
                    Status = 404
                };
            }

            var vendas = ingresso.ComprarIngressosDoEvento(usuarioId, quantidade);

            foreach (var venda in vendas)
            {
                ingresso.Erros.AddRange(venda.Erros);
            }

            if (ingresso.Erros.Any())
            {
                var erros = new List<string>();
                erros.AddRange(ingresso.Erros);

                return new ResponseResult<bool>()
                {
                    Erros = erros,
                    Data = false,
                    Status = 400
                };
            }

            await _repository.ComprarIngressos(ingresso, vendas);

            return new ResponseResult<bool>() { Data = true, Status = 201 };
        }
        catch (Exception ex)
        {
            return new ResponseResult<bool>() { Data = false, Status = 500 };
        }
    }

    public async Task<ResponseResult<IEnumerable<Domain.Ingresso>>> ObterHistoricoDeIngressosPorUsuario(Guid usuarioId)
    {
        try
        {
            var vendas = await _repository.ObterHistoricoDeIngressosPorUsuario(usuarioId);

            if (vendas == null || !vendas.Any())
            {
                return new ResponseResult<IEnumerable<Domain.Ingresso>>()
                {
                    Erros = new List<string>() { "Nenhum registro encontrado" },
                    Data = null,
                    Status = 404
                };
            }



            return new ResponseResult<IEnumerable<Domain.Ingresso>>() { Data = vendas, Status = 200 };

        }
        catch (Exception ex)
        {
            return new ResponseResult<IEnumerable<Domain.Ingresso>>() { Data = null, Status = 500 };
        }
    }

}
