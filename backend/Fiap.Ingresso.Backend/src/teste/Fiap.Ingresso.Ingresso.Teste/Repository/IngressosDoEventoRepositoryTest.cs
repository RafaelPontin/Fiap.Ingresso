using Fiap.Ingresso.Ingresso.API.Data;
using Fiap.Ingresso.Ingresso.API.Domain;
using Fiap.Ingresso.Ingresso.API.Infra;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Ingresso.Teste.Repository;
public class IngressosDoEventoRepositoryTest
{
    [Fact]
    public async void DeveCadastraIngressosDoEvento()
    {
        var options = new DbContextOptionsBuilder<IngressoContext>()
            .UseSqlServer("Server=localhost;Database=FiapIngressoEventoTest1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
            .Options;

        using (var context = new IngressoContext(options))
        {
            context.Database.EnsureCreated();

            var repository = new IngressosDoEventoRepository(context);
            IngressosDoEvento ingressosDoEvento = new IngressosDoEvento(Guid.NewGuid(),100,100,200,DateTime.Now.AddDays(10));

            // Act
            await repository.CadastraIngressosDoEvento(ingressosDoEvento);

            // Assert
            Assert.NotEqual(default, ingressosDoEvento.Id);
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }

    [Fact]
    public async void DeveRetornarIngressosDoEventoPorIdENulo()
    {
        var options = new DbContextOptionsBuilder<IngressoContext>()
            .UseSqlServer("Server=localhost;Database=FiapIngressoEventoTest2;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
            .Options;

        using (var context = new IngressoContext(options))
        {
            context.Database.EnsureCreated();

            var repository = new IngressosDoEventoRepository(context);
            IngressosDoEvento ingressosDoEvento = new IngressosDoEvento(Guid.NewGuid(),100,100,200,DateTime.Now.AddDays(10));

            // Act
            await repository.CadastraIngressosDoEvento(ingressosDoEvento);
            var ingressosDoEventoRetornado = await repository.ObterIngressosDoEventoPorId(ingressosDoEvento.Id);
            var ingressosDoEventoRetornadoNulo = await repository.ObterIngressosDoEventoPorId(Guid.NewGuid());

            // Assert
            Assert.Null(ingressosDoEventoRetornadoNulo);
            Assert.NotNull(ingressosDoEventoRetornado);
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }

    [Fact]
    public async void DeveRetornarIngressosDoEventoPorEventoENulo()
    {
        var options = new DbContextOptionsBuilder<IngressoContext>()
            .UseSqlServer("Server=localhost;Database=FiapIngressoEventoTest3;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
            .Options;

        using (var context = new IngressoContext(options))
        {
            context.Database.EnsureCreated();

            var repository = new IngressosDoEventoRepository(context);
            IngressosDoEvento ingressosDoEvento = new IngressosDoEvento(Guid.NewGuid(),100,100,200,DateTime.Now.AddDays(10));

            // Act
            await repository.CadastraIngressosDoEvento(ingressosDoEvento);
            var ingressosDoEventoRetornado = await repository.ObterIngressosDoEventoPorEvento(ingressosDoEvento.EventoId);
            var ingressosDoEventoRetornadoNulo = await repository.ObterIngressosDoEventoPorEvento(Guid.NewGuid());

            // Assert
            Assert.Null(ingressosDoEventoRetornadoNulo);
            Assert.NotNull(ingressosDoEventoRetornado);
            context.Database.EnsureDeleted();
            context.Dispose();
        }

    }

    [Fact]
    public async void DeveRetornarIngressosDoEventoDisponiveis()
    {
        var options = new DbContextOptionsBuilder<IngressoContext>()
            .UseSqlServer("Server=localhost;Database=FiapIngressoEventoTest4;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True")
            .Options;

        using (var context = new IngressoContext(options))
        {
            context.Database.EnsureCreated();

            var repository = new IngressosDoEventoRepository(context);
            IngressosDoEvento ingressosDoEvento1 = new IngressosDoEvento(Guid.NewGuid(),100,100,200,DateTime.Now.AddDays(10));
            IngressosDoEvento ingressosDoEvento2 = new IngressosDoEvento(Guid.NewGuid(), 100, 100, 200, DateTime.Now.AddDays(5));
            IngressosDoEvento ingressosDoEvento3 = new IngressosDoEvento(Guid.NewGuid(), 100, 2, 200, DateTime.Now.AddDays(2));

            ingressosDoEvento3.ComprarIngressosDoEvento(Guid.NewGuid(), 2);

            // Act
            await repository.CadastraIngressosDoEvento(ingressosDoEvento1);
            await repository.CadastraIngressosDoEvento(ingressosDoEvento2);
            await repository.CadastraIngressosDoEvento(ingressosDoEvento3);

            var ingressosDoEventoRetornado = await repository.ObterIngressosDoEventoDisponiveis();

            // Assert
            Assert.Equal(2, ingressosDoEventoRetornado.Count());
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }


}
