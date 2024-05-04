namespace Fiap.Ingresso.Ingresso.API.Domain;

public class Ingresso
{
    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }
    public Guid EventoId { get; set; }
    public DateTime DataVenda { get; private set; }

    public List<string> Erros = new List<string>();

    public Ingresso()
    {
        
    }
    public Ingresso(Guid usuarioId, Guid eventoId)
    {
        DefinirIngresso(usuarioId, eventoId);
    }

    private void DefinirIngresso(Guid usuarioId, Guid eventoId)
    {
        ValidarIngresso(usuarioId, eventoId);
        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        DataVenda = DateTime.Now;
        EventoId = eventoId;
    }

    private void ValidarIngresso(Guid usuarioId, Guid eventoId)
    {
        if (usuarioId == Guid.Empty) Erros.Add("UsuarioId é obrigatório");
        if (eventoId == Guid.Empty) Erros.Add("EventoId é obrigatório");
    }
}
