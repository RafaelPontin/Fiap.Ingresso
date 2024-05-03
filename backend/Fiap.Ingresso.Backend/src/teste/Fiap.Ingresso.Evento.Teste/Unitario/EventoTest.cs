using Bogus;
using AppDomain = Fiap.Ingresso.Evento.API.Domain;

namespace Fiap.Ingresso.Evento.Teste.Unitario;

public class EventoTest
{
    private Faker _faker;
    public EventoTest()
    {
        _faker = new Faker("pt_BR");
    }

    private AppDomain.Evento GetEventoValido()
    {
        AppDomain.Evento evento = new AppDomain.Evento();

        evento.AdicionarEvento(_faker.Name.FullName(),
                                DateTime.Now,
                                DateTime.Now.AddDays(10),
                                DateTime.Now.AddDays(20),
                                _faker.Random.Int(1, 3000),
                                _faker.Address.StreetName(),
                                _faker.Random.Int(1, 10000).ToString(),
                                _faker.Address.City(),
                                _faker.Address.Country(),
                                _faker.Address.ZipCode(),
                                _faker.Random.Decimal(1, 100)
                                );
        return evento;
    }

    [Fact(DisplayName = "Add evento valido")]
    public void Criar_evento_valido()
    {
        var evento = new AppDomain.Evento();

        evento.AdicionarEvento(_faker.Name.FullName(),
                               DateTime.Now,
                               DateTime.Now.AddDays(10),
                               DateTime.Now.AddDays(20),
                               _faker.Random.Int(1, 3000),
                               _faker.Address.StreetName(),
                               _faker.Random.Int(1, 10000).ToString(),
                               _faker.Address.City(),
                               _faker.Address.Country(),
                               _faker.Address.ZipCode(),
                               _faker.Random.Decimal(1, 100)
                               );

        Assert.Equal(evento.Erros.Count, 0);
    }

    [Fact(DisplayName = "Add evento nome invalido")]
    public void Criar_Evento_Com_nome_invalido()
    {
        var evento = GetEventoValido();

        evento.SetNomeEvento("");

        Assert.Equal(evento.Erros.Count, 1);
        Assert.Empty(evento.Nome);
    }

    [Fact(DisplayName = "Add evento com Data Inicio invalida")]
    public void Criar_evento_data_inicio_invalida()
    {
        var evento = GetEventoValido();

        evento.SetDataInicio(DateTime.Now.AddDays(-2));
        Assert.Equal(evento.Erros.Count, 1);
        Assert.Null(evento.DataInicio);
    }

    [Fact(DisplayName = "Add evento com data fim invalida")]
    public void Criar_evento_Com_DataFinalização_Invalida()
    {
        var evento = GetEventoValido();

        evento.SetDataFim(DateTime.Now.AddDays(-2));
        Assert.Equal(evento.Erros.Count, 1);
        Assert.Null(evento.DataFim);
    }

    [Fact(DisplayName = "Add evento com data evento invalida")]
    public void Criar_evento_Com_DataEvento_Invalida()
    {
        var evento = GetEventoValido();

        evento.SetDataEvento(DateTime.Now);
        Assert.Equal(evento.Erros.Count, 1);
        Assert.Null(evento.DataEvento);
    }

    [Fact(DisplayName = "Add evento com publico maxima invalida ")]
    public void Criar_evento_com_publico_maxima_invalida()
    {
        var evento = GetEventoValido();

        evento.SetPublicoMaximo(-1);

        Assert.Equal(evento.Erros.Count, 1);
        Assert.Equal(evento.PublicoMaximo, 0);

    }

    [Fact(DisplayName = "Add evento com endereco invalido")]
    public void Criar_evento_com_endereco_invalido()
    {
        var evento = GetEventoValido();

        evento.SetEndereco("","","","","");

        Assert.True(evento.Erros.Any());
        Assert.Empty(evento.Logradouro);
        Assert.Empty(evento.Numero);
        Assert.Empty(evento.Cidade);
        Assert.Empty(evento.Bairro);
        Assert.Empty(evento.Cep); 
    }

    [Fact(DisplayName = "Criar Id Evento")]
    public void Criar_id_valido()
    {
        var evento = new AppDomain.Evento();

        evento.CriarId();

        Assert.NotNull(evento.Id);
    }

    [Fact(DisplayName = "Ativa Evento")]
    public void Ativa_evento()
    {
        var evento = GetEventoValido();

        evento.SetAtivo(true);

        Assert.Equal(evento.Ativo, 1);
    }

    [Fact(DisplayName = "Destiva Evento")]
    public void Desativa_evento()
    {
        var evento = GetEventoValido();

        evento.SetAtivo(false);

        Assert.Equal(evento.Ativo, 0);
    }

    [Fact(DisplayName = "Criar evento com valor invalido (negativo)")]
    public void Criar_evento_valor_negativo() 
    {
        var evento = GetEventoValido();

        evento.SetValor(-2);

        Assert.Equal(evento.Erros.Count, 1);
        Assert.Equal(evento.Valor,0);
    }

    [Fact(DisplayName = "Cancelar Evento")]
    public void CancelarEvento()
    {
        var evento = GetEventoValido();

        evento.CancelarEvento();

        Assert.Equal(evento.Ativo, 0);
    }


    [Fact(DisplayName = "Adicionar descricao valida")]
    public void Adicionar_Descricao_valida()
    {
        var evento = GetEventoValido();

        evento.SetDescricao(_faker.Random.String(100));

        Assert.Equal(evento.Erros.Count, 0);
        Assert.NotEmpty(evento.Descricao);
    }

    [Fact(DisplayName = "Adicionar descricao invalida")]
    public void Adicionar_Descricao_Invalida()
    {
        var evento = GetEventoValido();

        evento.SetDescricao(_faker.Random.String(1001));

        Assert.Equal(evento.Erros.Count, 1);
        Assert.Null(evento.Descricao);
    }


    [Fact(DisplayName = "Adicionar ulr valida")]
    public void Adicionar_url_valida()
    {
        var evento = GetEventoValido();

        evento.SetUrlEvento(_faker.Internet.Url());

        Assert.Equal(evento.Erros.Count, 0);
        Assert.NotEmpty(evento.SiteEvento);
    }

    [Fact(DisplayName = "Adicionar ulr invalida")]
    public void Adicionar_url_invalida()
    {
        var evento = GetEventoValido();

        evento.SetUrlEvento("meusite.com");

        Assert.Equal(evento.Erros.Count, 1);
        Assert.Null(evento.SiteEvento);

    }

    [Fact(DisplayName = "Alterar Evento")]
    public void Alterar_evento()
    {
        var evento = GetEventoValido();

        evento.AlterarEvento
            (
                Guid.NewGuid(),
                evento.Nome,
                DateTime.Now,
                DateTime.Now.AddDays(10),
                DateTime.Now.AddDays(20),
                evento.PublicoMaximo,
                evento.Logradouro,
                evento.Numero,
                evento.Cidade,
                evento.Bairro,
                evento.Cep,
                evento.Valor,
                null,
                null
            );

        Assert.False(evento.Erros.Any());

    }

}
