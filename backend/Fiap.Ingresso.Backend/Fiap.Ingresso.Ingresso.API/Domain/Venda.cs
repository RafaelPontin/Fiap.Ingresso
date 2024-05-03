namespace Fiap.Ingresso.Ingresso.API.Domain;

public class Venda
{
    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }
    public Guid IngressoId { get; private set; }
    public DateTime DataVenda { get; private set; }

    public List<string> Erros = new List<string>();

    public Venda()
    {
        
    }
    public Venda(Guid usuarioId, Guid ingressoId)
    {
        DefinirVenda(usuarioId, ingressoId);
    }

    private void DefinirVenda(Guid usuarioId, Guid ingressoId)
    {
        ValidarVenda(usuarioId, ingressoId);
        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        IngressoId = ingressoId;
        DataVenda = DateTime.Now;
    }

    private void ValidarVenda(Guid usuarioId, Guid ingressoId)
    {
        if (usuarioId == Guid.Empty) Erros.Add("UsuarioId vazio ou nulo");
        if (ingressoId == Guid.Empty) Erros.Add("IngressoId vazio ou nulo");
    }
}
