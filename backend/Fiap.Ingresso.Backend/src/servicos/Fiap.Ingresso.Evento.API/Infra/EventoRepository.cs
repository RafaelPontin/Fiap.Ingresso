using Fiap.Ingresso.Evento.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Evento.API.Infra;

public class EventoRepository : IEventoRepository
{
    protected readonly EventoContext _dbContext;
    protected readonly DbSet<Domain.Evento> _dbSet;

    public EventoRepository(EventoContext context)
    {
        _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<Domain.Evento>();
    }

    public async Task CadastraEvento(Domain.Evento evento)
    {
       _dbContext.Eventos.Add(evento);
       await _dbContext.SaveChangesAsync();
    }

    public async Task AlterarEvento(Domain.Evento evento)
    {
        _dbContext.Entry(evento).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Domain.Evento>> GetEventos()
    {
        return _dbContext.Eventos.ToList();
    }

    public async Task<Domain.Evento> GetEventoById(Guid id)
    {
        return await _dbContext.Eventos.FirstOrDefaultAsync(e => e.Id == id);
    }

}
