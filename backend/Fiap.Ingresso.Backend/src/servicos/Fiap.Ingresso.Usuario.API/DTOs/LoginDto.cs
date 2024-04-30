using System.ComponentModel.DataAnnotations;

namespace Fiap.Ingresso.Usuario.API.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Login é obrigatório.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Senha é obrigatório.")]
        public string Senha { get; set; }
    }
}
