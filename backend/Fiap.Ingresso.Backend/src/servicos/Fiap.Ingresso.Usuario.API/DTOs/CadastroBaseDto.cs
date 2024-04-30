using System.ComponentModel.DataAnnotations;

namespace Fiap.Ingresso.Usuario.API.DTOs
{
    public class CadastroBaseDto
    {
        [Required(ErrorMessage = "Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Cpf é obrigatório.")]
        public string Cpf { get; set; }
    }

    public class Usuario : CadastroBaseDto
    {
        [Required(ErrorMessage = "Login é obrigatório.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Confirmação de senha é obrigatório.")]
        public string ConfirmacaoSenha { get; set; }

        [Required(ErrorMessage = "Senha é obrigatório.")]
        public string Senha { get; set; }

    }

    public class AlterarCadastroDto : CadastroBaseDto
    {
        
    }
}
