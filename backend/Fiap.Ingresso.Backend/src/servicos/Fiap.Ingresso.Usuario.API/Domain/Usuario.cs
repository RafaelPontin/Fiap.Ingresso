namespace Fiap.Ingresso.Usuario.API.Domain;

public class Usuario
{
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string Email { get; private set; }

}
