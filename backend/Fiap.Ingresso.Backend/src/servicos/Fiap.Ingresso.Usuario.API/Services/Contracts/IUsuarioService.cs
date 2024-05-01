using Fiap.Ingresso.Usuario.API.DTOs;
using Fiap.Ingresso.WebAPI.Core.Communication;

namespace Fiap.Ingresso.Usuario.API.Services.Contracts
{
    public interface IUsuarioService
    {
        Task<ResponseResult<Domain.Usuario>> CadastrarUsuario(CadastrarUsuario usuario);        
    }
}
