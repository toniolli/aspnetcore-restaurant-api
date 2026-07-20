using Domain.Entities;
using Domain.Enum;
using Domain.Validation;
using FluentAssertions;

namespace Triad.Tests.Domain.Entities;

public class ItemCardapioTests
{
    [Fact]
    public void Deve_Criar_Item_Cardapio_Disponivel()
    {
        // Arrange & Act
        var item = CriarItem();

        // Assert
        item.Nome.Should().Be("Hambúrguer");
        item.Descricao.Should().Be("Pão, carne e queijo");
        item.Preco.Should().Be(25);
        item.Disponivel.Should().BeTrue();
        item.Categoria.Should().Be(CategoriaEnum.Prato);
    }

    [Fact]
    public void Nao_Deve_Criar_Item_Com_Nome_Invalido()
    {
        Action acao = () => new ItemCardapio(
            "",
            "Descrição",
            10,
            CategoriaEnum.Prato);

        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Nome inválido");
    }

    [Fact]
    public void Nao_Deve_Criar_Item_Com_Descricao_Invalida()
    {
        Action acao = () => new ItemCardapio(
            "Hamburguer",
            "",
            10,
            CategoriaEnum.Prato);

        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Descrição inválida");
    }

    [Fact]
    public void Nao_Deve_Criar_Item_Com_Preco_Invalido()
    {
        Action acao = () => new ItemCardapio(
            "Hamburguer",
            "Descrição",
            0,
            CategoriaEnum.Prato);

        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Preço deve ser maior que zero");
    }

    [Fact]
    public void Deve_Atualizar_Item_Cardapio()
    {
        // Arrange
        var item = CriarItem();

        // Act
        item.AtualizarItemCardapio(
            "Pizza",
            "Pizza Grande",
            50,
            CategoriaEnum.Prato);

        // Assert
        item.Nome.Should().Be("Pizza");
        item.Descricao.Should().Be("Pizza Grande");
        item.Preco.Should().Be(50);
        item.Categoria.Should().Be(CategoriaEnum.Prato);
    }

    [Fact]
    public void Deve_Alterar_Nome()
    {
        // Arrange
        var item = CriarItem();

        // Act
        item.AlterarNome("X-Bacon");

        // Assert
        item.Nome.Should().Be("X-Bacon");
    }

    [Fact]
    public void Nao_Deve_Alterar_Nome_Invalido()
    {
        // Arrange
        var item = CriarItem();

        // Act
        Action acao = () => item.AlterarNome("");

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Nome inválido");
    }

    [Fact]
    public void Deve_Alterar_Descricao()
    {
        // Arrange
        var item = CriarItem();

        // Act
        item.AlterarDescricao("Novo texto");

        // Assert
        item.Descricao.Should().Be("Novo texto");
    }

    [Fact]
    public void Nao_Deve_Alterar_Descricao_Invalida()
    {
        // Arrange
        var item = CriarItem();

        // Act
        Action acao = () => item.AlterarDescricao("");

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Descrição inválida");
    }

    [Fact]
    public void Deve_Alterar_Preco()
    {
        // Arrange
        var item = CriarItem();

        // Act
        item.AlterarPreco(30);

        // Assert
        item.Preco.Should().Be(30);
    }

    [Fact]
    public void Nao_Deve_Alterar_Preco_Invalido()
    {
        // Arrange
        var item = CriarItem();

        // Act
        Action acao = () => item.AlterarPreco(0);

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Preço deve ser maior que zero");
    }

    [Fact]
    public void Deve_Alterar_Categoria()
    {
        // Arrange
        var item = CriarItem();

        // Act
        item.AlterarCategoria(CategoriaEnum.Bebida);

        // Assert
        item.Categoria.Should().Be(CategoriaEnum.Bebida);
    }

    [Fact]
    public void Deve_Desativar_Item()
    {
        // Arrange
        var item = CriarItem();

        // Act
        item.Desativar();

        // Assert
        item.Disponivel.Should().BeFalse();
    }

    [Fact]
    public void Deve_Ativar_Item()
    {
        // Arrange
        var item = CriarItem();

        item.Desativar();

        // Act
        item.Ativar();

        // Assert
        item.Disponivel.Should().BeTrue();
    }

    private static ItemCardapio CriarItem()
    {
        return new ItemCardapio(
            nome: "Hambúrguer",
            descricao: "Pão, carne e queijo",
            preco: 25,
            categoria: CategoriaEnum.Prato);
    }
}