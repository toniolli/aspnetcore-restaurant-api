using Application.Dto;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Validation;
using FluentAssertions;
using Moq;

namespace Triad.Tests.Application.Services;

public class CardapioServiceTests
{
    private readonly Mock<IcardapioRepository> _cardapioRepositoryMock;
    private readonly Mock<IitemCardapioRepository> _itemCardapioRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly CardapioService _service;

    public CardapioServiceTests()
    {
        _cardapioRepositoryMock = new Mock<IcardapioRepository>();
        _itemCardapioRepositoryMock = new Mock<IitemCardapioRepository>();
        _mapperMock = new Mock<IMapper>();

        _service = new CardapioService(
            _cardapioRepositoryMock.Object,
            _mapperMock.Object,
            _itemCardapioRepositoryMock.Object);
    }

    [Fact]
    public async Task Deve_Buscar_Cardapio_Por_Id()
    {
        var cardapio = new Cardapio("Lanches");

        var dto = new CardapioDto
        {
            Nome = "Lanches"
        };

        _cardapioRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(cardapio);

        _mapperMock
            .Setup(x => x.Map<CardapioDto>(cardapio))
            .Returns(dto);

        var resultado = await _service.BuscarPorId(1);

        resultado.Should().NotBeNull();
        resultado.Nome.Should().Be("Lanches");
    }

    [Fact]
    public async Task Deve_Adicionar_Cardapio()
    {
        var dto = new CardapioDto
        {
            Nome = "Lanches"
        };

        var cardapio = new Cardapio("Lanches");

        _mapperMock
            .Setup(x => x.Map<Cardapio>(dto))
            .Returns(cardapio);

        _cardapioRepositoryMock
            .Setup(x => x.Adicionar(cardapio))
            .ReturnsAsync(cardapio);

        _mapperMock
            .Setup(x => x.Map<CardapioDto>(cardapio))
            .Returns(dto);

        var resultado = await _service.Adicionar(dto);

        resultado.Should().NotBeNull();

        _cardapioRepositoryMock
            .Verify(
                x => x.Adicionar(cardapio),
                Times.Once);
    }

    [Fact]
    public async Task Deve_Atualizar_Cardapio()
    {
        var cardapio = new Cardapio("Lanches");

        var dto = new CardapioDto
        {
            Nome = "Bebidas"
        };

        _cardapioRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(cardapio);

        _cardapioRepositoryMock
            .Setup(x => x.Atualizar(cardapio))
            .ReturnsAsync(cardapio);

        _mapperMock
            .Setup(x => x.Map<CardapioDto>(cardapio))
            .Returns(dto);

        await _service.Atualizar(1, dto);

        cardapio.Nome.Should().Be("Bebidas");

        _cardapioRepositoryMock
            .Verify(
                x => x.Atualizar(cardapio),
                Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Atualizar_Cardapio_Inexistente()
    {
        _cardapioRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Cardapio)null);

        Func<Task> acao =
            async () => await _service.Atualizar(
                1,
                new CardapioDto());

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }

    [Fact]
    public async Task Deve_Adicionar_Item_Ao_Cardapio()
    {
        var cardapio = new Cardapio("Lanches");

        var itemDto = new ItemCardapioDto
        {
            Nome = "Hamburguer",
            Descricao = "Hamburguer artesanal",
            Preco = 25,
            Categoria = CategoriaEnum.Prato
        };

        var cardapioDto = new CardapioDto();

        _cardapioRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(cardapio);

        _mapperMock
            .Setup(x => x.Map<CardapioDto>(cardapio))
            .Returns(cardapioDto);

        var resultado =
            await _service.AdicionarItem(
                1,
                itemDto);

        resultado.Should().NotBeNull();

        cardapio.Itens.Should()
            .HaveCount(1);

        _cardapioRepositoryMock
            .Verify(
                x => x.Atualizar(cardapio),
                Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Adicionar_Item_Em_Cardapio_Inexistente()
    {
        _cardapioRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Cardapio)null);

        var itemDto = new ItemCardapioDto
        {
            Nome = "Hamburguer",
            Descricao = "Teste",
            Preco = 20,
            Categoria = CategoriaEnum.Prato
        };

        Func<Task> acao =
            async () => await _service.AdicionarItem(
                1,
                itemDto);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }

    [Fact]
    public async Task Deve_Remover_Cardapio()
    {
        await _service.Remover(1);

        _cardapioRepositoryMock
            .Verify(
                x => x.Remover(1),
                Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Remover_Item_De_Cardapio_Inexistente()
    {
        _cardapioRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Cardapio)null);

        Func<Task> acao =
            async () => await _service.RemoverItem(
                1,
                1);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }
}