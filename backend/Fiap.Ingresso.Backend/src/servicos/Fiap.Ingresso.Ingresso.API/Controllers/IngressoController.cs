using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
using Fiap.Ingresso.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Ingresso.Ingresso.API.Controllers;

[Authorize]
public class IngressoController : BaseController
{
    private readonly IIngressoService _services;

    public IngressoController(IIngressoService service)
    {
        _services = service;
    }

    [HttpPost("Comprar/{ingressoId}")]
    public async Task<IActionResult> ComprarIngresso([FromRoute]Guid ingressoId,[FromBody] IngressoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.ComprarIngresso(ingressoId, dto.UsuarioId, dto.Quantidade, dto.PagamentoId);
        return Ok(response);
    }

    [HttpGet("Obter-Histórico-Por-Usuario")]
    public async Task<IActionResult> ObterHistoricoDeIngressosPorUsuario()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var usuarioId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "sub").Value);
        var response = await _services.ObterHistoricoDeIngressosPorUsuario(usuarioId);
        return Ok(response);
    }
}
