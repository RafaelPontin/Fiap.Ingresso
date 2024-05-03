using AppDomain = Fiap.Ingresso.Ingresso.API.Domain;

namespace Fiap.Ingresso.Ingresso.Teste.Unitario;

public class IngressoTest
{
    [Fact]
    public void Deve_Criar_Ingresso_Corretamente()
    {
        // Arrange
        Guid eventoId = Guid.NewGuid();
        int total = 100;
        int disponiveis = 100;
        decimal preco = 50.00m;
        DateTime dataFim = DateTime.Now.AddDays(7);

        // Act
        var ingresso = new AppDomain.Ingresso(eventoId, total, disponiveis, preco, dataFim);

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
    public void Deve_Retornar_Erros_Ao_Criar_Ingresso_Com_Parametros_Invalidos()
    {
        // Arrange
        Guid eventoId = Guid.Empty;
        int total = -20;
        int disponiveis = -10;
        decimal preco = -50;
        DateTime dataFim = DateTime.Now.AddDays(-7);

        // Act
        var ingresso = new AppDomain.Ingresso(eventoId, total, disponiveis, preco, dataFim);

        // Assert
        Assert.NotEqual(Guid.Empty, ingresso.Id);
        Assert.NotEmpty(ingresso.Erros);
        Assert.Equal(6, ingresso.Erros.Count);
    }

    [Fact]
    public void Deve_Vender_Ingresso_Corretamente()
    {
        // Arrange
        var ingresso = new AppDomain.Ingresso(Guid.NewGuid(), 100, 100, 50, DateTime.Now.AddDays(7));
        Guid usuarioId = Guid.NewGuid();

        // Act
        var venda = ingresso.Vender(usuarioId);

        // Assert
        Assert.Equal(99, ingresso.Disponiveis);
        Assert.Single(ingresso.Vendas);
        Assert.Equal(usuarioId, venda.UsuarioId);
        Assert.Equal(ingresso.Id, venda.IngressoId);
    }

    [Fact]
    public void Deve_Desativar_Ingresso_Ao_Expirar()
    {
        // Arrange
        var ingresso = new AppDomain.Ingresso(Guid.NewGuid(), 100, 100, 50.00m, DateTime.Now.AddDays(-1));

        // Act
        ingresso.Vender(Guid.NewGuid());

        // Assert
        Assert.False(ingresso.Ativo);
        Assert.Contains("Ingresso expirado", ingresso.Erros);
    }

    [Fact]
    public void Deve_Desativar_Ingresso_Ao_Esgotar()
    {
        // Arrange
        var ingresso = new AppDomain.Ingresso(Guid.NewGuid(), 100, 0, 50.00m, DateTime.Now.AddDays(7));

        // Act
        ingresso.Vender(Guid.NewGuid());

        // Assert
        Assert.False(ingresso.Ativo);
        Assert.Contains("Ingressos Esgotados ou Indisponível", ingresso.Erros);
    }
}
