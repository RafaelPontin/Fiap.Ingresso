namespace Fiap.Ingresso.Evento.API.Domain;
public class Evento
{
    public Guid? Id { get; private set; }
    public string Nome { get; private set; }    
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataFim { get; private set; }
    public DateTime? DataEvento { get; private set; }
    public int PublicoMaximo { get; private set; }
    public int Ativo { get; private set; }
    public string Logradouro { get; private set; }
    public string Numero { get; private set; }
    public string Cidade { get; private set; }
    public string Bairro { get; private set; }
    public string Cep { get; private set; } 
    public string? Descricao { get; private set; }
    public string? SiteEvento { get; private set; }
    public decimal Valor { get; private set; }  

    public List<string> Erros { get; }

    public Evento()
    {
        Erros = new List<string>();
    }

    public void AdicionarEvento(string nome, DateTime inicio, DateTime fim, DateTime dataEvento, int publicoMaximo, string logradouro, string numero, string cidade, string bairro, string cep, decimal valor)
    {
        SetNomeEvento(nome);
        SetDataInicio(inicio);
        SetDataFim(fim);
        SetDataEvento(dataEvento);
        SetPublicoMaximo(publicoMaximo);
        SetEndereco(logradouro, numero, cidade, bairro, cep);
        SetAtivo(true);
        SetValor(valor);
        CriarId();
    }

    public void AlterarEvento(Guid id, string nome, DateTime inicio, DateTime fim, DateTime dataEvento, int publicoMaximo, string logradouro, string numero, string cidade, string bairro, string cep, decimal valor, string descricao, string url)
    {
        SetNomeEvento(nome);
        SetDataInicio(inicio);
        SetDataFim(fim);
        SetDataEvento(dataEvento);
        SetPublicoMaximo(publicoMaximo);
        SetEndereco(logradouro, numero, cidade, bairro, cep);
        SetAtivo(true);
        SetValor(valor);
        SetUrlEvento(url);
        SetDescricao(descricao);
        if (!Erros.Any())
        {
            Id = id;
        }
    }

    public void CancelarEvento()
    {
        SetAtivo(false);
    }

    public void SetEndereco(string logradouro, string numero, string cidade, string bairro, string cep)
    {
        if (string.IsNullOrWhiteSpace(logradouro)) Erros.Add("logradouro do evento vazio ou nulo");
        else if (string.IsNullOrWhiteSpace(numero)) Erros.Add("numero do evento vazio ou nulo");
        else if (string.IsNullOrWhiteSpace(cidade)) Erros.Add("cidade do evento vazio ou nulo");
        else if (string.IsNullOrWhiteSpace(bairro)) Erros.Add("bairro do evento vazio ou nulo");
        else if (string.IsNullOrWhiteSpace(cep)) Erros.Add("bairro do evento vazio ou nulo");
        if(!Erros.Any())
        {
            Logradouro = logradouro;
            Numero= numero;
            Cidade = cidade;
            Bairro = bairro;
            Cep = cep;
        }
        else
        {
            Logradouro = string.Empty;
            Numero = string.Empty;
            Cidade = string.Empty;
            Bairro = string.Empty;
            Cep = string.Empty;
        }
    }

    public void SetDescricao(string descricao)
    {
        if (string.IsNullOrWhiteSpace(descricao)) return;

        if (descricao.Length >= 1000) Erros.Add($"Tamanho invalido da descrição");
        else if (!string.IsNullOrEmpty(descricao)) Descricao = descricao;
    }

    public void SetUrlEvento(string urlEvento)
    {

        if (string.IsNullOrWhiteSpace(urlEvento)) return;

        if (urlEvento.Length >= 300) Erros.Add("Tamanho da url invalida");
        else if (!string.IsNullOrEmpty(urlEvento))
        {
            if (!urlEvento.Contains("http")) Erros.Add("Url Invalida");
            else SiteEvento = urlEvento;
        }
    }

    public void SetValor(Decimal valor)
    {
        if (valor <= 0)
        {
            Erros.Add("Valor do evento invalido");
            Valor = 0;
        } 
        else
            Valor = valor;
    }

    public void SetAtivo(bool ativo)
    {
        Ativo = (ativo == true) ? 1 : 0;
    }

    public void SetNomeEvento(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            Erros.Add("Nome do evento vazio ou nulo");
            Nome = string.Empty;
        }
        else Nome = nome;
    }

    public void SetDataInicio(DateTime inicio)
    {
        DateTime inicioTratado = new DateTime(inicio.Year, inicio.Month, inicio.Day);
        DateTime dataAtualTratada = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        int compareDate = DateTime.Compare(inicioTratado, dataAtualTratada); 

        if (compareDate < 0)
        {
            Erros.Add($"Data Inicio menor que hoje {inicio.ToString("dd/MM/yyyy")}");
            DataInicio = null;
        }
         else
           DataInicio = inicio;
    }

    public void SetDataFim(DateTime fim)
    {
        DateTime fimTratado = new DateTime(fim.Year, fim.Month, fim.Day);
        DateTime dataAtualTratada = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        int compareDateFim = DateTime.Compare(fimTratado, dataAtualTratada);

        if (compareDateFim < 0)
        {
            Erros.Add($"Data Fim menor que hoje {fim.ToString("dd/MM/yyyy")}");
            DataFim = null;
        }
        else 
            DataFim = fim;
    }

    public void SetDataEvento(DateTime dataEvento)
    {
        DateTime dataEventoTratado = new DateTime(dataEvento.Year, dataEvento.Month, dataEvento.Day);
        DateTime dataAtualTratada = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        int compareDataEvento = DateTime.Compare(dataEventoTratado, dataAtualTratada);

        if (compareDataEvento <= 0)
        {
            Erros.Add($"Data do evento invalida");
            DataEvento = null;
        }
        else
            DataEvento = dataEvento;    
    }

    public void SetPublicoMaximo(int publicoMaximo)
    {
        if (publicoMaximo <= 0) {
            Erros.Add("Evento vazio");
            PublicoMaximo = 0;
        }
        else PublicoMaximo = publicoMaximo;
    }

    public void CriarId()
    {
        if (!Erros.Any()) Id = Guid.NewGuid();
    }

}
