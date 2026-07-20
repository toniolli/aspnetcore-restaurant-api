using Application.Dto;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Domain.Validation;
using FluentAssertions;
using Moq;

namespace Triad.Tests.Application.Services;

public class ComandaServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IcomandaRepository> _comandaRepositoryMock;
    private readonly Mock<ImesaRepository> _mesaRepositoryMock;

    private readonly ComandaService _service;

    public ComandaServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _comandaRepositoryMock = new Mock<IcomandaRepository>();
        _mesaRepositoryMock = new Mock<ImesaRepository>();

        _service = new ComandaService(
            _mapperMock.Object,
            _comandaRepositoryMock.Object,
            _mesaRepositoryMock.Object);
    }

    [Fact]
    public async Task Deve_Buscar_Comanda_Por_Id()
    {
        // Arrange

        var comanda = new Comanda(1);

        var dto = new ComandaDto
        {
            MesaId = 1
        };

        _comandaRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(comanda);

        _mapperMock
            .Setup(x => x.Map<ComandaDto>(comanda))
            .Returns(dto);

        // Act

        var resultado = await _service.BuscarPorId(1);

        // Assert

        resultado.Should().NotBeNull();
        resultado.MesaId.Should().Be(1);
    }

    [Fact]
    public async Task Deve_Adicionar_Comanda()
    {
        // Arrange

        var mesa = new Mesa(1);

        var dto = new ComandaDto
        {
            MesaId = 1
        };

        var comanda = new Comanda(1);

        _mesaRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(mesa);

        _mapperMock
            .Setup(x => x.Map<Comanda>(dto))
            .Returns(comanda);

        _mapperMock
            .Setup(x => x.Map<ComandaDto>(comanda))
            .Returns(dto);

        // Act

        var resultado = await _service.Adicionar(dto);

        // Assert

        resultado.Should().NotBeNull();

        mesa.StatusMesa.Should()
            .Be(StatusMesaEnum.Ocupada);

        _mesaRepositoryMock.Verify(
            x => x.Atualizar(mesa),
            Times.Once);

        _comandaRepositoryMock.Verify(
            x => x.Adicionar(comanda),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Adicionar_Comanda_Para_Mesa_Inexistente()
    {
        // Arrange

        _mesaRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Mesa)null);

        var dto = new ComandaDto
        {
            MesaId = 1
        };

        // Act

        Func<Task> acao =
            async () => await _service.Adicionar(dto);

        // Assert

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Mesa não encontrada");
    }

    [Fact]
    public async Task Nao_Deve_Adicionar_Comanda_Para_Mesa_Ocupada()
    {
        // Arrange

        var mesa = new Mesa(1);

        mesa.OcuparMesa();

        _mesaRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(mesa);

        var dto = new ComandaDto
        {
            MesaId = 1
        };

        // Act

        Func<Task> acao =
            async () => await _service.Adicionar(dto);

        // Assert

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Mesa já está ocupada.");
    }

    [Fact]
    public async Task Deve_Atualizar_Comanda()
    {
        // Arrange

        var comanda = new Comanda(1);

        var dto = new ComandaDto
        {
            MesaId = 5
        };

        _comandaRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(comanda);

        _comandaRepositoryMock
            .Setup(x => x.Atualizar(comanda))
            .ReturnsAsync(comanda);

        _mapperMock
            .Setup(x => x.Map<ComandaDto>(comanda))
            .Returns(dto);

        // Act

        await _service.Atualizar(1, dto);

        // Assert

        comanda.MesaId.Should().Be(5);

        _comandaRepositoryMock.Verify(
            x => x.Atualizar(comanda),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Atualizar_Comanda_Inexistente()
    {
        // Arrange

        _comandaRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Comanda)null);

        // Act

        Func<Task> acao =
            async () => await _service.Atualizar(
                1,
                new ComandaDto());

        // Assert

        await acao.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Comanda não encontrada!");
    }

    [Fact]
    public async Task Deve_Fechar_Comanda()
    {
        // Arrange

        var mesa = new Mesa(1);
        mesa.OcuparMesa();

        var comanda = new Comanda(1);

        _comandaRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(comanda);

        _mesaRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(mesa);

        _mapperMock
            .Setup(x => x.Map<ComandaDto>(comanda))
            .Returns(new ComandaDto());

        // Act

        await _service.FecharComanda(1);

        // Assert

        comanda.StatusComanda
            .Should()
            .Be(StatusComandaEnum.Fechada);

        mesa.StatusMesa
            .Should()
            .Be(StatusMesaEnum.Livre);

        _comandaRepositoryMock.Verify(
            x => x.Atualizar(comanda),
            Times.Once);

        _mesaRepositoryMock.Verify(
            x => x.Atualizar(mesa),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Fechar_Comanda_Inexistente()
    {
        // Arrange

        _comandaRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Comanda)null);

        // Act

        Func<Task> acao =
            async () => await _service.FecharComanda(1);

        // Assert

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Comanda não encontrada");
    }

    [Fact]
    public async Task Deve_Remover_Comanda()
    {
        // Act

        await _service.Remover(1);

        // Assert

        _comandaRepositoryMock.Verify(
            x => x.Remover(1),
            Times.Once);
    }
}