using Domain.Entities;
using Domain.Enum;
using Domain.Validation;
using FluentAssertions;

namespace Triad.Tests.Domain.Entities;

public class ItemPedidoTests
{
    [Fact]
    public void Deve_Criar_ItemPedido_Pendente()
    {
        var item = CriarItem();

        item.Status.Should()
            .Be(StatusItemPedidoEnum.Pendente);
    }

    [Fact]
    public void Nao_Deve_Criar_ItemPedido_Com_Cardapio_Invalido()
    {
        Action acao = () => new ItemPedido(
            0,
            1,
            "Hamburguer",
            10,
            1,
            null);

        acao.Should()
            .Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void Nao_Deve_Criar_ItemPedido_Com_Setor_Invalido()
    {
        Action acao = () => new ItemPedido(
            1,
            0,
            "Hamburguer",
            10,
            1,
            null);

        acao.Should()
            .Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void Nao_Deve_Criar_ItemPedido_Com_Quantidade_Invalida()
    {
        Action acao = () => new ItemPedido(
            1,
            1,
            "Hamburguer",
            10,
            0,
            null);

        acao.Should()
            .Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void Deve_Adicionar_Quantidade()
    {
        var item = CriarItem();

        item.AdicionarQuantidade(2);

        item.Quantidade.Should().Be(3);
    }

    [Fact]
    public void Nao_Deve_Adicionar_Quantidade_Negativa()
    {
        var item = CriarItem();

        Action acao = () => item.AdicionarQuantidade(0);

        acao.Should()
            .Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void Deve_Alterar_Quantidade()
    {
        var item = CriarItem();

        item.AlterarQuantidade(10);

        item.Quantidade.Should().Be(10);
    }

    [Fact]
    public void Nao_Deve_Alterar_Quantidade_Negativa()
    {
        var item = CriarItem();

        Action acao = () => item.AlterarQuantidade(0);

        acao.Should()
            .Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void Deve_Iniciar_Preparo()
    {
        var item = CriarItem();

        item.IniciarPreparo();

        item.Status.Should()
            .Be(StatusItemPedidoEnum.EmPreparo);
    }

    [Fact]
    public void Nao_Deve_Iniciar_Preparo_Se_Nao_Estiver_Pendente()
    {
        var item = CriarItem();

        item.IniciarPreparo();

        Action acao = () => item.IniciarPreparo();

        acao.Should()
            .Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void Deve_Marcar_Como_Pronto()
    {
        var item = CriarItem();

        item.IniciarPreparo();

        item.MarcarComoPronto();

        item.Status.Should()
            .Be(StatusItemPedidoEnum.Pronto);
    }

    [Fact]
    public void Nao_Deve_Marcar_Como_Pronto_Se_Nao_Estiver_Em_Preparo()
    {
        var item = CriarItem();

        Action acao = () => item.MarcarComoPronto();

        acao.Should()
            .Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void Deve_Finalizar_Item()
    {
        var item = CriarItem();

        item.IniciarPreparo();
        item.MarcarComoPronto();

        item.Finalizar();

        item.Status.Should()
            .Be(StatusItemPedidoEnum.Finalizado);
    }

    [Fact]
    public void Nao_Deve_Finalizar_Se_Nao_Estiver_Pronto()
    {
        var item = CriarItem();

        Action acao = () => item.Finalizar();

        acao.Should()
            .Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void Deve_Cancelar_Item()
    {
        var item = CriarItem();

        item.Cancelar();

        item.Status.Should()
            .Be(StatusItemPedidoEnum.Cancelado);
    }

    [Fact]
    public void Nao_Deve_Cancelar_Se_Nao_Estiver_Pendente()
    {
        var item = CriarItem();

        item.IniciarPreparo();

        Action acao = () => item.Cancelar();

        acao.Should()
            .Throw<DomainExceptionValidation>();
    }

    [Fact]
    public void Deve_Alterar_Observacao()
    {
        var item = CriarItem();

        item.AlterarObservacao("Sem cebola");

        item.Observacao.Should()
            .Be("Sem cebola");
    }

    [Fact]
    public void Deve_Limpar_Observacao_Quando_Vazia()
    {
        var item = CriarItem();

        item.AlterarObservacao("");

        item.Observacao.Should()
            .BeNull();
    }

    [Fact]
    public void Deve_Atualizar_Item_Pedido()
    {
        var item = CriarItem();

        item.AtualizarItemPedido(
            2,
            3,
            5,
            "Nova observação");

        item.ItemCardapioId.Should().Be(2);
        item.SetorProducaoId.Should().Be(3);
        item.Quantidade.Should().Be(5);
        item.Observacao.Should().Be("Nova observação");
    }

    [Fact]
    public void Deve_Calcular_Total_Do_Item()
    {
        var item = CriarItem();

        var total = item.ObterTotal();

        total.Should().Be(10);
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