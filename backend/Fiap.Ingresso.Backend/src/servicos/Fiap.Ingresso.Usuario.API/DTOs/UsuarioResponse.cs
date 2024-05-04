namespace Fiap.Ingresso.Usuario.API.DTOs
{
    public class UsuarioResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }        
        public string Cpf { get; set; }
        public string Email { get; set; }        
    }
}
