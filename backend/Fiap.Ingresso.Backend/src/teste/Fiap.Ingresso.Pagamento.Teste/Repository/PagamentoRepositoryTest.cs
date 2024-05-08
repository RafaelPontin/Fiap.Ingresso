using Fiap.Ingresso.Pagamento.API.Data;
using Fiap.Ingresso.Pagamento.API.Domain;
using Fiap.Ingresso.Pagamento.API.Infra;
using Microsoft.EntityFrameworkCore;
using domain = Fiap.Ingresso.Pagamento.API.Domain;

namespace Fiap.Ingresso.Pagamento.Teste.Repository;

public class PagamentoRepositoryTest
{
    public PagamentoRepositoryTest()
    {
       
    }


    [Fact]
    public async Task GravaPagamento_Success()
    {

        var options = new DbContextOptionsBuilder<PagamentoContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDatabaseCreatePagamento;Trusted_Connection=True;")
            .Options;


        using(var context = new PagamentoContext(options)) 
        {
            context.Database.EnsureCreated();
            var pagamento = GetPagamento();
            var repository = new PagamentoRepository(context);

            await repository.GravaPagamento(pagamento);

            Assert.NotEqual(default, pagamento.Id);
            Assert.Single(context.Pagamentos);
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    
    }

    [Fact]
    public async Task GetPagamentoById_Success()
    {
        var options = new DbContextOptionsBuilder<PagamentoContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDatabaseCreatePagamentoId;Trusted_Connection=True;")
            .Options;

        using (var context = new PagamentoContext(options))
        {
            context.Database.EnsureCreated();
            var pagamento = GetPagamento();
            var repository = new PagamentoRepository(context);

            await repository.GravaPagamento(pagamento);


            var result = await repository.GetPagamentoById(pagamento.Id);

            Assert.NotNull(result);
            Assert.Equal(pagamento.Id, result.Id);
        }

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
