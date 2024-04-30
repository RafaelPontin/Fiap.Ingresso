using Fiap.Ingresso.Usuario.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Usuario.API.Data
{
    public class UsuarioContext : DbContext
    {

        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        {
        }

        public DbSet<Domain.Usuario> Usuarios{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioContext).Assembly);
        }

    }
}
