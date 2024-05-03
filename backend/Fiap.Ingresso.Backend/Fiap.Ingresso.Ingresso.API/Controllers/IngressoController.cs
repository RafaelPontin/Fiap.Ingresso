using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
using Fiap.Ingresso.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Ingresso.Ingresso.API.Controllers;

public class IngressoController : BaseController
{
    private readonly IIngressoService _services;

    public IngressoController(IIngressoService service)
    {
        _services = service;
    }

    [HttpPost("Criar-Ingressos")]
    public async Task<IActionResult> CadastraIngresso(CadastrarIngressoDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.CadastraIngresso(dto);
        return Ok(response);
    }

    [HttpGet("Obter-Ingressos-Disponiveis")]
    public async Task<IActionResult> ObterIngressosDisponiveis()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.BuscarIngressosDisponiveis();
        return Ok(response);
    }

    [HttpGet("Obter-Ingressos-Por-Evento")]
    public async Task<IActionResult> ObterIngressosPorEvento([FromRoute] Guid eventoId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.BuscarIngressosPorEvento(eventoId);
        return Ok(response);
    }

}
