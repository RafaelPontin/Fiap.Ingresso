using domain = Fiap.Ingresso.Usuario.API.Domain;

namespace Fiap.Ingresso.Usuario.API.Data.Seed;

public static class UsuarioSeed
{
    public static void Seed(UsuarioContext context)
    {
        if (!context.Usuarios.Any())
        {
            context.AddRange(AddUsuario());
            context.SaveChanges();
        }
    }

    private static List<domain.Usuario> AddUsuario()
    {
        List<domain.Usuario> usuarios = new List<domain.Usuario>();

        domain.Usuario usuarioNormal = new domain.Usuario();
        usuarioNormal.CadastraUsuario("usuario", "usuario@email.com", "1234567891", "123123");
        if (!usuarioNormal.Erros.Any()) usuarios.Add(usuarioNormal);


        domain.Usuario usuarioAdmin = new domain.Usuario();
        usuarioAdmin.CadastraUsuario("adm", "adm@adm.com", "45678912345", "123123");
        if (!usuarioAdmin.Erros.Any())
        {
            usuarioAdmin.IsAdmin = true;
            usuarios.Add(usuarioAdmin);
        }

        return usuarios;
    }
}
