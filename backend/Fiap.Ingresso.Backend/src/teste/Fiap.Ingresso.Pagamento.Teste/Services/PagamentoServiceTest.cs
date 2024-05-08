using Fiap.Ingresso.Pagamento.API.Domain.Enum;
using Fiap.Ingresso.Pagamento.API.DTOs;
using Fiap.Ingresso.Pagamento.API.Infra;
using Fiap.Ingresso.Pagamento.API.Services;
using Moq;
using domain = Fiap.Ingresso.Pagamento.API.Domain;

namespace Fiap.Ingresso.Pagamento.Teste.Services;

public class PagamentoServiceTest
{
    [Fact]
    public async Task CadastraEvento_Success()
    {
        // Arrange
        var repositoryMock = new Mock<IPagamentoRepository>();
        repositoryMock.Setup(repo => repo.GravaPagamento(It.IsAny<domain.Pagamento>())).Returns(Task.CompletedTask);

        var service = new PagamentoService(repositoryMock.Object);
        var pagamentoDto = new CadastrarPagamento()
        {
            CodigoVerificador = "123",
            IngressoId = Guid.NewGuid(),
            NomeCartao = "teste",
            NumeroCartao = "123456789",
            TipoPagamento = domain.Enum.EPagamento.Cartao,
            ValorPagamento = 200,
            VencimentoCartao = "11/05"
        }; 

        // Act
        var result = await service.CadastraPagamento(pagamentoDto);

        // Assert
        Assert.Equal(201, result.Status);
        Assert.NotEqual(Guid.Empty, result.Data);
    }

    [Fact]
    public async Task GetPagamentoById_Success()
    {
        // Arrange
        var pagamentoId = Guid.NewGuid();
        var pagamentoMock = GetPagamento();

        var repositoryMock = new Mock<IPagamentoRepository>();
        repositoryMock.Setup(repo => repo.GetPagamentoById(It.IsAny<Guid>())).ReturnsAsync(pagamentoMock);

        var service = new PagamentoService(repositoryMock.Object);

        // Act
        var result = await service.GetPagamentoById(pagamentoId);

        // Assert
        Assert.Equal(200, result.Status);
    }


    private domain.Pagamento GetPagamento()
    {
        var pagamento = new domain.Pagamento();
        var ingressoId = Guid.NewGuid();
        var valorPagamento = 100.00m;
        var numeroCartao = "1234567890123456";
        var nomeCartao = "FULANO DA SILVA";
        var vencimento = "12/25";
        var codigoVerificador = "123";

        pagamento.AdicionarPagamentoCartao(ingressoId, valorPagamento, numeroCartao, nomeCartao, vencimento, codigoVerificador);

        return pagamento;
    }


    [Fact]
    public async Task GetLinhaDigitavel_Success()
    {
        // Arrange
        var pagamentoId = Guid.NewGuid();
        var linhaDigitavel = "1234567890";

        var pagamentoMock = GetPagamento();
        pagamentoMock.GerarLinhaDigitavel();

        var repositoryMock = new Mock<IPagamentoRepository>();
        repositoryMock.Setup(repo => repo.GetPagamentoById(It.IsAny<Guid>())).ReturnsAsync(pagamentoMock);

        var service = new PagamentoService(repositoryMock.Object);

        // Act
        var result = await service.GetLinhaDigitavel(pagamentoId);

        // Assert
        Assert.Equal(400, result.Status);
    }
}
