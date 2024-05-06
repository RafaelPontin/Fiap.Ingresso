using Fiap.Ingresso.Ingresso.API.Domain;
using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Fiap.Ingresso.Ingresso.API.Services;
using Moq;

namespace Fiap.Ingresso.Ingresso.Teste.Servicos;
public class IngressosDoEventoServiceTest
{
    Mock<IIngressosDoEventoRepository> _ingressosDoEventoRepositoryMock =  new Mock<IIngressosDoEventoRepository>();

    [Fact]
    public async Task DeveCadastrarIngressosDoEvento()
    {
        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        var dto = new CadastrarIngressosDoEventoDto()
        {
            EventoId = Guid.NewGuid(),
            Total = 10,
            Disponiveis = 5,
            Preco = 10,
            DataFim = DateTime.Now.AddDays(10)
        };

        var result = await ingressosDoEventoService.CadastraIngressosDoEvento(dto);

        Assert.True(result.Data);
        Assert.Equal(201, result.Status);
    }

    [Fact]
    public async Task DeveRetornarErroAoCadastrarIngressosDoEvento()
    {
        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        var dto = new CadastrarIngressosDoEventoDto()
        {
            EventoId = Guid.NewGuid(),
            Total = 10,
            Disponiveis = 5,
            Preco = 10,
            DataFim = DateTime.Now
        };

        var result = await ingressosDoEventoService.CadastraIngressosDoEvento(dto);

        Assert.False(result.Data);
        Assert.Equal(400, result.Status);
    }

    [Fact]
    public async Task DeveRetornarErroAoCadastrarIngressosDoEventoComErroNoRepositorio()
    {
        _ingressosDoEventoRepositoryMock.Setup(x => x.CadastraIngressosDoEvento(It.IsAny<IngressosDoEvento>())).Throws(new Exception());

        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        var dto = new CadastrarIngressosDoEventoDto()
        {
            EventoId = Guid.NewGuid(),
            Total = 10,
            Disponiveis = 5,
            Preco = 10,
            DataFim = DateTime.Now.AddDays(10)
        };

        var result = await ingressosDoEventoService.CadastraIngressosDoEvento(dto);

        Assert.False(result.Data);
        Assert.Equal(500, result.Status);
    }

    [Fact]
    public async Task DeveRetornarIngressosDoEventoDisponiveis()
    {
        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoDisponiveis()).ReturnsAsync(new List<IngressosDoEvento>()
        {
            new IngressosDoEvento(Guid.NewGuid(), 10, 5, 10, DateTime.Now.AddDays(10))
        });

        var result = await ingressosDoEventoService.ObterIngressosDoEventoDisponiveis();

        Assert.NotNull(result.Data);
        Assert.Equal(200, result.Status);
    }

    [Fact]
    public async Task DeveRetornarNotFoundAoTentarObterIngressosDoEventoDisponiveis()
    {
        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoDisponiveis()).ReturnsAsync(new List<IngressosDoEvento>());

        var result = await ingressosDoEventoService.ObterIngressosDoEventoDisponiveis();

        Assert.Null(result.Data);
        Assert.Equal(404, result.Status);
    }

    [Fact]
    public async Task DeveRetornarErroAoObterIngressosDoEventoDisponiveisComErroNoRepositorio()
    {
        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoDisponiveis()).Throws(new Exception());

        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        var result = await ingressosDoEventoService.ObterIngressosDoEventoDisponiveis();

        Assert.Null(result.Data);
        Assert.Equal(500, result.Status);
    }

    [Fact]
    public async Task DeveRetornarIngressosDoEventoPorEvento()
    {
        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        var eventoId = Guid.NewGuid();

        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoPorEvento(eventoId)).ReturnsAsync(new IngressosDoEvento(eventoId, 10, 5, 10, DateTime.Now.AddDays(10)));

        var result = await ingressosDoEventoService.ObterIngressosDoEventoPorEvento(eventoId);

        Assert.NotNull(result.Data);
        Assert.Equal(200, result.Status);
    }

    [Fact]
    public async Task DeveRetornarNotFoundAoTentarObterIngressosDoEventoPorEvento()
    {
        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        var eventoId = Guid.NewGuid();

        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoPorEvento(eventoId)).ReturnsAsync((IngressosDoEvento)null);

        var result = await ingressosDoEventoService.ObterIngressosDoEventoPorEvento(eventoId);

        Assert.Null(result.Data);
        Assert.Equal(404, result.Status);
    }

    [Fact]
    public async Task DeveRetornarErroAoObterIngressosDoEventoPorEventoComErroNoRepositorio()
    {
        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoPorEvento(It.IsAny<Guid>())).Throws(new Exception());

        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        var result = await ingressosDoEventoService.ObterIngressosDoEventoPorEvento(Guid.NewGuid());

        Assert.Null(result.Data);
        Assert.Equal(500, result.Status);
    }

    [Fact]
    public async Task DeveRetornarIngressosDoEventoPorId()
    {
        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        var ingressoId = Guid.NewGuid();

        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoPorId(ingressoId)).ReturnsAsync(new IngressosDoEvento(Guid.NewGuid(), 10, 5, 10, DateTime.Now.AddDays(10)));

        var result = await ingressosDoEventoService.ObterIngressosDoEventoPorId(ingressoId);

        Assert.NotNull(result.Data);
        Assert.Equal(200, result.Status);
    }

    [Fact]
    public async Task DeveRetornarNotFoundAoTentarObterIngressosDoEventoPorId()
    {
        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        var ingressoId = Guid.NewGuid();

        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoPorId(ingressoId)).ReturnsAsync((IngressosDoEvento)null);

        var result = await ingressosDoEventoService.ObterIngressosDoEventoPorId(ingressoId);

        Assert.Null(result.Data);
        Assert.Equal(404, result.Status);
    }

    [Fact]
    public async Task DeveRetornarErroAoObterIngressosDoEventoPorIdComErroNoRepositorio()
    {
        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoPorId(It.IsAny<Guid>())).Throws(new Exception());

        var ingressosDoEventoService = new IngressosDoEventoService(_ingressosDoEventoRepositoryMock.Object);

        var result = await ingressosDoEventoService.ObterIngressosDoEventoPorId(Guid.NewGuid());

        Assert.Null(result.Data);
        Assert.Equal(500, result.Status);
    }
}
