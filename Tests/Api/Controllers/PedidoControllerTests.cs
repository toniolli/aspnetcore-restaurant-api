using Application.Dto;
using Application.Interfaces;
using Domain.Enum;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Triad.Controllers;

namespace Triad.Tests.Api.Controllers;

public class PedidoControllerTests
{
    private readonly Mock<IpedidoService> _serviceMock;
    private readonly PedidoController _controller;

    public PedidoControllerTests()
    {
        _serviceMock = new Mock<IpedidoService>();

        _controller = new PedidoController(
            _serviceMock.Object);
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Por_Id()
    {
        var dto = new PedidoDto();

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
    public async Task Deve_Retornar_NotFound_Quando_Pedido_Nao_Existir()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((PedidoDto)null);

        var resultado = await _controller.BuscarPorId(1);

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Todos()
    {
        var lista = new List<PedidoDto>
        {
            new()
        };

        _serviceMock
            .Setup(x => x.BuscarTodos())
            .ReturnsAsync(lista);

        var resultado = await _controller.BuscarTodos();

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Nao_Houver_Pedidos()
    {
        _serviceMock
            .Setup(x => x.BuscarTodos())
            .ReturnsAsync((IEnumerable<PedidoDto>)null);

        var resultado = await _controller.BuscarTodos();

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Pedidos_Abertos()
    {
        var lista = new List<PedidoDto>
        {
            new()
        };

        _serviceMock
            .Setup(x => x.BuscarPedidosAbertos())
            .ReturnsAsync(lista);

        var resultado = await _controller.BuscarPedidosAberto();

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Nao_Houver_Pedidos_Abertos()
    {
        _serviceMock
            .Setup(x => x.BuscarPedidosAbertos())
            .ReturnsAsync((IEnumerable<PedidoDto>)null);

        var resultado = await _controller.BuscarPedidosAberto();

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Buscar_Por_Status()
    {
        var lista = new List<PedidoDto>
        {
            new()
        };

        _serviceMock
            .Setup(x => x.BuscarPorStatus(StatusPedidoEnum.Pendente))
            .ReturnsAsync(lista);

        var resultado =
            await _controller.BuscarPorStatus(
                StatusPedidoEnum.Pendente);

        resultado.Result.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Status_Nao_Possuir_Pedidos()
    {
        _serviceMock
            .Setup(x => x.BuscarPorStatus(StatusPedidoEnum.Pendente))
            .ReturnsAsync((IEnumerable<PedidoDto>)null);

        var resultado =
            await _controller.BuscarPorStatus(
                StatusPedidoEnum.Pendente);

        resultado.Result.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Adicionar()
    {
        var dto = new PedidoDto();

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
    public async Task Deve_Retornar_Ok_Ao_Adicionar_Item_Pedido()
    {
        var pedido = new PedidoDto();

        var item = new ItemPedidoDto();

        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(pedido);

        var resultado =
            await _controller.AdicionarItemPedido(
                1,
                item);

        resultado.Should()
            .BeOfType<OkObjectResult>();

        _serviceMock.Verify(
            x => x.AdicionarItemPedido(1, item),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Quando_PedidoId_For_Invalido()
    {
        var resultado =
            await _controller.AdicionarItemPedido(
                0,
                new ItemPedidoDto());

        resultado.Should()
            .BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Quando_Item_For_Nulo()
    {
        var resultado =
            await _controller.AdicionarItemPedido(
                1,
                null);

        resultado.Should()
            .BeOfType<BadRequestResult>();
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Quando_Pedido_Nao_Existir_Ao_Adicionar_Item()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((PedidoDto)null);

        var resultado =
            await _controller.AdicionarItemPedido(
                1,
                new ItemPedidoDto());

        resultado.Should()
            .BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Atualizar()
    {
        var dto = new PedidoDto();

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
                new PedidoDto());

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
    public async Task Deve_Retornar_Ok_Ao_Cancelar_Pedido()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(new PedidoDto());

        var resultado =
            await _controller.CancelarPedido(1);

        resultado.Should()
            .BeOfType<OkObjectResult>();

        _serviceMock.Verify(
            x => x.CancelarPedido(1),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Ao_Cancelar_Pedido_Inexistente()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((PedidoDto)null);

        var resultado =
            await _controller.CancelarPedido(1);

        resultado.Should()
            .BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task Deve_Retornar_Ok_Ao_Finalizar_Pedido()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(new PedidoDto());

        var resultado =
            await _controller.FinalizarPedido(1);

        resultado.Should()
            .BeOfType<OkObjectResult>();

        _serviceMock.Verify(
            x => x.FinalizarPedido(1),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_NotFound_Ao_Finalizar_Pedido_Inexistente()
    {
        _serviceMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((PedidoDto)null);

        var resultado =
            await _controller.FinalizarPedido(1);

        resultado.Should()
            .BeOfType<NotFoundResult>();
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
    public async Task Deve_Retornar_Ok_Ao_Remover_Item_Pedido()
    {
        _serviceMock
            .Setup(x => x.RemoverItemPedido(1, 1))
            .ReturnsAsync(new PedidoDto());

        var resultado =
            await _controller.RemoverItemPedido(
                1,
                1);

        resultado.Should()
            .BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Deve_Retornar_BadRequest_Quando_Ids_Remocao_Item_Forem_Invalidos()
    {
        var resultado =
            await _controller.RemoverItemPedido(
                0,
                0);

        resultado.Should()
            .BeOfType<BadRequestObjectResult>();
    }
}