using Application.Dto;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Triad.Controllers;

namespace Triad.Tests.Api.Controllers;

public class MesaControllerTests
{
    private readonly Mock<ImesaService> _serviceMock;
    private readonly MesaController _controller;

    public MesaControllerTests()
    {
        _serviceMock = new Mock<ImesaService>();

        _controller = new MesaController(
            _serviceMock.Object);
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Mesa()
    {
        var dto = new MesaDto
        {
            NumeroMesa = 1
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
    public async Task Deve_Retornar_NotFound_Quando_Mesa_Nao_Existir()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((MesaDto)null);

        var resultado = await _controller.BuscarPorId(1);

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Por_Numero()
    {
        var dto = new MesaDto
        {
            NumeroMesa = 1
        };

        _serviceMock
            .Setup(x => x.BuscarPorNumeroMesa(1))
            .ReturnsAsync(dto);

        var resultado =
            await _controller.BuscarPorNumeroMesa(1);

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Quando_Numero_For_Invalido()
    {
        var resultado =
            await _controller.BuscarPorNumeroMesa(0);

        resultado.Result.Should()
            .BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Numero_Nao_Existir()
    {
        _serviceMock
            .Setup(x => x.BuscarPorNumeroMesa(1))
            .ReturnsAsync((MesaDto)null);

        var resultado =
            await _controller.BuscarPorNumeroMesa(1);

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Todos()
    {
        var lista = new List<MesaDto>
        {
            new() { NumeroMesa = 1 }
        };

        _serviceMock
            .Setup(x => x.BuscarTodos())
            .ReturnsAsync(lista);

        var resultado = await _controller.BuscarTodos();

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Nao_Houver_Mesas()
    {
        _serviceMock
            .Setup(x => x.BuscarTodos())
            .ReturnsAsync((IEnumerable<MesaDto>)null);

        var resultado =
            await _controller.BuscarTodos();

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Adicionar()
    {
        var dto = new MesaDto
        {
            NumeroMesa = 1
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
    public async Task Deve_Retornar_Ok_Ao_Ocupar_Mesa()
    {
        var dto = new MesaDto
        {
            NumeroMesa = 1
        };

        _serviceMock
            .Setup(x => x.BuscarPorNumeroMesa(1))
            .ReturnsAsync(dto);

        var resultado =
            await _controller.OcuparMesa(1);

        resultado.Should()
            .BeOfType<OkResult>();

        _serviceMock.Verify(
            x => x.OcuparMesa(1),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Ao_Ocupar_Mesa_Com_Numero_Invalido()
    {
        var resultado =
            await _controller.OcuparMesa(0);

        resultado.Should()
            .BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Ao_Ocupar_Mesa_Inexistente()
    {
        _serviceMock
            .Setup(x => x.BuscarPorNumeroMesa(1))
            .ReturnsAsync((MesaDto)null);

        var resultado =
            await _controller.OcuparMesa(1);

        resultado.Should()
            .BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Liberar_Mesa()
    {
        var dto = new MesaDto
        {
            NumeroMesa = 1
        };

        _serviceMock
            .Setup(x => x.BuscarPorNumeroMesa(1))
            .ReturnsAsync(dto);

        var resultado =
            await _controller.LiberarMesa(1);

        resultado.Should()
            .BeOfType<OkResult>();

        _serviceMock.Verify(
            x => x.LiberarMesa(1),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Ao_Liberar_Mesa_Com_Numero_Invalido()
    {
        var resultado =
            await _controller.LiberarMesa(0);

        resultado.Should()
            .BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Ao_Liberar_Mesa_Inexistente()
    {
        _serviceMock
            .Setup(x => x.BuscarPorNumeroMesa(1))
            .ReturnsAsync((MesaDto)null);

        var resultado =
            await _controller.LiberarMesa(1);

        resultado.Should()
            .BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Atualizar()
    {
        var dto = new MesaDto
        {
            NumeroMesa = 10
        };

        var resultado =
            await _controller.Atualizar(1, dto);

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
                new MesaDto());

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