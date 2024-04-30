using Bogus;
using Dominio = Fiap.Ingresso.Usuario.API.Domain;

namespace Fiap.Ingresso.Usuario.Teste.Unitario
{
    public class UsuarioAdicionarTest
    {

        private Dominio.Usuario _usuario;
        public UsuarioAdicionarTest()
        {
            _usuario = new Dominio.Usuario();
        }

        [Fact(DisplayName = "Cadastro de usuario correto")]
        public void Adiciona_usuario_corretamente()
        {
            _usuario.CadastraUsuario("Rafael", "teste@gmail.com", "123456789", "123123");
            Assert.Equal(_usuario.Erros.Count(), 0);
        }

        [Fact(DisplayName = "Cadastro de usuario com o email vazio")]
        public void Adicionar_usuario_email_vazio()
        {
            _usuario.CadastraUsuario("Rafael", string.Empty, "123456789", "123123");

            Assert.True(_usuario.Erros.Any());
            Assert.Null(_usuario.Email);
        }

        [Fact(DisplayName = "Cadastro de usuario com o email invalido")]
        public void Adicionar_usuario_email_invalido()
        {
            _usuario.CadastraUsuario("Rafael", "teste email", "123456789", "123123");

            Assert.True(_usuario.Erros.Any());
            Assert.Null(_usuario.Email);
        }

        [Fact(DisplayName = "Cadastro de usuario com o nome vazio")]
        public void Adicionar_usuario_nome_vazio()
        {
            _usuario.CadastraUsuario("", "teste@email.com", "123456789", "123123");

            Assert.True(_usuario.Erros.Any());
            Assert.Null(_usuario.Nome);
        }

        [Fact(DisplayName = "Cadastro de usuario com o cpf vazio")]
        public void Adicionar_usuario_com_cpf_vazio()
        {
            _usuario.CadastraUsuario("Usuario", "teste@email.com", string.Empty, "123123");
            Assert.True(_usuario.Erros.Any());
            Assert.Null(_usuario.Cpf);
        }

        [Fact(DisplayName = "Cadastro de usuario com senha invalida")]
        public void Adicionar_usuario_com_senha_invalida()
        {
            _usuario.CadastraUsuario("Usuario", "teste@email.com", "12345678901", string.Empty);
            Assert.True(_usuario.Erros.Any());
            Assert.Null(_usuario.Senha);
        }

        [Fact(DisplayName = "Valida Id Usuario")]
        public void Cria_id_usuario()
        {
            _usuario.CriarId();
            Assert.True(_usuario.Id is not null);
        }


        [Fact(DisplayName = "Cria Senha valida Usuario")]
        public void Cria_Senha_Usuario()
        {
            _usuario.AdicionaSenha("123123");
            Assert.NotNull(_usuario.Senha);
        }

        [Fact(DisplayName = "Cria Senha invalida Usuario")]
        public void Cria_Invalida_Senha_Usuario()
        {
            _usuario.AdicionaSenha("");
            Assert.Null(_usuario.Senha);
        }



        [Fact(DisplayName = "Cria nome valida Usuario")]
        public void Cria_Nome_Usuario()
        {
            _usuario.AdicionaNome("Usuario");
            Assert.NotNull(_usuario.Nome);
        }

        [Fact(DisplayName = "Cria Nome invalida Usuario")]
        public void Cria_Invalida_Nome_Usuario()
        {
            _usuario.AdicionaNome("");
            Assert.Null(_usuario.Nome);
        }


        [Fact(DisplayName = "Cria Email valida Usuario")]
        public void Cria_Email_Usuario()
        {
            _usuario.AdicionaEmail("teste@email.com");
            Assert.NotNull(_usuario.Email);
        }

        [Fact(DisplayName = "Cria Email invalida Usuario")]
        public void Cria_Invalida_Email_Usuario()
        {
            _usuario.AdicionaEmail("email.com");
            Assert.Null(_usuario.Email);
        }



        [Fact(DisplayName = "Cria Cpf valida Usuario")]
        public void Cria_Cpf_Usuario()
        {
            _usuario.AdicionaCpf("1231231231");
            Assert.NotNull(_usuario.Cpf);
        }

        [Fact(DisplayName = "Cria Cpf invalida Usuario")]
        public void Cria_Invalida_Cpf_Usuario()
        {
            _usuario.AdicionaCpf(string.Empty);
            Assert.Null(_usuario.Cpf);
        }

        [Fact(DisplayName = "Valida contrutor do usuario")]
        public void Valida_Construtor()
        {
            Assert.NotNull(_usuario.Erros);
        }


    }
}
