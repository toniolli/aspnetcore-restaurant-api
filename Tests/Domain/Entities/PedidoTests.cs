using Domain.Entities;
using Domain.Enum;
using Domain.Validation;
using FluentAssertions;

namespace Triad.Tests.Domain.Entities;

public class PedidoTests
{
    [Fact]
    public void Deve_Criar_Pedido_Pendente()
    {
        // Arrange & Act
        var pedido = new Pedido(1, 1);

        // Assert
        pedido.MesaId.Should().Be(1);
        pedido.ComandaId.Should().Be(1);
        pedido.Status.Should().Be(StatusPedidoEnum.Pendente);
        pedido.DataCriacao.Should().NotBe(default);
    }

    [Fact]
    public void Deve_Atualizar_Pedido()
    {
        // Arrange
        var pedido = new Pedido(1, 1);

        // Act
        pedido.AtualizarPedido(2, 3);

        // Assert
        pedido.MesaId.Should().Be(2);
        pedido.ComandaId.Should().Be(3);
    }

    [Fact]
    public void Deve_Cancelar_Pedido()
    {
        // Arrange
        var pedido = new Pedido(1, 1);

        // Act
        pedido.CancelarPedido();

        // Assert
        pedido.Status.Should().Be(StatusPedidoEnum.Cancelado);
    }

    [Fact]
    public void Nao_Deve_Cancelar_Pedido_Nao_Pendente()
    {
        // Arrange
        var pedido = new Pedido(1, 1);

        pedido.CancelarPedido();

        // Act
        Action acao = () => pedido.CancelarPedido();

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage(
                "O pedido só pode ser cancelado se estiver pendente");
    }

    [Fact]
    public void Nao_Deve_Adicionar_Item_Nulo()
    {
        // Arrange
        var pedido = new Pedido(1, 1);

        // Act
        Action acao = () => pedido.AdicionarItem(null);

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage(
                "Item do pedido é obrigatório");
    }

    [Fact]
    public void Deve_Adicionar_Item_Ao_Pedido()
    {
        // Arrange
        var pedido = new Pedido(1, 1);

        var item = new ItemPedido(
            itemCardapioId: 1,
            setorProducaoId: 1,
            nome: "Hamburguer",
            valorUnitario: 10,
            quantidade: 2,
            observacao: null);

        // Act
        pedido.AdicionarItem(item);

        // Assert
        pedido.ListaItemPedidos.Should().HaveCount(1);
        pedido.ListaItemPedidos.Should().Contain(item);
    }

    [Fact]
    public void Deve_Retornar_Zero_Quando_Nao_Houver_Itens()
    {
        // Arrange
        var pedido = new Pedido(1, 1);

        // Act
        var total = pedido.TotalPedido();

        // Assert
        total.Should().Be(0);
    }

    [Fact]
    public void Deve_Calcular_Total_Do_Pedido()
    {
        // Arrange
        var pedido = new Pedido(1, 1);

        var item1 = new ItemPedido(
            itemCardapioId: 1,
            setorProducaoId: 1,
            nome: "Hamburguer",
            valorUnitario: 10,
            quantidade: 2,
            observacao: null);

        var item2 = new ItemPedido(
            itemCardapioId: 2,
            setorProducaoId: 1,
            nome: "Refrigerante",
            valorUnitario: 5,
            quantidade: 3,
            observacao: null);

        pedido.AdicionarItem(item1);
        pedido.AdicionarItem(item2);

        // Act
        var total = pedido.TotalPedido();

        // Assert
        total.Should().Be(35);
    }

    [Fact]
    public void Nao_Deve_Remover_Item_Inexistente()
    {
        // Arrange
        var pedido = new Pedido(1, 1);

        // Act
        Action acao = () => pedido.RemoverItem(999);

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage(
                "Item do pedido não encontrado");
    }

    [Fact]
    public void Nao_Deve_Finalizar_Pedido_Sem_Itens()
    {
        // Arrange
        var pedido = new Pedido(1, 1);

        // Act
        Action acao = () => pedido.FinalizarPedido();

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage(
                "Pedido não possui itens");
    }
}