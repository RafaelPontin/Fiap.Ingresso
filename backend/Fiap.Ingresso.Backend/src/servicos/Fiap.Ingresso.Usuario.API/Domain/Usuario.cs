namespace Fiap.Ingresso.Usuario.API.Domain;

public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Cpf { get; private set; }
    public string Senha { get; private set; }   
    public bool IsAdmin { get; set; }   
    
    public List<string> Erros { get; }


    public Usuario()
    {
        Erros = new List<string>();
        IsAdmin = false;
    }

    public void CadastraUsuario(string nome, string email, string cpf, string senha)
    {
        AdicionaNome(nome);
        AdicionaEmail(email);
        AdicionaCpf(cpf);
        AdicionaSenha(senha);
        CriarId();
    }

    public void CriarId()
    {
        if(!Erros.Any()) Id = Guid.NewGuid();
    }

    public void AdicionaSenha(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha)) Erros.Add("Senha vazio ou nulo");
        else Senha = senha;
    }

    public void AdicionaNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome)) Erros.Add("Nome vazio ou nulo");
        else Nome = nome;
    }

    public void AdicionaEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) Erros.Add("Email vazio ou nulo");
        else if (!email.Contains("@")) Erros.Add("Email Invalido");
        else Email = email;
    }

    public void AdicionaCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) Erros.Add("Cpf vazio ou nulo");
        else Cpf = cpf;
    }

    public bool EhValido()
    {
        return !Erros.Any();
    }    
}
