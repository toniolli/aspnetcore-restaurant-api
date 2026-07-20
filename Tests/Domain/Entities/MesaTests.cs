using Domain.Entities;
using Domain.Enum;
using Domain.Validation;
using FluentAssertions;

namespace Triad.Tests.Domain.Entities;

public class MesaTests
{
    [Fact]
    public void Deve_Ocupar_Mesa()
    {
        // Arrange
        var mesa = new Mesa(1);

        // Act
        mesa.OcuparMesa();

        // Assert
        mesa.StatusMesa.Should().Be(StatusMesaEnum.Ocupada);
    }

    [Fact]
    public void Nao_Deve_Ocupar_Mesa_Ja_Ocupada()
    {
        // Arrange
        var mesa = new Mesa(1);
        mesa.OcuparMesa();

        // Act
        Action acao = () => mesa.OcuparMesa();

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Mesa já está ocupada");
    }

    [Fact]
    public void Deve_Liberar_Mesa()
    {
        // Arrange
        var mesa = new Mesa(1);
        mesa.OcuparMesa();

        // Act
        mesa.LiberarMesa();

        // Assert
        mesa.StatusMesa.Should().Be(StatusMesaEnum.Livre);
    }

    [Fact]
    public void Nao_Deve_Liberar_Mesa_Ja_Livre()
    {
        // Arrange
        var mesa = new Mesa(1);

        // Act
        Action acao = () => mesa.LiberarMesa();

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("A mesa já está livre.");
    }

    [Fact]
    public void Deve_Atualizar_Numero_Da_Mesa()
    {
        // Arrange
        var mesa = new Mesa(1);

        // Act
        mesa.AtualizarMesa(10);

        // Assert
        mesa.NumeroMesa.Should().Be(10);
    }

    [Fact]
    public void Nao_Deve_Atualizar_Mesa_Com_Numero_Invalido()
    {
        // Arrange
        var mesa = new Mesa(1);

        // Act
        Action acao = () => mesa.AtualizarMesa(0);

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Numero da mesa não pode ser vazio!");
    }

    [Fact]
    public void Deve_Criar_Mesa_Com_Numero_Valido()
    {
        // Arrange & Act
        var mesa = new Mesa(1);

        // Assert
        mesa.Should().NotBeNull();
        mesa.NumeroMesa.Should().Be(1);
        mesa.StatusMesa.Should().Be(StatusMesaEnum.Livre);
    }

    [Fact]
    public void Nao_Deve_Criar_Mesa_Com_Numero_Invalido()
    {
        // Act
        Action acao = () => new Mesa(0);

        // Assert
        acao.Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Numero da mesa não pode ser vazio!");
    }
}