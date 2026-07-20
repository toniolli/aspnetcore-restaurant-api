using Domain.Entities;
using Domain.Enum;
using Domain.Validation;
using FluentAssertions;

namespace Triad.Tests.Domain.Entities;

public class ComandaTests
{
    [Fact]
    public void Deve_Criar_Comanda_Aberta()
    {
        // Arrange & Act
        var comanda = new Comanda(1);

        // Assert
        comanda.MesaId.Should().Be(1);
        comanda.StatusComanda.Should().Be(StatusComandaEnum.Aberta);
        comanda.DataAbertura.Should().NotBe(default);
        comanda.ListaPedidos.Should().BeEmpty();
    }

    [Fact]
    public void Deve_Atualizar_Mesa_Da_Comanda()
    {
        // Arrange
        var comanda = new Comanda(1);

        // Act
        comanda.AtualizarComanda(10);

        // Assert
        comanda.MesaId.Should().Be(10);
    }

    [Fact]
    public void Deve_Fechar_Comanda_Sem_Pedidos()
    {
        // Arrange
        var comanda = new Comanda(1);

        // Act
        comanda.FecharComanda();

        // Assert
        comanda.StatusComanda.Should().Be(StatusComandaEnum.Fechada);
        comanda.DataFechamento.Should().NotBeNull();
    }

    [Fact]
    public void Nao_Deve_Fechar_Comanda_Com_Pedido_Pendente()
    {
        // Arrange
        var comanda = new Comanda(1);

        var pedido = new Pedido(
            mesaId: 1,
            comandaId: 1);

        comanda.ListaPedidos.Add(pedido);

        // Act
        Action acao = () => comanda.FecharComanda();

        // Assert
        acao.Should()
             .Throw<DomainExceptionValidation>()
             .WithMessage("Não pode finalizar a comanda com pedidos em andamento!");
    }

    [Fact]
    public void Deve_Retornar_Zero_Quando_Nao_Houver_Pedidos()
    {
        // Arrange
        var comanda = new Comanda(1);

        // Act
        var total = comanda.TotalComanda();

        // Assert
        total.Should().Be(0);
    }
}