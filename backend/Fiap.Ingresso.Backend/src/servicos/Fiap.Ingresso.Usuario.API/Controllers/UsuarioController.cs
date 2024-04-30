using Fiap.Ingresso.Usuario.API.DTOs;
using Fiap.Ingresso.WebAPI.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Ingresso.Usuario.API.Controllers
{
    public class UsuarioController : BaseController
    {
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
        public IActionResult Cadastrar(CadastrarDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Informações inválidas");
            }

            return Ok();
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
