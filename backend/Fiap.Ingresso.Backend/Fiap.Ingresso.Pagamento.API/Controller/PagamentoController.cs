using Fiap.Ingresso.Pagamento.API.DTOs;
using Fiap.Ingresso.Pagamento.API.Services.Contracts;
using Fiap.Ingresso.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Ingresso.Pagamento.API.Controller
{
    public class PagamentoController : BaseController
    {
        private readonly IPagamentoService _service;

        public PagamentoController(IPagamentoService service)
        {
            _service = service;
        }

        [HttpPost("Cadastra-Pagamento")]
        [AllowAnonymous]
        public async Task<IActionResult> CadastraPagamento(CadastrarPagamento pagamento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações inválidas");
            }

            var retorno = await _service.CadastraEvento(pagamento);
            return Ok(retorno);
        }

        [HttpGet("Pagamento-por-Id")]
        [AllowAnonymous]
        public async Task<IActionResult> PagamentoPorId(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações inválidas");
            }

            var retorno = await _service.GetPagamentoById(id);
            return Ok(retorno);
        }

        [HttpGet("GetLinhaDigitavel")]
        public async Task<IActionResult> GetLinhaDigitavel(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações inválidas");
            }

            var retorno = await _service.GetLinhaDigitavel(id);
            return Ok(retorno);
        }


    }
}
