using Fiap.Ingresso.Evento.API.DTOs;
using Fiap.Ingresso.Evento.API.Infra;
using Fiap.Ingresso.Evento.API.Services.Contracts;
using Fiap.Ingresso.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Ingresso.Evento.API.Controllers;

[Authorize]
public class EventoController : BaseController
{
    private readonly IEventoService _services;


    public EventoController(IEventoService service)
    {
        _services = service;
    }

    [HttpPost("Criar-Evento")]
    [AllowAnonymous]
    public async Task<IActionResult> CadastraEvento(CadastraEventoDto dto)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.CadastraEvento(dto);
        return Ok(response);
    }


    [HttpPut("Alterar-Evento")]
    public async Task<IActionResult> AlterarEvento([FromBody] AlterarEventoDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.AlterarEvento(dto);
        return Ok(response);
    }

    [HttpPut("Finalizar-Evento/{id:guid}")]
    public async Task<IActionResult> FinalizarEvento(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }

        var response = await _services.CancelaEvento(id);

        return Ok(response);
    }

    [HttpGet("Evento/{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Informações inválidas");
        }
        var eventos = await _services.GetById(id);
        return Ok(eventos);
    }

    [HttpGet("Listar")]
    public async Task<IActionResult> ListarEventos()
    {
        var eventos = await _services.ListarEvento();
        return Ok(eventos);
    }

}
