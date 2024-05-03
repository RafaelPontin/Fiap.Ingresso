using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
using Fiap.Ingresso.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Ingresso.Ingresso.API.Controllers;

public class VendaController : BaseController
{
    private readonly IVendaService _services;

    public VendaController(IVendaService service)
    {
        _services = service;
    }

    [HttpPost("Comprar-Ingresso")]
    public async Task<IActionResult> VenderIngresso([FromRoute]Guid ingressoId,[FromBody] VenderIngressoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.VenderIngresso(ingressoId, dto.UsuarioId);
        return Ok(response);
    }

    [HttpGet("Obter-Histórico-Por-Usuario")]
    public async Task<IActionResult> ObterHistorico([FromRoute] Guid usuarioId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.ObterHistoricoUsuario(usuarioId);
        return Ok(response);
    }
}
