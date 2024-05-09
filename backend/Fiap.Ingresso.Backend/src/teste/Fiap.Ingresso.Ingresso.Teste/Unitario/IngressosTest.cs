using AppDomain = Fiap.Ingresso.Ingresso.API.Domain;

namespace Fiap.Ingresso.Ingresso.Teste.Unitario;

public class IngressosTest
{
    [Fact]
    public void Deve_Criar_Ingressos_Corretamente()
    {
        // Arrange
        Guid usuarioId = Guid.NewGuid();
        Guid eventoId = Guid.NewGuid();

        // Act
        var venda = new AppDomain.Ingresso(usuarioId, eventoId);

        // Assert
        Assert.NotEqual(Guid.Empty, venda.Id);
        Assert.Equal(usuarioId, venda.UsuarioId);
        Assert.Equal(eventoId, venda.EventoId);
        Assert.Equal(DateTime.Now.Date, venda.DataVenda.Date);
        Assert.Empty(venda.Erros);
    }

    [Fact]
    public void Deve_Retornar_Erros_Ao_Criar_Ingressos_Com_Parametros_Invalidos()
    {
        // Arrange
        Guid usuarioId = Guid.Empty;
        Guid eventoId = Guid.Empty;

        // Act
        var venda = new AppDomain.Ingresso(usuarioId, eventoId);

        // Assert
        Assert.Equal(2, venda.Erros.Count);
        Assert.Contains("UsuarioId é obrigatório", venda.Erros);
        Assert.Contains("EventoId é obrigatório", venda.Erros);
    }
}