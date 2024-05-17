namespace Fiap.Ingresso.Ingresso.API.Domain;

public class IngressosDoEvento
{
    public Guid Id { get; private set; }
    public Guid EventoId { get; private set; }
    public int Total { get; private set; }
    public int Disponiveis { get; private set; }
    public decimal Preco { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime DataFim { get; private set; }
    public bool Ativo { get; private set; }

    public List<Ingresso> IngressosVendidos = new List<Ingresso>();

    public List<string> Erros = new List<string>();

    public IngressosDoEvento()
    {
        
    }
    public IngressosDoEvento(Guid eventoid,int total, int disponiveis,decimal preco,DateTime dataFim)
    {
        DefinirIngressosDoEvento(eventoid, total, disponiveis, preco, dataFim);
    }

    private void DefinirIngressosDoEvento(Guid eventoid, int total, int disponiveis, decimal preco, DateTime dataFim)
    {
        ValidarIngressosDoEvento(eventoid, total, disponiveis, preco, dataFim);
        Id = Guid.NewGuid();
        EventoId = eventoid;
        Total = total;
        Disponiveis = disponiveis;
        Preco = preco;
        DataInicio = DateTime.Now;
        DataFim = dataFim;
        Ativo = true;
    }

    public IEnumerable<Ingresso> ComprarIngressosDoEvento(Guid usuarioId, int quantidade)
    {
        ValidarCompra(quantidade);
        for (int i = 0; i < quantidade; i++)
        {
            Disponiveis--;
            var venda = new Ingresso(usuarioId, this.EventoId);
            IngressosVendidos.Add(venda);
            if (Disponiveis == 0)
            {
                Ativo = false;
            }
        }

        return IngressosVendidos;
    }

    private void ValidarCompra(int quantidade)
    {
        if (quantidade <= 0)
        {
            Erros.Add("Quantidade de ingressos deve ser maior que 0");
        }
        if (DataFim < DateTime.Now)
        {   
            Ativo = false;
            Erros.Add("Ingresso expirado");
        }
        if (Disponiveis < quantidade)
        {   
            Ativo = false;
            Erros.Add("Ingressos Esgotados ou Indisponível");
        }
        if (!Ativo)
        {
            Erros.Add("Compra de ingresso não está mais disponível");
        }
    }

    private void ValidarIngressosDoEvento(Guid eventoid, int totalIngressos, int disponiveis, decimal preco, DateTime dataFim)
    {
        if (eventoid == Guid.Empty) Erros.Add("EventoId vazio ou nulo");
        if (totalIngressos <= 0) Erros.Add("Total de ingressos deve ser maior que 0");
        if (disponiveis <= 0) Erros.Add("Total de ingressos disponiveis deve ser maior que 0");
        if (preco <= 0) Erros.Add("Preço unitário deve ser maior que 0");
        if (dataFim < DateTime.Now) Erros.Add("Data fim deve ser maior que a data atual");
        if (disponiveis > totalIngressos) Erros.Add("Total de ingressos disponiveis não pode ser maior que o total de ingressos");
    }
}
