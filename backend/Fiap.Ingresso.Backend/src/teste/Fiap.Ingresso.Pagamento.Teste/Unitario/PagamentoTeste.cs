using Fiap.Ingresso.Pagamento.API.Domain.Enum;
using domain = Fiap.Ingresso.Pagamento.API.Domain;

namespace Fiap.Ingresso.Pagamento.Teste.Unitario;

public class PagamentoTeste
{
    [Fact]
    public void AdicionarPagamentoCartao_CartaoValido_PagamentoAdicionado()
    {
        // Arrange
        var pagamento = GetPagamento();

        // Assert
        Assert.Equal(EPagamento.Cartao, pagamento.TipoPagamento);
        Assert.NotNull(pagamento.ValorPagamento);
        Assert.NotNull(pagamento.NumeroCartao);
        Assert.NotNull(pagamento.IngressoId);
        Assert.NotNull(pagamento.NomeCartao);
        Assert.NotNull(pagamento.VencimentoCartao);
        Assert.NotNull(pagamento.CodigoVerificador);
        Assert.NotNull(pagamento.Id);
        Assert.NotNull(pagamento.DataPagamento);
    }

    [Fact]
    public void AdicionarPagamentoBoleto_BoletoValido_PagamentoAdicionado()
    {
        // Arrange
        var pagamento = new domain.Pagamento();
        var ingressoId = Guid.NewGuid();
        var valorPagamento = 100.00m;

        // Act
        pagamento.AdicionarPagamentoBoleto(ingressoId, valorPagamento);

        // Assert
        Assert.Equal(EPagamento.Boleto, pagamento.TipoPagamento);
        Assert.Equal(valorPagamento, pagamento.ValorPagamento);
        Assert.NotNull(pagamento.IngressoId);
        Assert.NotNull(pagamento.Id);
        Assert.NotNull(pagamento.DataPagamento);
        Assert.NotNull(pagamento.LinhaDigitavel);
    }

    [Fact]
    public void SetValorPagamento_ValorNegativo_ErroAdicionado()
    {
        // Arrange
        var pagamento = new domain.Pagamento();
        var valorPagamento = -100.00m;

        // Act
        pagamento.SetValorPagamento(valorPagamento);

        // Assert
        Assert.Single(pagamento.Erros);
    }

    [Fact]
    public void Gera_linha_digitavel()
    {
        // Arrange
        var pagamento = GetPagamento();
        // Assert
        pagamento.GerarLinhaDigitavel();

        Assert.NotNull(pagamento.LinhaDigitavel);
    }

    [Fact]
    public void Gera_id_valido()
    {
        var pagamento = new domain.Pagamento();
        pagamento.GeraId();
        Assert.NotNull(pagamento.Id);
    }

    [Fact]
    public void Gerar_validaPagamento()
    {
        var pagamento = GetPagamento();

        pagamento.ValidaPagamento();

        Assert.Equal(pagamento.PagamentoValido, EPagamentoValido.Valido);
    }

    [Fact]
    public void Gera_cartao_credito()
    {
        var numeroCartao = "1234567890123456";
        var nomeCartao = "FULANO DA SILVA";
        var vencimento = "12/25";
        var codigoVerificador = "123";

        var pagamento = new domain.Pagamento();

        pagamento.SetCartao(numeroCartao, nomeCartao, vencimento, codigoVerificador);

        Assert.Equal(pagamento.NumeroCartao, numeroCartao);
        Assert.Equal(pagamento.NomeCartao, nomeCartao);
        Assert.Equal(pagamento.VencimentoCartao, vencimento);
        Assert.Equal(pagamento.CodigoVerificador, codigoVerificador);
    }

    [Fact]
    public void Atribui_ingresso()
    {
        var pagamento = new domain.Pagamento();
        pagamento.SetIngresso(Guid.NewGuid());

        Assert.NotNull(pagamento.IngressoId);
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
}
