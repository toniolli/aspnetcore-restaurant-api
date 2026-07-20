using Application.Dto;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Triad.Controllers;

namespace Triad.Tests.Api.Controllers;

public class ComandaControllerTests
{
    private readonly Mock<IcomandaService> _serviceMock;
    private readonly ComandaController _controller;

    public ComandaControllerTests()
    {
        _serviceMock = new Mock<IcomandaService>();

        _controller = new ComandaController(
            _serviceMock.Object);
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Por_Id()
    {
        var dto = new ComandaDto();

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
    public async Task Deve_Retornar_NotFound_Quando_Comanda_Nao_Existir()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ComandaDto)null);

        var resultado = await _controller.BuscarPorId(1);

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Comanda_Aberta_Por_Mesa()
    {
        var dto = new ComandaDto();

        _serviceMock
            .Setup(x => x.BuscarComandaAbertaPorMesa(1))
            .ReturnsAsync(dto);

        var resultado =
            await _controller.BuscarComandaAbertaPorMesa(1);

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Quando_MesaId_For_Invalido()
    {
        var resultado =
            await _controller.BuscarComandaAbertaPorMesa(0);

        resultado.Result.Should()
            .BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Comanda_Aberta_Nao_Existir()
    {
        _serviceMock
            .Setup(x => x.BuscarComandaAbertaPorMesa(1))
            .ReturnsAsync((ComandaDto)null);

        var resultado =
            await _controller.BuscarComandaAbertaPorMesa(1);

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Todos()
    {
        _serviceMock
            .Setup(x => x.BuscarTodos())
            .ReturnsAsync(new List<ComandaDto>());

        var resultado =
            await _controller.BuscarTodos();

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Nao_Houver_Comandas()
    {
        _serviceMock
            .Setup(x => x.BuscarTodos())
            .ReturnsAsync((IEnumerable<ComandaDto>)null);

        var resultado =
            await _controller.BuscarTodos();

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Comandas_Fechadas()
    {
        _serviceMock
            .Setup(x => x.BuscarComandasFechadas())
            .ReturnsAsync(new List<ComandaDto>());

        var resultado =
            await _controller.BuscarComandasFechadas();

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Nao_Houver_Comandas_Fechadas()
    {
        _serviceMock
            .Setup(x => x.BuscarComandasFechadas())
            .ReturnsAsync((IEnumerable<ComandaDto>)null);

        var resultado =
            await _controller.BuscarComandasFechadas();

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Adicionar()
    {
        var dto = new ComandaDto();

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
    public async Task Deve_Retornar_Ok_Ao_Fechar_Comanda()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(new ComandaDto());

        var resultado =
            await _controller.FecharComanda(1);

        resultado.Should()
            .BeOfType<OkObjectResult>();

        _serviceMock.Verify(
            x => x.FecharComanda(1),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Ao_Fechar_Comanda_Com_Id_Invalido()
    {
        var resultado =
            await _controller.FecharComanda(0);

        resultado.Should()
            .BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Ao_Fechar_Comanda_Inexistente()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ComandaDto)null);

        var resultado =
            await _controller.FecharComanda(1);

        resultado.Should()
            .BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Atualizar()
    {
        var dto = new ComandaDto();

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
                new ComandaDto());

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