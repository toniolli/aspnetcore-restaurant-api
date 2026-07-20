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

public class ItemPedidoServiceTests
{
    private readonly Mock<IitemPedidoRepository> _repositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly ItemPedidoService _service;

    public ItemPedidoServiceTests()
    {
        _repositoryMock = new Mock<IitemPedidoRepository>();
        _mapperMock = new Mock<IMapper>();

        _service = new ItemPedidoService(
            _repositoryMock.Object,
            _mapperMock.Object);
    }

    [Fact]
    public async Task Deve_Buscar_Item_Por_Id()
    {
        var item = CriarItem();
        var dto = new ItemPedidoDto();

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemPedidoDto>(item))
            .Returns(dto);

        var resultado = await _service.BuscarPorId(1);

        resultado.Should().NotBeNull();
    }

    [Fact]
    public async Task Deve_Atualizar_Item()
    {
        var item = CriarItem();

        var dto = new ItemPedidoDto
        {
            ItemCardapioId = 2,
            SetorProducaoId = 3,
            Quantidade = 5,
            Observacao = "Nova observação"
        };

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _repositoryMock
            .Setup(x => x.Atualizar(item))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemPedidoDto>(item))
            .Returns(dto);

        await _service.Atualizar(1, dto);

        item.ItemCardapioId.Should().Be(2);
        item.SetorProducaoId.Should().Be(3);
        item.Quantidade.Should().Be(5);

        _repositoryMock.Verify(
            x => x.Atualizar(item),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Atualizar_Item_Inexistente()
    {
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemPedido)null);

        Func<Task> acao =
            async () => await _service.Atualizar(
                1,
                new ItemPedidoDto());

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Itens do pedido não encontrado");
    }

    [Fact]
    public async Task Deve_Alterar_Quantidade()
    {
        var item = CriarItem();

        var dto = new ItemPedidoDto();

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemPedidoDto>(item))
            .Returns(dto);

        await _service.AlterarQuantidade(1, 10);

        item.Quantidade.Should().Be(10);

        _repositoryMock.Verify(
            x => x.Atualizar(item),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Alterar_Quantidade_Item_Inexistente()
    {
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemPedido)null);

        Func<Task> acao =
            async () => await _service.AlterarQuantidade(
                1,
                10);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Item não encontrado");
    }

    [Fact]
    public async Task Deve_Alterar_Observacao()
    {
        var item = CriarItem();

        var dto = new ItemPedidoDto();

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemPedidoDto>(item))
            .Returns(dto);

        await _service.AlterarObservacao(
            1,
            "Sem cebola");

        item.Observacao.Should().Be("Sem cebola");

        _repositoryMock.Verify(
            x => x.Atualizar(item),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Alterar_Observacao_Item_Inexistente()
    {
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemPedido)null);

        Func<Task> acao =
            async () => await _service.AlterarObservacao(
                1,
                "Teste");

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Item não encontrado");
    }

    [Fact]
    public async Task Deve_Iniciar_Preparo()
    {
        var item = CriarItem();

        var dto = new ItemPedidoDto();

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemPedidoDto>(item))
            .Returns(dto);

        await _service.IniciarPreparo(1);

        item.Status.Should()
            .Be(StatusItemPedidoEnum.EmPreparo);

        _repositoryMock.Verify(
            x => x.Atualizar(item),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Iniciar_Preparo_Item_Inexistente()
    {
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemPedido)null);

        Func<Task> acao =
            async () => await _service.IniciarPreparo(1);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }

    [Fact]
    public async Task Deve_Cancelar_Item()
    {
        var item = CriarItem();

        var dto = new ItemPedidoDto();

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        _mapperMock
            .Setup(x => x.Map<ItemPedidoDto>(item))
            .Returns(dto);

        await _service.CancelarItem(1);

        item.Status.Should()
            .Be(StatusItemPedidoEnum.Cancelado);

        _repositoryMock.Verify(
            x => x.Atualizar(item),
            Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Cancelar_Item_Inexistente()
    {
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemPedido)null);

        Func<Task> acao =
            async () => await _service.CancelarItem(1);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>();
    }

    [Fact]
    public async Task Deve_Obter_Total()
    {
        var item = CriarItem();

        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync(item);

        var total = await _service.ObterTotal(1);

        total.Should().Be(10);
    }

    [Fact]
    public async Task Nao_Deve_Obter_Total_Item_Inexistente()
    {
        _repositoryMock
            .Setup(x => x.BuscarPorId(1))
            .ReturnsAsync((ItemPedido)null);

        Func<Task> acao =
            async () => await _service.ObterTotal(1);

        await acao.Should()
            .ThrowAsync<DomainExceptionValidation>()
            .WithMessage("Item não encontrado!");
    }

    private static ItemPedido CriarItem()
    {
        return new ItemPedido(
            itemCardapioId: 1,
            setorProducaoId: 1,
            nome: "Hamburguer",
            valorUnitario: 10,
            quantidade: 1,
            observacao: null);
    }
}