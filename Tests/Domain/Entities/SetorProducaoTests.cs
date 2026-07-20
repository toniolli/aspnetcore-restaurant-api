using Domain.Entities;
using Domain.Validation;
using FluentAssertions;

namespace Triad.Tests.Domain.Entities;

public class SetorProducaoTests
{
    [Fact]
    public void Deve_Criar_Setor_Ativo()
    {
        // Arrange & Act
        var setor = new SetorProducao("Cozinha");

        // Assert
        setor.Nome.Should().Be("Cozinha");
        setor.Ativo.Should().BeTrue();
        setor.Pedidos.Should().BeEmpty();
    }

    [Fact]
    public void Nao_Deve_Criar_Setor_Com_Nome_Invalido()
    {
        // Act
        Action acao = () => new SetorProducao("");

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Nome do setor é obrigatório");
    }

    [Fact]
    public void Deve_Atualizar_Setor_Producao()
    {
        // Arrange
        var setor = new SetorProducao("Cozinha");

        // Act
        setor.AtualizarSetorProducao("Bar");

        // Assert
        setor.Nome.Should().Be("Bar");
    }

    [Fact]
    public void Deve_Alterar_Nome()
    {
        // Arrange
        var setor = new SetorProducao("Cozinha");

        // Act
        setor.AlterarNome("Padaria");

        // Assert
        setor.Nome.Should().Be("Padaria");
    }

    [Fact]
    public void Nao_Deve_Alterar_Nome_Invalido()
    {
        // Arrange
        var setor = new SetorProducao("Cozinha");

        // Act
        Action acao = () => setor.AlterarNome("");

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Nome inválido");
    }

    [Fact]
    public void Deve_Desativar_Setor()
    {
        // Arrange
        var setor = new SetorProducao("Cozinha");

        // Act
        setor.Desativar();

        // Assert
        setor.Ativo.Should().BeFalse();
    }

    [Fact]
    public void Deve_Ativar_Setor()
    {
        // Arrange
        var setor = new SetorProducao("Cozinha");

        setor.Desativar();

        // Act
        setor.Ativar();

        // Assert
        setor.Ativo.Should().BeTrue();
    }
}