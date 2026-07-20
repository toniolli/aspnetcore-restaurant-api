using Application.Dto;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Triad.Controllers;

namespace Triad.Tests.Api.Controllers;

public class CardapioControllerTests
{
    private readonly Mock<IcardapioService> _serviceMock;
    private readonly CardapioController _controller;

    public CardapioControllerTests()
    {
        _serviceMock = new Mock<IcardapioService>();

        _controller = new CardapioController(
            _serviceMock.Object);
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Por_Id()
    {
        var dto = new CardapioDto
        {
            Nome = "Lanches"
        };

        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(dto);

        var resultado = await _controller.BuscarPorId(1);

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Quando_Id_For_Invalido()
    {
        var resultado = await _controller.BuscarPorId(0);

        resultado.Result.Should()
            .BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Cardapio_Nao_Existir()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((CardapioDto)null);

        var resultado = await _controller.BuscarPorId(1);

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Por_Nome()
    {
        var dto = new CardapioDto
        {
            Nome = "Lanches"
        };

        _serviceMock
            .Setup(x => x.BuscarPorNome("Lanches"))
            .ReturnsAsync(dto);

        var resultado =
            await _controller.BuscarPorNome("Lanches");

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Nome_Nao_Existir()
    {
        _serviceMock
            .Setup(x => x.BuscarPorNome("Teste"))
            .ReturnsAsync((CardapioDto)null);

        var resultado =
            await _controller.BuscarPorNome("Teste");

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Todos()
    {
        var lista = new List<CardapioDto>
        {
            new() { Nome = "Lanches" }
        };

        _serviceMock
            .Setup(x => x.BuscarTodos())
            .ReturnsAsync(lista);

        var resultado =
            await _controller.BuscarTodos();

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Adicionar()
    {
        var dto = new CardapioDto
        {
            Nome = "Lanches"
        };

        var resultado =
            await _controller.Adicionar(dto);

        resultado.Should()
            .BeOfType<OkObjectResult>();

        _serviceMock.Verify(
            x => x.Adicionar(dto),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Ao_Adicionar_Dto_Nulo()
    {
        var resultado =
            await _controller.Adicionar(null);

        resultado.Should()
            .BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Adicionar_Item_Cardapio()
    {
        var cardapio = new CardapioDto
        {
            Nome = "Lanches"
        };

        var item = new ItemCardapioDto
        {
            Nome = "Hamburguer"
        };

        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(cardapio);

        var resultado =
            await _controller.AdicionarItemCardapio(
                1,
                item);

        resultado.Should()
            .BeOfType<OkObjectResult>();

        _serviceMock.Verify(
            x => x.AdicionarItem(1, item),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Ao_Adicionar_Item_Nulo()
    {
        var resultado =
            await _controller.AdicionarItemCardapio(
                1,
                null);

        resultado.Should()
            .BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Ao_Adicionar_Item_Em_Cardapio_Inexistente()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((CardapioDto)null);

        var resultado =
            await _controller.AdicionarItemCardapio(
                1,
                new ItemCardapioDto());

        resultado.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Atualizar()
    {
        var dto = new CardapioDto
        {
            Nome = "Bebidas"
        };

        var resultado =
            await _controller.Atualizar(
                1,
                dto);

        resultado.Should()
            .BeOfType<OkObjectResult>();

        _serviceMock.Verify(
            x => x.Atualizar(1, dto),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Quando_Id_Atualizacao_For_Invalido()
    {
        var resultado =
            await _controller.Atualizar(
                0,
                new CardapioDto());

        resultado.Should()
            .BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Quando_Dto_Atualizacao_For_Nulo()
    {
        var resultado =
            await _controller.Atualizar(
                1,
                null);

        resultado.Should()
            .BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Remover()
    {
        var resultado =
            await _controller.Remover(1);

        resultado.Should()
            .BeOfType<OkResult>();

        _serviceMock.Verify(
            x => x.Remover(1),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Quando_Id_Remocao_For_Invalido()
    {
        var resultado =
            await _controller.Remover(0);

        resultado.Should()
            .BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Remover_Item()
    {
        _serviceMock
            .Setup(x => x.RemoverItem(1, 1))
            .ReturnsAsync(new CardapioDto());

        var resultado =
            await _controller.RemoverItem(
                1,
                1);

        resultado.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Quando_Ids_Remocao_Item_Forem_Invalidos()
    {
        var resultado =
            await _controller.RemoverItem(
                0,
                0);

        resultado.Should()
            .BeOfType<BadRequestObjectResult>();
    }
}