using Fiap.Ingresso.Usuario.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Usuario.API.Infra.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        protected readonly UsuarioContext _dbContext;
        protected readonly DbSet<Domain.Usuario> _dbSet;

        public UsuarioRepository(UsuarioContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<Domain.Usuario>();
        }

        public async Task AlterarCadastro(Domain.Usuario usuario)
        {
            _dbContext.Entry(usuario).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Cadastrar(Domain.Usuario usuario)
        {
            _dbSet.Add(usuario);
            await _dbContext.SaveChangesAsync();
        }

        public Task Login(string email, string senha)
        {
            throw new NotImplementedException();
        }
    }
}
