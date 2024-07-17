using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
using Fiap.Ingresso.WebAPI.Core.Communication;
using Newtonsoft.Json;
using System.Text;

namespace Fiap.Ingresso.Ingresso.API.Services;

public class ValidarPagamentoService : IValidarPagamentoService
{
    private readonly HttpClient _httpClient;

    public ValidarPagamentoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<bool> ValidarPagamento(Guid pagamento)
    {
        var baseUrl = "https://localhost:8068/";
        var response = await _httpClient.GetAsync(baseUrl+"Pagamento-por-Id?id="+pagamento);
        string result = await response.Content.ReadAsStringAsync();
        var responseResult = JsonConvert.DeserializeObject<ResponseResult<int>>(result);
        return responseResult.Status == 200;
    }
}
