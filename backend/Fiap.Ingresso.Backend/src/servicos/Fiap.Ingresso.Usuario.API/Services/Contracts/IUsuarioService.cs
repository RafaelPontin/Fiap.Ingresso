using Fiap.Ingresso.Usuario.API.DTOs;
using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Usuario.API.Services.Contracts
{
    public interface IUsuarioService
    {
        Task<ResponseResult<UsuarioResponse>> CadastrarUsuario(CadastrarUsuarioDto usuario);        
        Task<ResponseResult<UsuarioResponse>> AlterarUsuario(AlterarCadastroDto usuario);        
        Task<ResponseResult<string>> Login(string email, string senha);        
    }
}
