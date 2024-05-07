using Fiap.Ingresso.Usuario.API.DTOs;
using Fiap.Ingresso.Usuario.API.Services.Contracts;
using Fiap.Ingresso.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Ingresso.Usuario.API.Controllers
{
    public class UsuarioController : BaseController
    {

        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações inválidas");
            }

            var response = await _usuarioService.Login(request.Login, request.Senha);

            return Ok(response);
        }

        [HttpPost("Criar-Usuario")]
        [AllowAnonymous]
        public async Task<IActionResult> CadastrarAsync(DTOs.CadastrarUsuarioDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações inválidas");
            }

            var response = await _usuarioService.CadastrarUsuario(request);

            return Ok(response);
        }

        [HttpPost("Alterar-Usuario")]
        [Authorize]
        public async Task<IActionResult> AlterarAsync(AlterarCadastroDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações inválidas");
            }

            var response = await _usuarioService.AlterarUsuario(request);

            return Ok(response);
        }
    }
}
