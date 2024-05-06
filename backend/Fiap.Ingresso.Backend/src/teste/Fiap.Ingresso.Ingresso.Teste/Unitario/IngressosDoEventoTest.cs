using AppDomain = Fiap.Ingresso.Ingresso.API.Domain;

namespace Fiap.Ingresso.Ingresso.Teste.Unitario;

public class IngressosDoEventoTest
{
    [Fact]
    public void Deve_Criar_Ingressos_Do_Evento_Corretamente()
    {
        // Arrange
        Guid eventoId = Guid.NewGuid();
        int total = 100;
        int disponiveis = 100;
        decimal preco = 50.00m;
        DateTime dataFim = DateTime.Now.AddDays(7);

        // Act
        var ingresso = new AppDomain.IngressosDoEvento(eventoId, total, disponiveis, preco, dataFim);

        // Assert
        Assert.Equal(eventoId, ingresso.EventoId);
        Assert.Equal(total, ingresso.Total);
        Assert.Equal(disponiveis, ingresso.Disponiveis);
        Assert.Equal(preco, ingresso.Preco);
        Assert.Equal(dataFim, ingresso.DataFim);
        Assert.True(ingresso.Ativo);
        Assert.Empty(ingresso.Erros);
    }

    [Fact]
    public void Deve_Retornar_Erros_Ao_Criar_Ingressos_Do_Evento_Com_Parametros_Invalidos()
    {
        // Arrange
        Guid eventoId = Guid.Empty;
        int total = -20;
        int disponiveis = -10;
        decimal preco = -50;
        DateTime dataFim = DateTime.Now.AddDays(-7);

        // Act
        var ingresso = new AppDomain.IngressosDoEvento(eventoId, total, disponiveis, preco, dataFim);

        // Assert
        Assert.NotEqual(Guid.Empty, ingresso.Id);
        Assert.NotEmpty(ingresso.Erros);
        Assert.Equal(6, ingresso.Erros.Count);
    }

    [Fact]
    public void Deve_Vender_Ingressos_Do_Evento_Corretamente()
    {
        // Arrange
        Random rand = new Random();
        var ingresso = new AppDomain.IngressosDoEvento(Guid.NewGuid(), 100, 100, 50, DateTime.Now.AddDays(7));
        Guid usuarioId = Guid.NewGuid();
        int quantidade = rand.Next(1, 3);

        // Act
        var vendas = ingresso.ComprarIngressosDoEvento(usuarioId ,quantidade);

        // Assert
        Assert.Equal(ingresso.Erros.Count, 0);
        foreach (var v in vendas)
        {
            Assert.Equal(usuarioId, v.UsuarioId);
            Assert.Equal(ingresso.EventoId, v.EventoId);
            Assert.Equal(ingresso.Disponiveis, ingresso.Total-quantidade);
            Assert.Equal(DateTime.Now.Date, v.DataVenda.Date);
            Assert.Empty(v.Erros);
        }
    }

    [Fact]
    public void Deve_Desativar_Ingressos_Do_Evento_Ao_Expirar()
    {
        // Arrange
        var ingresso = new AppDomain.IngressosDoEvento(Guid.NewGuid(), 100, 100, 50.00m, DateTime.Now.AddDays(-1));

        // Act
        ingresso.ComprarIngressosDoEvento(Guid.NewGuid(),1);

        // Assert
        Assert.False(ingresso.Ativo);
        Assert.Contains("Ingresso expirado", ingresso.Erros);
    }

    [Fact]
    public void Deve_Desativar_Ingressos_Do_Evento_Ao_Esgotar()
    {
        // Arrange
        var ingresso = new AppDomain.IngressosDoEvento(Guid.NewGuid(), 100, 0, 50.00m, DateTime.Now.AddDays(7));

        // Act
        ingresso.ComprarIngressosDoEvento(Guid.NewGuid(), 1);

        // Assert
        Assert.False(ingresso.Ativo);
        Assert.Contains("Ingressos Esgotados ou Indisponível", ingresso.Erros);
    }

    [Fact]
    public void Deve_Falhar_Ingressos_Do_Evento_Com_Quantidade_Zero()
    {
        // Arrange
        var ingresso = new AppDomain.IngressosDoEvento(Guid.NewGuid(), 100, 100, 50.00m, DateTime.Now.AddDays(7));

        // Act
        ingresso.ComprarIngressosDoEvento(Guid.NewGuid(), 0);

        // Assert
        Assert.Equal(ingresso.Erros.Count, 1);
        Assert.Contains("Quantidade de ingressos deve ser maior que 0", ingresso.Erros);
    }
}
