using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Evento.API.Data;

public class EventoContext : DbContext
{
    public EventoContext(DbContextOptions<EventoContext> options): base(options)  
    {
        
    }

    public DbSet<Domain.Evento> Eventos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EventoContext).Assembly);
    }

}
