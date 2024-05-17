using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Fiap.Ingresso.Evento.API.Infra;
using Fiap.Ingresso.Evento.API.Services;
using Fiap.Ingresso.Evento.API.DTOs;
using Bogus;
using AppDomain = Fiap.Ingresso.Evento.API.Domain;

namespace Fiap.Ingresso.Evento.Teste.Services;
public class EventoServiceTest
{
    private readonly Mock<IEventoRepository> _eventoRepositoryMock;
    private readonly EventoService _eventoService;
    private Faker _faker;

    public EventoServiceTest()
    {
        _eventoRepositoryMock = new Mock<IEventoRepository>();
        _eventoService = new EventoService(_eventoRepositoryMock.Object);
        _faker = new Faker("pt_BR");
    }

    [Fact(DisplayName = "Cadastra evento")]
    public async Task Cadastra_evento_teste()
    {
        var dto = new CadastraEventoDto
        {
            Nome = _faker.Name.FullName(),
            DataInicio = DateTime.Now,
            DataFim = DateTime.Now.AddDays(10),
            DataEvento = DateTime.Now.AddDays(20),
            PublicoMaximo =  _faker.Random.Int(1, 3000),
            Logradouro = _faker.Address.StreetName(),
            Numero = _faker.Random.Int(1, 10000).ToString(),
            Cidade = _faker.Address.City(),
            Bairro = _faker.Address.Country(),
            Cep = _faker.Address.ZipCode(),
            Valor = _faker.Random.Decimal(1, 100),
            Ativo = 1,
            Descricao = _faker.Random.String(400)
        };

        _eventoRepositoryMock.Setup(repo => repo.CadastraEvento(It.IsAny<AppDomain.Evento>())).Returns(Task.CompletedTask);

        var result = await _eventoService.CadastraEvento(dto);

        //Assert.Equal((result.Data as Guid)., typeof(Guid));
        Assert.Equal(201, result.Status);
    }

    [Fact(DisplayName = "Alterar evento")]
    public async Task Alterar_cadastro_evento()
    {
        // Arrange
        var dto = new AlterarEventoDTO
        {
            Nome = _faker.Name.FullName(),
            DataInicio = DateTime.Now,
            DataFim = DateTime.Now.AddDays(10),
            DataEvento = DateTime.Now.AddDays(20),
            PublicoMaximo = _faker.Random.Int(1, 3000),
            Logradouro = _faker.Address.StreetName(),
            Numero = _faker.Random.Int(1, 10000).ToString(),
            Cidade = _faker.Address.City(),
            Bairro = _faker.Address.Country(),
            Cep = _faker.Address.ZipCode(),
            Valor = _faker.Random.Decimal(1, 100),
            Ativo = 1,
            Descricao = _faker.Random.String(400),
            Id = Guid.NewGuid()
        };

        _eventoRepositoryMock.Setup(repo => repo.AlterarEvento(It.IsAny<AppDomain.Evento>())).Returns(Task.CompletedTask);

        // Act
        var result = await _eventoService.AlterarEvento(dto);

        // Assert
        Assert.True(result.Data);
        Assert.Equal(201, result.Status);
    }

    [Fact(DisplayName = "Cancela evento")]
    public async Task Cancela_evento()
    {
        // Arrange
        var existingId = Guid.NewGuid();
        _eventoRepositoryMock.Setup(repo => repo.GetEventoById(existingId)).ReturnsAsync(new AppDomain.Evento());
    
        // Act
        var result = await _eventoService.CancelaEvento(existingId);
    
        // Assert
        Assert.Equal(existingId, result.Data);
        Assert.Equal(200, result.Status);
    }

    [Fact(DisplayName = "Listar todos os eventos")]
    public async Task Listar_todos_os_eventos()
    {
        // Arrange
        var evento = GetEventoValido();
        var eventos = new List<AppDomain.Evento> { evento };

        _eventoRepositoryMock.Setup(repo => repo.GetEventos()).ReturnsAsync(eventos);
    
        // Act
        var result = await _eventoService.ListarEvento();
    
        // Assert
        Assert.NotNull(result.Data);
        Assert.Equal(eventos.Count, result.Data.Count);
        Assert.Equal(200, result.Status);
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
