using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
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
        var content = new StringContent(JsonConvert.SerializeObject(pagamento), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("api/pagamento/validar", content);

        return response.IsSuccessStatusCode;
    }
}
