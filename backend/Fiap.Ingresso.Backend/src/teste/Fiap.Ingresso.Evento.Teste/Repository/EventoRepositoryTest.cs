using Bogus;
using Fiap.Ingresso.Evento.API.Data;
using Fiap.Ingresso.Evento.API.Infra;
using Microsoft.EntityFrameworkCore;
using Moq;
using AppDomain = Fiap.Ingresso.Evento.API.Domain;

namespace Fiap.Ingresso.Evento.Teste.Repository;

public class EventoRepositoryTest 
{
    private Faker _faker;
    public EventoRepositoryTest()
    {
        _faker = new Faker("pt_BR");
    }

    [Fact]
    public async Task CadastraEvento_ValidEvento_CallsSaveChangesAsync()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EventoContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDatabaseCreate;Trusted_Connection=True;")
            .Options;

        using (var context = new EventoContext(options))
        {
            context.Database.EnsureCreated(); // Garante que o banco de dados foi criado

            var repository = new EventoRepository(context);
            var evento = GetEventoValido();

            // Act
            await repository.CadastraEvento(evento);

            // Assert
            Assert.NotEqual(default, evento.Id); // Verifica se o ID foi atribuído
            Assert.Single(context.Eventos); // Verifica se o evento foi adicionado ao contexto
            context.Database.EnsureDeleted();
            context.Dispose();
        }

    }


    [Fact]
    public async Task AlterarEvento_ValidEvento_CallsSaveChangesAsync()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EventoContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDatabaseUpdate;Trusted_Connection=True;")
            .Options;

        using (var context = new EventoContext(options))
        {
            context.Database.EnsureCreated();

            var repository = new EventoRepository(context);
            var evento = GetEventoValido();
            await repository.CadastraEvento(evento);

            // Act
            evento.SetNomeEvento("Novo Nome do Evento");
            await repository.AlterarEvento(evento);

            // Assert
            var updatedEvento = await context.Eventos.FindAsync(evento.Id);
            Assert.Equal("Novo Nome do Evento", updatedEvento.Nome); // Verifica se o nome do evento foi alterado

            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }


    [Fact]
    public async Task GetEventos_ReturnsListOfEventos()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EventoContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDatabaseList;Trusted_Connection=True;")
            .Options;

        using (var context = new EventoContext(options))
        {
            context.Database.EnsureCreated();

            var repository = new EventoRepository(context);
            var evento1 = GetEventoValido();
            var evento2 = GetEventoValido();
            await repository.CadastraEvento(evento1);
            await repository.CadastraEvento(evento2);

            // Act
            var eventos = await repository.GetEventos();

            // Assert
            Assert.Contains(evento1, eventos); // Verifica se o evento 1 está presente na lista
            Assert.Contains(evento2, eventos); // Verifica se o evento 2 está presente na lista

            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }


    [Fact]
    public async Task GetEventoById_ExistingId_ReturnsEvento()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<EventoContext>()
            .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDatabaseExite;Trusted_Connection=True;")
            .Options;

        using (var context = new EventoContext(options))
        {
            context.Database.EnsureCreated();

            var repository = new EventoRepository(context);
            var evento = GetEventoValido();
            await repository.CadastraEvento(evento);

            // Act
            var result = await repository.GetEventoById((Guid) evento.Id);

            // Assert
            Assert.NotNull(result); // Verifica se o evento retornado não é nulo
            Assert.Equal(evento.Nome, result.Nome); // Verifica se o nome do evento retornado está correto

            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }

    private AppDomain.Evento GetEventoValido()
    {
        AppDomain.Evento evento = new AppDomain.Evento();

        evento.AdicionarEvento(_faker.Name.FullName(),
                                DateTime.Now,
                                DateTime.Now.AddDays(10),
                                DateTime.Now.AddDays(20),
                                _faker.Random.Int(1, 3000),
                                _faker.Address.StreetName(),
                                _faker.Random.Int(1, 10000).ToString(),
                                _faker.Address.City(),
                                _faker.Address.Country(),
                                _faker.Address.ZipCode(),
                                _faker.Random.Decimal(1, 100)
                                );
        return evento;
    }

   
}
