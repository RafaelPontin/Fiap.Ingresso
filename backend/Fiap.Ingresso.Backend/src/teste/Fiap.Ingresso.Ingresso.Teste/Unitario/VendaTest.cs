using AppDomain = Fiap.Ingresso.Ingresso.API.Domain;

namespace Fiap.Ingresso.Ingresso.Teste.Unitario;

public class VendaTest
{
    [Fact]
    public void Deve_Criar_Venda_Corretamente()
    {
        // Arrange
        Guid usuarioId = Guid.NewGuid();
        Guid ingressoId = Guid.NewGuid();

        // Act
        var venda = new AppDomain.Venda(usuarioId, ingressoId);

        // Assert
        Assert.NotEqual(Guid.Empty, venda.Id);
        Assert.Equal(usuarioId, venda.UsuarioId);
        Assert.Equal(ingressoId, venda.IngressoId);
        Assert.Equal(DateTime.Now.Date, venda.DataVenda.Date);
        Assert.Empty(venda.Erros);
    }

    [Fact]
    public void Deve_Retornar_Erros_Ao_Criar_Venda_Com_Parametros_Invalidos()
    {
        // Arrange
        Guid usuarioId = Guid.Empty;
        Guid ingressoId = Guid.Empty;

        // Act
        var venda = new AppDomain.Venda(usuarioId, ingressoId);

        // Assert
        Assert.Equal(2, venda.Erros.Count);
    }
}