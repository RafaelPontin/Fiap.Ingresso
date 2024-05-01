using Fiap.Ingresso.Usuario.API.DTOs;
using Fiap.Ingresso.Usuario.API.Infra.Repository;
using Fiap.Ingresso.Usuario.API.Services.Contracts;
using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Usuario.API.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseResult<Domain.Usuario>> CadastrarUsuario(CadastrarUsuario usuario)
        {
            var response = new ResponseResult<Domain.Usuario>();

            response = await NovoUsuario(usuario);

            if (response.Erros.Any()) return response;

            await _repository.Cadastrar(response.Data);

            // Criar Metodo de Map

            return response;
        }


        

        public async Task<ResponseResult<Domain.Usuario>> NovoUsuario(CadastrarUsuario usuarioDto)
        {
            var response = new ResponseResult<Domain.Usuario>();

            if (await VerificarEmailCadastrado(usuarioDto.Email))
            {
                response.Erros.Add("Email ja cadastrado");
                response.Status = 400;
                return response;
            }

            var usuario = new Domain.Usuario();            
            usuario.CadastraUsuario(usuario.Nome, usuario.Email, usuario.Cpf, usuario.Senha);
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
    }
}
