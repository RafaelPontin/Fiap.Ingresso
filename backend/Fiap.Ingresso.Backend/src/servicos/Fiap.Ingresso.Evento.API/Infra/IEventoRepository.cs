namespace Fiap.Ingresso.Evento.API.Infra;

public interface IEventoRepository
{
    Task CadastraEvento(Domain.Evento evento);
    Task AlterarEvento(Domain.Evento evento);
    Task<IEnumerable<Domain.Evento>> GetEventos();

    Task<Domain.Evento> GetEventoById(Guid id);
}
