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

public class PedidoServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IpedidoRepository> _pedidoRepositoryMock;
    private readonly Mock<IitemCardapioRepository> _itemCardapioRepositoryMock;

    private readonly PedidoService _service;

    public PedidoServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _pedidoRepositoryMock = new Mock<IpedidoRepository>();
        _itemCardapioRepositoryMock = new Mock<IitemCardapioRepository>();

        _service = new PedidoService(
            _mapperMock.Object,
            _pedidoRepositoryMock.Object,
            _itemCardapioRepositoryMock.Object);
    }

    [Fact]
    public async Task Deve_Buscar_Pedido_Por_Id()
    {
        var pedido = new Pedido(1, 1);

        var dto = new PedidoDto
        {
            MesaId = 1,
            ComandaId = 1
        };

        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(pedido);

        _mapperMock
            .Setup(x => x.Map<PedidoDto>(pedido))
            .Returns(dto);

        var resultado = await _service.BuscarPorId(1);

        resultado.Should().NotBeNull();
        resultado.MesaId.Should().Be(1);
    }

    [Fact]
    public async Task Deve_Adicionar_Pedido()
    {
        var dto = new PedidoDto
        {
            MesaId = 1,
            ComandaId = 1
        };

        var pedido = new Pedido(1, 1);

        _mapperMock
            .Setup(x => x.Map<Pedido>(dto))
            .Returns(pedido);

        _pedidoRepositoryMock
            .Setup(x => x.Adicionar(pedido))
            .ReturnsAsync(pedido);

        _mapperMock
            .Setup(x => x.Map<PedidoDto>(pedido))
            .Returns(dto);

        var resultado = await _service.Adicionar(dto);

        resultado.Should().NotBeNull();

        _pedidoRepositoryMock.Verify(
            x => x.Adicionar(pedido),
            Times.Once);
    }

    [Fact]
    public async Task Deve_Atualizar_Pedido()
    {
        var pedido = new Pedido(1, 1);

        var dto = new PedidoDto
        {
            MesaId = 2,
            ComandaId = 3
        };

        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(pedido);

        _pedidoRepositoryMock
            .Setup(x => x.Atualizar(pedido))
            .ReturnsAsync(pedido);

        _mapperMock
            .Setup(x => x.Map<PedidoDto>(pedido))
            .Returns(dto);

        await _service.Atualizar(1, dto);

        pedido.MesaId.Should().Be(2);
        pedido.ComandaId.Should().Be(3);

        _pedidoRepositoryMock.Verify(
            x => x.Atualizar(pedido),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Atualizar_Pedido_Inexistente()
    {
        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Pedido)null);

        Func<Task> acao =
            async () => await _service.Atualizar(
                1,
                new PedidoDto());

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }

    [Fact]
    public async Task Deve_Adicionar_Item_Pedido()
    {
        var pedido = new Pedido(1, 1);

        var itemCardapio = new ItemCardapio(
            "Hamburguer",
            "Artesanal",
            25,
            CategoriaEnum.Prato);

        var itemDto = new ItemPedidoDto
        {
            ItemCardapioId = 1,
            SetorProducaoId = 1,
            Quantidade = 2,
            Observacao = "Sem cebola"
        };

        var pedidoDto = new PedidoDto();

        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(pedido);

        _itemCardapioRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(itemCardapio);

        _mapperMock
            .Setup(x => x.Map<PedidoDto>(pedido))
            .Returns(pedidoDto);

        var resultado = await _service.AdicionarItemPedido(
            1,
            itemDto);

        resultado.Should().NotBeNull();

        pedido.ListaItemPedidos.Should()
            .HaveCount(1);

        _pedidoRepositoryMock.Verify(
            x => x.Atualizar(pedido),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Adicionar_Item_Em_Pedido_Inexistente()
    {
        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Pedido)null);

        var itemDto = new ItemPedidoDto
        {
            ItemCardapioId = 1,
            SetorProducaoId = 1,
            Quantidade = 1
        };

        Func<Task> acao =
            async () => await _service.AdicionarItemPedido(
                1,
                itemDto);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Pedido não encontrado");
    }

    [Fact]
    public async Task Deve_Cancelar_Pedido()
    {
        var pedido = new Pedido(1, 1);

        var dto = new PedidoDto();

        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(pedido);

        _mapperMock
            .Setup(x => x.Map<PedidoDto>(pedido))
            .Returns(dto);

        await _service.CancelarPedido(1);

        pedido.Status.Should()
            .Be(StatusPedidoEnum.Cancelado);

        _pedidoRepositoryMock.Verify(
            x => x.Atualizar(pedido),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Cancelar_Pedido_Inexistente()
    {
        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Pedido)null);

        Func<Task> acao =
            async () => await _service.CancelarPedido(1);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }

    [Fact]
    public async Task Nao_Deve_Finalizar_Pedido_Inexistente()
    {
        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Pedido)null);

        Func<Task> acao =
            async () => await _service.FinalizarPedido(1);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }

    [Fact]
    public async Task Nao_Deve_Finalizar_Pedido_Sem_Itens()
    {
        var pedido = new Pedido(1, 1);

        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(pedido);

        Func<Task> acao =
            async () => await _service.FinalizarPedido(1);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }

    [Fact]
    public async Task Deve_Retornar_Total_Do_Pedido()
    {
        var pedido = new Pedido(1, 1);

        var item = new ItemPedido(
            1,
            1,
            "Hamburguer",
            10,
            2,
            null);

        pedido.AdicionarItem(item);

        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(pedido);

        var total = await _service.TotalPedido(1);

        total.Should().Be(20);
    }

    [Fact]
    public async Task Nao_Deve_Calcular_Total_De_Pedido_Inexistente()
    {
        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Pedido)null);

        Func<Task> acao =
            async () => await _service.TotalPedido(1);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }

    [Fact]
    public async Task Deve_Remover_Item_Pedido()
    {
        // Arrange

        var pedido = new Pedido(1, 1);

        var item = new ItemPedido(
            1,
            1,
            "Hamburguer",
            10,
            1,
            null);

        pedido.AdicionarItem(item);

        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(pedido);

        _mapperMock
            .Setup(x => x.Map<PedidoDto>(pedido))
            .Returns(new PedidoDto());

        // Act

        await _service.RemoverItemPedido(
            1,
            item.Id_ItemPedido);

        // Assert

        _pedidoRepositoryMock.Verify(
            x => x.Atualizar(pedido),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Remover_Item_Pedido_De_Pedido_Inexistente()
    {
        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((Pedido)null);

        Func<Task> acao =
            async () => await _service.RemoverItemPedido(
                1,
                1);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Pedido não encontrado");
    }

    [Fact]
    public async Task Deve_Remover_Pedido()
    {
        await _service.Remover(1);

        _pedidoRepositoryMock.Verify(
            x => x.Remover(1),
            Times.Once);
    }


    [Fact]
    public async Task Nao_Deve_Remover_Item_Inexistente()
    {
        var pedido = new Pedido(1, 1);

        _pedidoRepositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(pedido);

        Func<Task> acao =
            async () => await _service.RemoverItemPedido(
                1,
                999);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Item do pedido não encontrado");
    }


}