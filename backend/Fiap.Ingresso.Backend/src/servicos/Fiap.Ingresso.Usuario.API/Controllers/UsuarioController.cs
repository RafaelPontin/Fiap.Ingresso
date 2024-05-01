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
        public IActionResult Login(LoginDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações inválidas");
            }            

            return Ok();
        }

        [HttpPost("Criar-Usuario")]
        [AllowAnonymous]
        public IActionResult Cadastrar(DTOs.CadastrarUsuario request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações inválidas");
            }

            var response = _usuarioService.CadastrarUsuario(request);

            return Ok(response);
        }

        [HttpPost("Alterar-Usuario")]        
        public IActionResult Cadastrar(AlterarCadastroDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações inválidas");
            }

            return Ok();
        }
    }
}
