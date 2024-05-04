using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
using Fiap.Ingresso.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Ingresso.Ingresso.API.Controllers;

public class IngressosDoEventoController : BaseController
{
    private readonly IIngressosDoEventoService _services;

    public IngressosDoEventoController(IIngressosDoEventoService service)
    {
        _services = service;
    }

    [HttpPost("Cadastrar")]
    public async Task<IActionResult> CadastraIngressosDoEvento(CadastrarIngressosDoEventoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.CadastraIngressosDoEvento(dto);
        return Ok(response);
    }

    [HttpGet("Obter-Disponiveis")]
    public async Task<IActionResult> ObterIngressosDoEventoDisponiveis()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.ObterIngressosDoEventoDisponiveis();
        return Ok(response);
    }

    [HttpGet("Obter-Por-Evento/{eventoId}")]
    public async Task<IActionResult> ObterIngressosDoEventoPorEvento([FromRoute] Guid eventoId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.ObterIngressosDoEventoPorEvento(eventoId);
        return Ok(response);
    }

    [HttpGet("Obter-Por-Id/{ingressoId}")]
    public async Task<IActionResult> ObterIngressosDoEventoPorId([FromRoute] Guid ingressoId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.ObterIngressosDoEventoPorId(ingressoId);
        return Ok(response);
    }

}
