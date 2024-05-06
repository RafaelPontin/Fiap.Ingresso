using Fiap.Ingresso.Ingresso.API.Data;
using Fiap.Ingresso.Ingresso.API.Domain;
using Fiap.Ingresso.Ingresso.API.Infra;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Ingresso.Teste.Repository;
public class IngressoRepositoryTest
{
    [Fact]
    public async void DeveCadastraIngresso()
    {
        var options = new DbContextOptionsBuilder<IngressoContext>()
            .UseSqlServer("Server=localhost;Database=FiapIngressoEventoTest10;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
            .Options;

        using (var context = new IngressoContext(options))
        {
            context.Database.EnsureCreated();

            var usuarioId = Guid.NewGuid();
            var ingressoDoEventoRepository = new IngressosDoEventoRepository(context);
            var ingressoRepository = new IngressoRepository(context);
            IngressosDoEvento ingressosDoEvento = new IngressosDoEvento(Guid.NewGuid(), 100, 100,200,DateTime.Now.AddDays(10));

            // Act
            await ingressoDoEventoRepository.CadastraIngressosDoEvento(ingressosDoEvento);
            var ingressosDoEventoRecuperado = await ingressoDoEventoRepository.ObterIngressosDoEventoPorId(ingressosDoEvento.Id);

            var ingressos = ingressosDoEvento.ComprarIngressosDoEvento(usuarioId, 2);
            await ingressoRepository.ComprarIngressos(ingressosDoEventoRecuperado, ingressos);

            // Assert
            Assert.Equal(98, ingressosDoEventoRecuperado.Disponiveis);
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }

    [Fact]
    public async void DeveRetornarIngressoPorUsuarioId()
    {
        var options = new DbContextOptionsBuilder<IngressoContext>()
            .UseSqlServer("Server=localhost;Database=FiapIngressoEventoTest11;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
            .Options;

        using (var context = new IngressoContext(options))
        {
            context.Database.EnsureCreated();

            var usuarioId = Guid.NewGuid();
            var ingressoDoEventoRepository = new IngressosDoEventoRepository(context);
            var ingressoRepository = new IngressoRepository(context);
            IngressosDoEvento ingressosDoEvento = new IngressosDoEvento(Guid.NewGuid(), 100, 100, 200, DateTime.Now.AddDays(10));

            // Act
            await ingressoDoEventoRepository.CadastraIngressosDoEvento(ingressosDoEvento);
            var ingressosDoEventoRecuperado = await ingressoDoEventoRepository.ObterIngressosDoEventoPorId(ingressosDoEvento.Id);

            var ingressos = ingressosDoEventoRecuperado.ComprarIngressosDoEvento(usuarioId, 2);
            await ingressoRepository.ComprarIngressos(ingressosDoEventoRecuperado, ingressos);

            var ingressosRecuperado = await ingressoRepository.ObterHistoricoDeIngressosPorUsuario(usuarioId);

            // Assert
            foreach (var ingresso in ingressosRecuperado)
            {
                Assert.Equal(ingresso.UsuarioId, usuarioId);
            }
            Assert.Equal(2, ingressosRecuperado.Count());
            Assert.Equal(98, ingressosDoEventoRecuperado.Disponiveis);
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }

    [Fact]
    public async void DeveRetornarListaVaziaAoTentarRetornarIngressoPorUsuarioId()
    {
        var options = new DbContextOptionsBuilder<IngressoContext>()
            .UseSqlServer("Server=localhost;Database=FiapIngressoEventoTest12;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
            .Options;

        using (var context = new IngressoContext(options))
        {
            context.Database.EnsureCreated();
            var ingressoRepository = new IngressoRepository(context);

            // Act
            var ingressosRecuperado = await ingressoRepository.ObterHistoricoDeIngressosPorUsuario(Guid.NewGuid());

            // Assert
            Assert.Empty(ingressosRecuperado);
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }

}
