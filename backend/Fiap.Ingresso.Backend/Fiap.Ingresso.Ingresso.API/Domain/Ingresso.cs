﻿namespace Fiap.Ingresso.Ingresso.API.Domain;

public class Ingresso
{
    public Guid Id { get; private set; }
    public Guid EventoId { get; private set; }
    public int Total { get; private set; }
    public int Disponiveis { get; private set; }
    public decimal Preco { get; private set; }
    public DateTime DataInicio { get; private set; }
    public DateTime DataFim { get; private set; }
    public bool Ativo { get; private set; }

    public List<Venda> Vendas = new List<Venda>();

    public List<string> Erros = new List<string>();

    public Ingresso()
    {
        
    }
    public Ingresso(Guid eventoid,int total, int disponiveis,decimal preco,DateTime dataFim)
    {
        DefinirIngresso(eventoid, total, disponiveis, preco, dataFim);
    }

    private void DefinirIngresso(Guid eventoid, int total, int disponiveis, decimal preco, DateTime dataFim)
    {
        ValidarIngresso(eventoid, total, disponiveis, preco, dataFim);
        Id = Guid.NewGuid();
        EventoId = eventoid;
        Total = total;
        Disponiveis = disponiveis;
        Preco = preco;
        DataInicio = DateTime.Now;
        DataFim = dataFim;
        Ativo = true;
    }

    public Venda Vender(Guid usuarioId)
    {
        ValidarVenda();

        Disponiveis--;

        var venda = new Venda(usuarioId,this.Id);
        Vendas.Add(venda);

        return venda;
    }

    private void ValidarVenda()
    {
        if (DataFim < DateTime.Now)
        {   
            Ativo = false;
            Erros.Add("Ingresso expirado");
        }
        if (Disponiveis <= 0)
        {   
            Ativo = false;
            Erros.Add("Ingressos Esgotados ou Indisponível");
        }
    }

    private void ValidarIngresso(Guid eventoid, int totalIngressos, int disponiveis, decimal preco, DateTime dataFim)
    {
        if (eventoid == Guid.Empty) Erros.Add("EventoId vazio ou nulo");
        if (totalIngressos <= 0) Erros.Add("Total de ingressos deve ser maior que 0");
        if (disponiveis <= 0) Erros.Add("Total de ingressos disponiveis deve ser maior que 0");
        if (preco <= 0) Erros.Add("Preço unitário deve ser maior que 0");
        if (dataFim < DateTime.Now) Erros.Add("Data fim deve ser maior que a data atual");
        if (disponiveis > totalIngressos) Erros.Add("Total de ingressos disponiveis não pode ser maior que o total de ingressos");
    }
}
