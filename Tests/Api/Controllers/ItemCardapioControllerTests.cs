using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Triad.Controllers;

namespace Triad.Tests.Api.Controllers;

public class ItemCardapioControllerTests
{
    private readonly Mock<IitemCardapioService> _serviceMock;
    private readonly ItemCardapioController _controller;

    public ItemCardapioControllerTests()
    {
        _serviceMock = new Mock<IitemCardapioService>();

        _controller = new ItemCardapioController(
            _serviceMock.Object);
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Por_Id()
    {
        var dto = new ItemCardapioDto();

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
    public async Task Deve_Retornar_NotFound_Quando_Item_Nao_Existir()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemCardapioDto)null);

        var resultado = await _controller.BuscarPorId(1);

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Por_Nome()
    {
        var dto = new ItemCardapioDto();

        _serviceMock
            .Setup(x => x.BuscarPorNome("Hamburguer"))
            .ReturnsAsync(dto);

        var resultado =
            await _controller.BuscarPorNome("Hamburguer");

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Quando_Nome_For_Invalido()
    {
        var resultado =
            await _controller.BuscarPorNome("");

        resultado.Result.Should()
            .BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Nome_Nao_Existir()
    {
        _serviceMock
            .Setup(x => x.BuscarPorNome("Teste"))
            .ReturnsAsync((ItemCardapioDto)null);

        var resultado =
            await _controller.BuscarPorNome("Teste");

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Disponiveis()
    {
        _serviceMock
            .Setup(x => x.BuscarDisponiveis())
            .ReturnsAsync(new List<ItemCardapioDto>());

        var resultado =
            await _controller.BuscarDisponiveis();

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Nao_Houver_Disponiveis()
    {
        _serviceMock
            .Setup(x => x.BuscarDisponiveis())
            .ReturnsAsync((IEnumerable<ItemCardapioDto>)null);

        var resultado =
            await _controller.BuscarDisponiveis();

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Por_Categoria()
    {
        _serviceMock
            .Setup(x => x.BuscarPorCategoria(CategoriaEnum.Prato))
            .ReturnsAsync(new List<ItemCardapioDto>());

        var resultado =
            await _controller.BuscarPorCategoria(
                CategoriaEnum.Prato);

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Categoria_Nao_Possuir_Itens()
    {
        _serviceMock
            .Setup(x => x.BuscarPorCategoria(CategoriaEnum.Prato))
            .ReturnsAsync((IEnumerable<ItemCardapioDto>)null);

        var resultado =
            await _controller.BuscarPorCategoria(
                CategoriaEnum.Prato);

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Todos()
    {
        _serviceMock
            .Setup(x => x.BuscarTodos())
            .ReturnsAsync(new List<ItemCardapioDto>());

        var resultado =
            await _controller.BuscarTodos();

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Nao_Houver_Itens()
    {
        _serviceMock
            .Setup(x => x.BuscarTodos())
            .ReturnsAsync((IEnumerable<ItemCardapioDto>)null);

        var resultado =
            await _controller.BuscarTodos();

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Atualizar()
    {
        var dto = new ItemCardapioDto();

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
                new ItemCardapioDto());

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
    public async Task Deve_Retornar_Ok_Ao_Alterar_Nome()
    {
        var dto = new ItemCardapioDto
        {
            Nome = "X-Bacon"
        };

        _serviceMock
            .Setup(x => x.AlterarNome(1, dto.Nome))
            .ReturnsAsync(dto);

        var resultado =
            await _controller.AlterarNome(
                1,
                dto);

        resultado.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Ao_Alterar_Nome_Com_Id_Invalido()
    {
        var resultado =
            await _controller.AlterarNome(
                0,
                new ItemCardapioDto());

        resultado.Should()
            .BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Alterar_Descricao()
    {
        var dto = new ItemCardapioDto
        {
            Descricao = "Descrição nova"
        };

        _serviceMock
            .Setup(x => x.AlterarDescricao(
                1,
                dto.Descricao))
            .ReturnsAsync(dto);

        var resultado =
            await _controller.AlterarDescricao(
                1,
                dto);

        resultado.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Ao_Alterar_Descricao_Com_Id_Invalido()
    {
        var resultado =
            await _controller.AlterarDescricao(
                0,
                new ItemCardapioDto());

        resultado.Should()
            .BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Alterar_Preco()
    {
        var dto = new ItemCardapioDto
        {
            Preco = 25
        };

        _serviceMock
            .Setup(x => x.AlterarPreco(
                1,
                dto.Preco))
            .ReturnsAsync(dto);

        var resultado =
            await _controller.AlterarPreco(
                1,
                dto);

        resultado.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Ao_Alterar_Preco_Com_Id_Invalido()
    {
        var resultado =
            await _controller.AlterarPreco(
                0,
                new ItemCardapioDto());

        resultado.Should()
            .BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Alterar_Categoria()
    {
        var dto = new ItemCardapioDto
        {
            Categoria = CategoriaEnum.Bebida
        };

        _serviceMock
            .Setup(x => x.AlterarCategoria(
                1,
                CategoriaEnum.Bebida))
            .ReturnsAsync(dto);

        var resultado =
            await _controller.AlterarCategoria(
                1,
                dto);

        resultado.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Ao_Alterar_Categoria_Com_Id_Invalido()
    {
        var resultado =
            await _controller.AlterarCategoria(
                0,
                new ItemCardapioDto());

        resultado.Should()
            .BeOfType<BadRequestObjectResult>();
    }
}