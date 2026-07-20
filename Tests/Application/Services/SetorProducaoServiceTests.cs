using Application.Dto;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace Triad.Tests.Application.Services;

public class SetorProducaoServiceTests
{
    private readonly Mock<IsetorProducaoRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly SetorProducaoService _service;

    public SetorProducaoServiceTests()
    {
        _repositoryMock = new Mock<IsetorProducaoRepository>();
        _mapperMock = new Mock<IMapper>();

        _service = new SetorProducaoService(
            _repositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task Deve_Buscar_Setor_Por_Id()
    {
        // Arrange
        var setor = new SetorProducao("Cozinha");

        var dto = new SetorProducaoDto
        {
            Nome = "Cozinha"
        };

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(setor);

        _mapperMock
            .Setup(x => x.Map<SetorProducaoDto>(setor))
            .Returns(dto);

        // Act
        var resultado = await _service.BuscarPorId(1);

        // Assert
        resultado.Should().NotBeNull();
        resultado.Nome.Should().Be("Cozinha");
    }

    [Fact]
    public async Task Deve_Buscar_Setor_Por_Nome()
    {
        // Arrange
        var setor = new SetorProducao("Cozinha");

        var dto = new SetorProducaoDto
        {
            Nome = "Cozinha"
        };

        _repositoryMock
            .Setup(x => x.BuscarPorNome("Cozinha"))
            .ReturnsAsync(setor);

        _mapperMock
            .Setup(x => x.Map<SetorProducaoDto>(setor))
            .Returns(dto);

        // Act
        var resultado =
            await _service.BuscarPorNome("Cozinha");

        // Assert
        resultado.Should().NotBeNull();
        resultado.Nome.Should().Be("Cozinha");
    }

    [Fact]
    public async Task Deve_Adicionar_Setor()
    {
        // Arrange
        var dto = new SetorProducaoDto
        {
            Nome = "Cozinha"
        };

        var setor = new SetorProducao("Cozinha");

        _mapperMock
            .Setup(x => x.Map<SetorProducao>(dto))
            .Returns(setor);

        _repositoryMock
            .Setup(x => x.Adicionar(setor))
            .ReturnsAsync(setor);

        _mapperMock
            .Setup(x => x.Map<SetorProducaoDto>(setor))
            .Returns(dto);

        // Act
        var resultado = await _service.Adicionar(dto);

        // Assert
        resultado.Should().NotBeNull();

        _repositoryMock.Verify(
            x => x.Adicionar(setor),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Atualizar_Setor()
    {
        // Arrange
        var setor = new SetorProducao("Cozinha");

        var dto = new SetorProducaoDto
        {
            Nome = "Bar"
        };

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(setor);

        _repositoryMock
            .Setup(x => x.Atualizar(setor))
            .ReturnsAsync(setor);

        _mapperMock
            .Setup(x => x.Map<SetorProducaoDto>(setor))
            .Returns(dto);

        // Act
        await _service.Atualizar(1, dto);

        // Assert
        setor.Nome.Should().Be("Bar");

        _repositoryMock.Verify(
            x => x.Atualizar(setor),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Atualizar_Setor_Inexistente()
    {
        // Arrange
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((SetorProducao)null);

        // Act
        Func<Task> acao =
            async () => await _service.Atualizar(
                1,
                new SetorProducaoDto());

        // Assert
        await acao.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Setor não encontrado");
    }

    [Fact]
    public async Task Deve_Remover_Setor()
    {
        // Act
        await _service.Remover(1);

        // Assert
        _repositoryMock.Verify(
            x => x.Remover(1),
            Times.Once);
    }
}