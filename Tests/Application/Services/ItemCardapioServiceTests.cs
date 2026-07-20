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

public class ItemCardapioServiceTests
{
    private readonly Mock<IitemCardapioRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ItemCardapioService _service;

    public ItemCardapioServiceTests()
    {
        _repositoryMock = new Mock<IitemCardapioRepository>();
        _mapperMock = new Mock<IMapper>();

        _service = new ItemCardapioService(
            _repositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task Deve_Buscar_Por_Id()
    {
        var item = CriarItem();
        var dto = new ItemCardapioDto();

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemCardapioDto>(item))
            .Returns(dto);

        var resultado = await _service.BuscarPorId(1);

        resultado.Should().NotBeNull();
    }

    [Fact]
    public async Task Deve_Buscar_Por_Nome()
    {
        var item = CriarItem();
        var dto = new ItemCardapioDto();

        _repositoryMock
            .Setup(x => x.BuscarPorNome("Hamburguer"))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemCardapioDto>(item))
            .Returns(dto);

        var resultado =
            await _service.BuscarPorNome("Hamburguer");

        resultado.Should().NotBeNull();
    }

    [Fact]
    public async Task Deve_Alterar_Nome()
    {
        var item = CriarItem();

        var dto = new ItemCardapioDto();

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemCardapioDto>(item))
            .Returns(dto);

        await _service.AlterarNome(
            1,
            "X-Bacon");

        item.Nome.Should().Be("X-Bacon");

        _repositoryMock.Verify(
            x => x.Atualizar(item),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Alterar_Nome_Item_Inexistente()
    {
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemCardapio)null);

        Func<Task> acao =
            async () => await _service.AlterarNome(
                1,
                "X-Bacon");

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Item cardapio não encontrado");
    }

    [Fact]
    public async Task Deve_Alterar_Descricao()
    {
        var item = CriarItem();

        var dto = new ItemCardapioDto();

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemCardapioDto>(item))
            .Returns(dto);

        await _service.AlterarDescricao(
            1,
            "Nova descrição");

        item.Descricao.Should()
            .Be("Nova descrição");

        _repositoryMock.Verify(
            x => x.Atualizar(item),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Alterar_Descricao_Item_Inexistente()
    {
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemCardapio)null);

        Func<Task> acao =
            async () => await _service.AlterarDescricao(
                1,
                "Teste");

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }

    [Fact]
    public async Task Deve_Alterar_Preco()
    {
        var item = CriarItem();

        var dto = new ItemCardapioDto();

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemCardapioDto>(item))
            .Returns(dto);

        await _service.AlterarPreco(1, 50);

        item.Preco.Should().Be(50);

        _repositoryMock.Verify(
            x => x.Atualizar(item),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Alterar_Preco_Item_Inexistente()
    {
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemCardapio)null);

        Func<Task> acao =
            async () => await _service.AlterarPreco(
                1,
                50);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }

    [Fact]
    public async Task Deve_Alterar_Categoria()
    {
        var item = CriarItem();

        var dto = new ItemCardapioDto();

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemCardapioDto>(item))
            .Returns(dto);

        await _service.AlterarCategoria(
            1,
            CategoriaEnum.Bebida);

        item.Categoria.Should()
            .Be(CategoriaEnum.Bebida);

        _repositoryMock.Verify(
            x => x.Atualizar(item),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Alterar_Categoria_Item_Inexistente()
    {
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemCardapio)null);

        Func<Task> acao =
            async () => await _service.AlterarCategoria(
                1,
                CategoriaEnum.Bebida);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Item cardapio não encontrado!");
    }

    [Fact]
    public async Task Deve_Atualizar_Item_Cardapio()
    {
        var item = CriarItem();

        var dto = new ItemCardapioDto
        {
            Nome = "Pizza",
            Preco = 60
        };

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _repositoryMock
            .Setup(x => x.Atualizar(item))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemCardapioDto>(item))
            .Returns(dto);

        await _service.Atualizar(1, dto);

        item.Nome.Should().Be("Pizza");
        item.Preco.Should().Be(60);

        _repositoryMock.Verify(
            x => x.Atualizar(item),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Atualizar_Item_Cardapio_Inexistente()
    {
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemCardapio)null);

        Func<Task> acao =
            async () => await _service.Atualizar(
                1,
                new ItemCardapioDto());

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Itens do cardapio não encontrado");
    }

    private static ItemCardapio CriarItem()
    {
        return new ItemCardapio(
            nome: "Hamburguer",
            descricao: "Artesanal",
            preco: 25,
            categoria: CategoriaEnum.Prato);
    }
}