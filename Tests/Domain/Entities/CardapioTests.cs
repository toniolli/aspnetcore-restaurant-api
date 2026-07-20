using Domain.Entities;
using Domain.Validation;
using FluentAssertions;

namespace Triad.Tests.Domain.Entities;

public class CardapioTests
{
    [Fact]
    public void Deve_Criar_Cardapio()
    {
        // Arrange & Act
        var cardapio = new Cardapio("Lanches");

        // Assert
        cardapio.Nome.Should().Be("Lanches");
        cardapio.Itens.Should().BeEmpty();
    }

    [Fact]
    public void Deve_Atualizar_Nome_Do_Cardapio()
    {
        // Arrange
        var cardapio = new Cardapio("Lanches");

        // Act
        cardapio.AtualizarCardapio("Bebidas");

        // Assert
        cardapio.Nome.Should().Be("Bebidas");
    }

    [Fact]
    public void Nao_Deve_Adicionar_Item_Nulo()
    {
        // Arrange
        var cardapio = new Cardapio("Lanches");

        // Act
        Action acao = () => cardapio.AdicionarItem(null);

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Item do cardápio é obrigatório");
    }

    [Fact]
    public void Deve_Adicionar_Item_Ao_Cardapio()
    {
        // Arrange
        var cardapio = new Cardapio("Lanches");

        var item = CriarItemCardapio();

        // Act
        cardapio.AdicionarItem(item);

        // Assert
        cardapio.Itens.Should().HaveCount(1);
        cardapio.Itens.Should().Contain(item);
    }

    [Fact]
    public void Nao_Deve_Remover_Item_Inexistente()
    {
        // Arrange
        var cardapio = new Cardapio("Lanches");

        // Act
        Action acao = () => cardapio.RemoverItem(999);

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Item não encontrado no cardápio");
    }

    private static ItemCardapio CriarItemCardapio()
    {
        return new ItemCardapio(
            nome: "Hambúrguer",
            descricao: "Pão, carne e queijo",
            preco: 25,
            categoria: CategoriaEnum.Prato);
    }
}