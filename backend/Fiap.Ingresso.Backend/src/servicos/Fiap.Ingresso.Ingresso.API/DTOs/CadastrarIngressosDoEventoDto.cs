namespace Fiap.Ingresso.Ingresso.API.DTOs;

public record CadastrarIngressosDoEventoDto
{
    public Guid EventoId { get;  set; }
    public int Total { get;  set; }
    public int Disponiveis { get;  set; }
    public decimal Preco { get;  set; }
    public DateTime DataFim { get;  set; }
}
