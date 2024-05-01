namespace Fiap.Ingresso.Usuario.API.Infra.Repository
{
    public interface IUsuarioRepository
    {
        Task Cadastrar(Domain.Usuario usuario);
        Task AlterarCadastro(Domain.Usuario usuario);
        Task<Domain.Usuario> Login(string email, string senha);
        Task<Domain.Usuario> ObterPorEmail(string email);
    }
}
