using Application.Dto;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Triad.Controllers;

namespace Triad.Tests.Api.Controllers;

public class SetorProducaoControllerTests
{
    private readonly Mock<IsetorProducaoService> _serviceMock;
    private readonly SetorProducaoController _controller;

    public SetorProducaoControllerTests()
    {
        _serviceMock = new Mock<IsetorProducaoService>();

        _controller = new SetorProducaoController(
            _serviceMock.Object);
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Por_Id()
    {
        var dto = new SetorProducaoDto
        {
            Nome = "Cozinha"
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
    public async Task Deve_Retornar_NotFound_Quando_Setor_Nao_Existir()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((SetorProducaoDto)null);

        var resultado = await _controller.BuscarPorId(1);

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Por_Nome()
    {
        var dto = new SetorProducaoDto
        {
            Nome = "Cozinha"
        };

        _serviceMock
            .Setup(x => x.BuscarPorNome("Cozinha"))
            .ReturnsAsync(dto);

        var resultado =
            await _controller.BuscarPorNome("Cozinha");

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
            .ReturnsAsync((SetorProducaoDto)null);

        var resultado =
            await _controller.BuscarPorNome("Teste");

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Ativos()
    {
        var lista = new List<SetorProducaoDto>
        {
            new() { Nome = "Cozinha" }
        };

        _serviceMock
            .Setup(x => x.BuscarAtivos())
            .ReturnsAsync(lista);

        var resultado =
            await _controller.BuscarAtivos();

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Nao_Houver_Ativos()
    {
        _serviceMock
            .Setup(x => x.BuscarAtivos())
            .ReturnsAsync((IEnumerable<SetorProducaoDto>)null);

        var resultado =
            await _controller.BuscarAtivos();

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Todos()
    {
        var lista = new List<SetorProducaoDto>
        {
            new() { Nome = "Cozinha" }
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
    public async Task Deve_Retornar_NotFound_Quando_Nao_Houver_Setores()
    {
        _serviceMock
            .Setup(x => x.BuscarTodos())
            .ReturnsAsync((IEnumerable<SetorProducaoDto>)null);

        var resultado =
            await _controller.BuscarTodos();

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Adicionar()
    {
        var dto = new SetorProducaoDto
        {
            Nome = "Cozinha"
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
    public async Task Deve_Retornar_BadRequest_Quando_Dto_For_Nulo()
    {
        var resultado =
            await _controller.Adicionar(null);

        resultado.Should()
            .BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Atualizar()
    {
        var dto = new SetorProducaoDto
        {
            Nome = "Bar"
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
                new SetorProducaoDto());

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
}