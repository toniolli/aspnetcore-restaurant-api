using Application.Dto;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Triad.Tests.Application.Services;

public class MesaServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ImesaRepository> _mesaRepositoryMock;
    private readonly MesaService _service;

    public MesaServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _mesaRepositoryMock = new Mock<ImesaRepository>();

        _service = new MesaService(
            _mapperMock.Object,
            _mesaRepositoryMock.Object);
    }

    [Fact]
    public async Task Deve_Buscar_Mesa_Por_Id()
    {
        // Arrange
        var mesa = new Mesa(1);

        var dto = new MesaDto
        {
            NumeroMesa = 1
        };

        _mesaRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(mesa);

        _mapperMock
            .Setup(x => x.Map<MesaDto>(mesa))
            .Returns(dto);

        // Act
        var resultado = await _service.BuscarPorId(1);

        // Assert
        resultado.Should().NotBeNull();
        resultado.NumeroMesa.Should().Be(1);
    }

    [Fact]
    public async Task Deve_Ocupar_Mesa()
    {
        // Arrange
        var mesa = new Mesa(1);

        var dto = new MesaDto
        {
            NumeroMesa = 1
        };

        _mesaRepositoryMock
            .Setup(x => x.BuscarPorNumeroMesa(1))
            .ReturnsAsync(mesa);

        _mapperMock
            .Setup(x => x.Map<MesaDto>(mesa))
            .Returns(dto);

        // Act
        var resultado = await _service.OcuparMesa(1);

        // Assert
        mesa.StatusMesa.Should().Be(StatusMesaEnum.Ocupada);
        resultado.Should().NotBeNull();

        _mesaRepositoryMock
            .Verify(x => x.Atualizar(mesa), Times.Once);
    }

    [Fact]
    public async Task Deve_Liberar_Mesa()
    {
        // Arrange
        var mesa = new Mesa(1);

        mesa.OcuparMesa();

        var dto = new MesaDto
        {
            NumeroMesa = 1
        };

        _mesaRepositoryMock
            .Setup(x => x.BuscarPorNumeroMesa(1))
            .ReturnsAsync(mesa);

        _mapperMock
            .Setup(x => x.Map<MesaDto>(mesa))
            .Returns(dto);

        // Act
        var resultado = await _service.LiberarMesa(1);

        // Assert
        mesa.StatusMesa.Should().Be(StatusMesaEnum.Livre);
        resultado.Should().NotBeNull();

        _mesaRepositoryMock
            .Verify(x => x.Atualizar(mesa), Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Ocupar_Mesa_Inexistente()
    {
        // Arrange

        _mesaRepositoryMock
            .Setup(x => x.BuscarPorNumeroMesa(99))
            .ReturnsAsync((Mesa)null);

        // Act
        Func<Task> acao =
            async () => await _service.OcuparMesa(99);

        // Assert

        await acao.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Mesa não encontrada!");
    }

    [Fact]
    public async Task Nao_Deve_Liberar_Mesa_Inexistente()
    {
        // Arrange

        _mesaRepositoryMock
            .Setup(x => x.BuscarPorNumeroMesa(99))
            .ReturnsAsync((Mesa)null);

        // Act
        Func<Task> acao =
            async () => await _service.LiberarMesa(99);

        // Assert

        await acao.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Mesa não encontrada!");
    }

    [Fact]
    public async Task Deve_Remover_Mesa()
    {
        // Act
        await _service.Remover(1);

        // Assert

        _mesaRepositoryMock
            .Verify(
                x => x.Remover(1),
                Times.Once);
    }
}