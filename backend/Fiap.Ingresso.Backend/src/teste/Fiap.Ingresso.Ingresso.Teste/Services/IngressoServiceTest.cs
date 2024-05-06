using Fiap.Ingresso.Ingresso.API.Domain;
using Fiap.Ingresso.Ingresso.API.DTOs;
using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Fiap.Ingresso.Ingresso.API.Services;
using Moq;

namespace Fiap.Ingresso.Ingresso.Teste.Servicos;
public class IngressoServiceTest
{
    Mock<IIngressoRepository> _ingressoRepositoryMock = new Mock<IIngressoRepository>();
    Mock<IIngressosDoEventoRepository> _ingressosDoEventoRepositoryMock = new Mock<IIngressosDoEventoRepository>();

    [Fact]
    public async Task DeveCadastrarIngresso()
    {
        var ingressoService = new IngressoService(_ingressoRepositoryMock.Object, _ingressosDoEventoRepositoryMock.Object); ;
        Guid ingressoId = Guid.NewGuid();
        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoPorId(ingressoId)).ReturnsAsync(new IngressosDoEvento(Guid.NewGuid(), 10, 5, 10, DateTime.Now.AddDays(10)));

        var dto = new IngressoDto() { Quantidade = 2, UsuarioId = Guid.NewGuid() };

        var result = await ingressoService.ComprarIngresso(ingressoId, dto.UsuarioId, dto.Quantidade);

        Assert.True(result.Data);
        Assert.Equal(201, result.Status);
    }

    [Fact]
    public async Task DeveRetornarErroAoCadastrarIngresso()
    {
        var ingressoService = new IngressoService(_ingressoRepositoryMock.Object, _ingressosDoEventoRepositoryMock.Object);
        Guid ingressoId = Guid.NewGuid();
        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoPorId(ingressoId)).ReturnsAsync(new IngressosDoEvento(Guid.NewGuid(), 10, 1, 10, DateTime.Now.AddDays(10)));

        var dto = new IngressoDto() { Quantidade = 2, UsuarioId = Guid.NewGuid() };

        var result = await ingressoService.ComprarIngresso(ingressoId, dto.UsuarioId, dto.Quantidade);

        Assert.False(result.Data);
        Assert.Equal(400, result.Status);
    }

    [Fact]
    public async Task DeveRetornarErroAoCadastrarIngressoComErroNoRepositorio()
    {
        _ingressoRepositoryMock.Setup(x => x.ComprarIngressos(It.IsAny<Ingresso.API.Domain.IngressosDoEvento>(), It.IsAny<IEnumerable<Ingresso.API.Domain.Ingresso>>())).Throws(new Exception());

        var ingressoService = new IngressoService(_ingressoRepositoryMock.Object, _ingressosDoEventoRepositoryMock.Object);
        Guid ingressoId = Guid.NewGuid();
        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoPorId(ingressoId)).ReturnsAsync(new IngressosDoEvento(Guid.NewGuid(), 10, 5, 10, DateTime.Now.AddDays(10)));

        var dto = new IngressoDto() { Quantidade = 2, UsuarioId = Guid.NewGuid() };

        var result = await ingressoService.ComprarIngresso(ingressoId, dto.UsuarioId, dto.Quantidade);

        Assert.False(result.Data);
        Assert.Equal(500, result.Status);
    }

    [Fact]
    public async Task DeveRetornarErroAoCadastrarIngressoComErroNoRepositorioIngressosDoEvento()
    {
        var ingressoService = new IngressoService(_ingressoRepositoryMock.Object, _ingressosDoEventoRepositoryMock.Object);
        Guid ingressoId = Guid.NewGuid();
        _ingressosDoEventoRepositoryMock.Setup(x => x.ObterIngressosDoEventoPorId(ingressoId)).Throws(new Exception());

        var dto = new IngressoDto() { Quantidade = 2, UsuarioId = Guid.NewGuid() };

        var result = await ingressoService.ComprarIngresso(ingressoId, dto.UsuarioId, dto.Quantidade);

        Assert.False(result.Data);
        Assert.Equal(500, result.Status);
    }

    [Fact]
    public async Task DeveRetornarHistoricoDeIngressosPorUsuario()
    {
        var ingressoService = new IngressoService(_ingressoRepositoryMock.Object, _ingressosDoEventoRepositoryMock.Object);
        Guid usuarioId = Guid.NewGuid();
        _ingressoRepositoryMock.Setup(x => x.ObterHistoricoDeIngressosPorUsuario(usuarioId)).ReturnsAsync(new List<Ingresso.API.Domain.Ingresso>() { new Ingresso.API.Domain.Ingresso(usuarioId, Guid.NewGuid()) });

        var result = await ingressoService.ObterHistoricoDeIngressosPorUsuario(usuarioId);

        Assert.True(result.Data.Any());
        Assert.Equal(200, result.Status);
        Assert.Equal(usuarioId, result.Data.First().UsuarioId);
    }

    [Fact]
    public async Task DeveRetornarErroAoObterHistoricoDeIngressosPorUsuarioComErroNoRepositorio()
    {
        var ingressoService = new IngressoService(_ingressoRepositoryMock.Object, _ingressosDoEventoRepositoryMock.Object);
        Guid usuarioId = Guid.NewGuid();
        _ingressoRepositoryMock.Setup(x => x.ObterHistoricoDeIngressosPorUsuario(usuarioId)).Throws(new Exception());

        var result = await ingressoService.ObterHistoricoDeIngressosPorUsuario(usuarioId);

        Assert.Equal(result.Data, null);
        Assert.Equal(500, result.Status);
    }

    [Fact]
    public async Task DeveRetornarErroNotFoundAoObterHistoricoDeIngressosPorUsuarioComListaVazia()
    {
        var ingressoService = new IngressoService(_ingressoRepositoryMock.Object, _ingressosDoEventoRepositoryMock.Object);
        Guid usuarioId = Guid.NewGuid();
        _ingressoRepositoryMock.Setup(x => x.ObterHistoricoDeIngressosPorUsuario(usuarioId)).ReturnsAsync(new List<Ingresso.API.Domain.Ingresso>());

        var result = await ingressoService.ObterHistoricoDeIngressosPorUsuario(usuarioId);

        Assert.Equal(result.Data, null);
        Assert.Equal(404, result.Status);
    }
}
