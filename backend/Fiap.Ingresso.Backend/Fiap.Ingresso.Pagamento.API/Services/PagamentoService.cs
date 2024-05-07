using Fiap.Ingresso.Pagamento.API.Domain.Enum;
using Fiap.Ingresso.Pagamento.API.DTOs;
using Fiap.Ingresso.Pagamento.API.Infra;
using Fiap.Ingresso.Pagamento.API.Services.Contracts;
using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Pagamento.API.Services;
public class PagamentoService : IPagamentoService
{
    private readonly IPagamentoRepository _respository;
    public PagamentoService(IPagamentoRepository repository)
    {
        _respository = repository;
    }

    public async Task<ResponseResult<Guid>> CadastraEvento(CadastrarPagamento pagamentoDto)
    {
        var pagamento = pagamentoDto.ConvertToPagamento();
       

        if (pagamento.Erros.Any())
        {
            return new ResponseResult<Guid>() 
            {
                Erros = pagamento.Erros,
                Data = Guid.Empty,
                Status = 400
            };
        }
       
        await _respository.GravaPagamento(pagamento);

        return new ResponseResult<Guid>()
        {
            Data = pagamento.Id,
            Status = 201
        };
    }

    public async Task<ResponseResult<EPagamento>> GetPagamentoById(Guid id)
    {
        var pagamento = await _respository.GetPagamentoById(id);
        
        if(pagamento == null)
        {
            return new ResponseResult<EPagamento>()
            {
                Status = 404
            };
        }

        return new ResponseResult<EPagamento>()
        {
            Data = pagamento.TipoPagamento,
            Status = 200
        };
    } 


    public async Task<ResponseResult<string>> GetLinhaDigitavel(Guid id)
    {
        var pagamento = await _respository.GetPagamentoById(id);

        if(pagamento == null)
        {
            return new ResponseResult<string>()
            {
                Status = 404
            };
        }
        else if(pagamento.TipoPagamento == EPagamento.Cartao)
        {
            return new ResponseResult<string>
            {
                Status = 400
            };
        }

        return new ResponseResult<string>()
        {
            Data = pagamento.LinhaDigitavel, 
            Status = 200
        };
    }
   
}
