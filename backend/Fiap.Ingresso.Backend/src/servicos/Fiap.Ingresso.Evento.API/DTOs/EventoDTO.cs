namespace Fiap.Ingresso.Evento.API.DTOs;
public class EventoDTO
{
    public Guid? Id { get; set; }
    public string Nome { get;  set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public DateTime? DataEvento { get; set; }
    public int PublicoMaximo { get; set; }
    public int Ativo { get; set; }
    public string Logradouro { get; set; }
    public string Numero { get; set; }
    public string Cidade { get; set;}
    public string Bairro { get; set;}
    public string Cep { get; set; }
    public string? Descricao { get; set;}
    public string? SiteEvento { get; set; }
    public decimal Valor { get; set; }

    public void ConvertToEventoDto(Domain.Evento evento)
    {
        Id = evento.Id;
        Nome = evento.Nome; 
        DataInicio = evento.DataInicio;
        DataFim = evento.DataFim;
        DataEvento = evento.DataEvento;
        PublicoMaximo = evento.PublicoMaximo;
        Ativo = evento.Ativo;
        Logradouro = evento.Logradouro;
        Numero = evento.Numero;
        Cidade = evento.Cidade;
        Bairro = evento.Bairro;
        Cep = evento.Cep;
        Descricao = evento.Descricao;
        SiteEvento = evento.SiteEvento;
        Valor = evento.Valor;
    }
}
