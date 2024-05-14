using Azure;
using Fiap.Ingresso.Usuario.API.DTOs;
using Fiap.Ingresso.Usuario.API.Infra.Repository;
using Fiap.Ingresso.Usuario.API.Services.Contracts;
using Fiap.Ingresso.WebAPI.Core.Communication;
using Fiap.Ingresso.WebAPI.Core.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fiap.Ingresso.Usuario.API.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _repository;
        private readonly AppSettings _appSettings;
        private readonly IHttpContextAccessor _accessor;
        public UsuarioService(IUsuarioRepository repository, IOptions<AppSettings> appSettings, IHttpContextAccessor accessor)
        {
            _repository = repository;
            _appSettings = appSettings.Value;
            _accessor = accessor;
        }

        public async Task<ResponseResult<UsuarioResponse>> CadastrarUsuario(CadastrarUsuarioDto usuario)
        {
            var response = new ResponseResult<UsuarioResponse>();

            var novoUsuario = await NovoUsuario(usuario);

            if (novoUsuario.Erros.Any())
            {
                response.Status = novoUsuario.Status;
                response.Erros = novoUsuario.Erros;
                return response;
            }

            await _repository.Cadastrar(novoUsuario.Data);

            ObterUsuarioResponse(response, novoUsuario.Data);

            return response;
        }

        private void ObterUsuarioResponse(ResponseResult<UsuarioResponse> response, Domain.Usuario usuario)
        {
            response.Data = new UsuarioResponse
            {
                Cpf = usuario.Cpf,
                Email = usuario.Email,
                Id = usuario.Id,
                Nome = usuario.Nome
            };
        }

        public async Task<ResponseResult<UsuarioLogadoResponse>> Login(string email, string senha)
        {
            var response = new ResponseResult<UsuarioLogadoResponse>();
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                response.Status = 400;
                response.Erros.Add("Informe usuario e senha");
                return response;
            }

            var usuario = await _repository.Login(email, senha);
            if (usuario == null)
            {
                response.Status = 400;
                response.Erros.Add("Usuario/senha inválidos");
                return response;
            }

            var token = CodificarToken(usuario);
            response.Data = new UsuarioLogadoResponse
            {
                AccessToken = token,                
                Email = usuario.Email,
                Nome = usuario.Nome,
                Id = usuario.Id,
                IsAdmin = usuario.IsAdmin
            };

            return response;
        }

        private string CodificarToken(Domain.Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Emissor,
                Audience = _appSettings.ValidoEm,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.Nome.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, usuario.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, usuario.Email.ToString()),                    
                    new Claim("IsAdmin", usuario.IsAdmin.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        public async Task<ResponseResult<Domain.Usuario>> NovoUsuario(CadastrarUsuarioDto usuarioDto)
        {
            var response = new ResponseResult<Domain.Usuario>();

            if (await VerificarEmailCadastrado(usuarioDto.Email))
            {
                response.Erros.Add("Email ja cadastrado");
                response.Status = 400;
                return response;
            }

            var usuario = new Domain.Usuario();
            usuario.CadastraUsuario(usuarioDto.Nome, usuarioDto.Email, usuarioDto.Cpf, usuarioDto.Senha);
            if (usuario.EhValido())
            {
                response.Data = usuario;
                response.Status = 201;
                return response;
            }

            response.Status = 400;
            response.Erros = usuario.Erros;

            return response;
        }


        public async Task<bool> VerificarEmailCadastrado(string email)
        {
            var usuario = await _repository.ObterPorEmail(email);
            return usuario != null;
        }

        public async Task<ResponseResult<UsuarioResponse>> AlterarUsuario(AlterarCadastroDto usuarioDto)
        {

            var response = new ResponseResult<UsuarioResponse>();

            var usuarioEmail = ObterUserEmail();

            if (!await VerificarEmailCadastrado(usuarioEmail))
            {
                response.Erros.Add("Não foi possível alterar os dados. Usuário não encontrado");
                response.Status = 400;
                return response;
            }

            var usuario = await _repository.ObterPorEmail(usuarioEmail);
            usuario.AdicionaNome(usuarioDto.Nome);
            usuario.AdicionaCpf(usuarioDto.Cpf);

            await _repository.AlterarCadastro(usuario);

            ObterUsuarioResponse(response, usuario);

            return response;
        }

        private string ObterUserEmail()
        {
            var claims = _accessor.HttpContext.User.Identity as ClaimsIdentity;

            var email = _accessor.HttpContext.User.FindFirst("Email");
            return email.Value.ToString();
        }
    }
}
