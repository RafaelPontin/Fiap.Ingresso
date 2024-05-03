using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.Ingresso.API.Infra;
using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
using Fiap.Ingresso.WebAPI.Core.Communication;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Ingresso.Ingresso.API.Services;

public class VendaService : IVendaService
{
    private readonly IVendaRepository _repository;
    private readonly IIngressoRepository _ingressoRepository;

    public VendaService(IVendaRepository repository, IIngressoRepository ingressoRepository)
    {
        _repository = repository;
        _ingressoRepository = ingressoRepository;
    }

    public async Task<ResponseResult<bool>> VenderIngresso(Guid ingressoId, Guid usuarioId)
    {
        try
        {
            var ingresso = await _ingressoRepository.ObterIngressoPorId(ingressoId);

            if (ingresso == null)
            {
                return new ResponseResult<bool>()
                {
                    Erros = new List<string>() { "Ingresso não encontrado" },
                    Data = false,
                    Status = 404
                };
            }

            if (await _repository.BuscaUsuarioPorIngresso(usuarioId, ingressoId) != null)
            {
                return new ResponseResult<bool>()
                {
                    Erros = new List<string>() { "É possivel somente uma compra por usuario" },
                    Data = false,
                    Status = 404
                };
            }

            var venda = ingresso.Vender(usuarioId);

            if (ingresso.Erros.Any() || venda.Erros.Any())
            {
                var erros = new List<string>();
                erros.AddRange(ingresso.Erros);
                erros.AddRange(venda.Erros);

                return new ResponseResult<bool>()
                {
                    Erros = erros,
                    Data = false,
                    Status = 400
                };
            }

            await _repository.VenderIngresso(ingresso, venda);

            return new ResponseResult<bool>() { Data = true, Status = 200 };
        }
        catch (Exception ex)
        {
            return new ResponseResult<bool>() { Data = false, Status = 500 };
        }
    }

    public async Task<ResponseResult<IEnumerable<Domain.Venda>>> ObterHistoricoUsuario(Guid usuarioId)
    {
        try
        {
            var vendas = await _repository.ObterHistoricoUsuario(usuarioId);

            if (vendas == null || !vendas.Any())
            {
                return new ResponseResult<IEnumerable<Domain.Venda>>()
                {
                    Erros = new List<string>() { "Nenhum registro encontrado" },
                    Data = null,
                    Status = 404
                };
            }



            return new ResponseResult<IEnumerable<Domain.Venda>>() { Data = vendas, Status = 200 };

        }
        catch (Exception ex)
        {
            return new ResponseResult<IEnumerable<Domain.Venda>>() { Data = null, Status = 500 };
        }
    }

}
