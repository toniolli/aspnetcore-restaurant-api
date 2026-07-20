using Domain.Entities;
using Domain.Validation;
using FluentAssertions;

namespace Tests.Domain.Entities
{
    public class PermissaoTests
    {
        [Fact]
        public void Deve_Criar_Permissao_Valida()
        {
            var permissao = new Permissao(
                "Pedido.Criar",
                "Permite criar pedidos");

            permissao.Should().NotBeNull();

            permissao.Nome.Should()
                .Be("Pedido.Criar");

            permissao.Descricao.Should()
                .Be("Permite criar pedidos");

            permissao.Ativo.Should().BeTrue();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Nome_For_Vazio()
        {
            var action = () =>
                new Permissao(
                    string.Empty,
                    "Descrição");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Nome da permissão é obrigatório");
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Nome_For_Null()
        {
            var action = () =>
                new Permissao(
                    null!,
                    "Descrição");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Nome da permissão é obrigatório");
        }

        [Fact]
        public void Deve_Atualizar_Permissao()
        {
            var permissao = new Permissao(
                "Pedido.Criar",
                "Descrição antiga");

            permissao.Atualizar(
                "Pedido.Cancelar",
                "Nova descrição");

            permissao.Nome.Should()
                .Be("Pedido.Cancelar");

            permissao.Descricao.Should()
                .Be("Nova descrição");
        }

        [Fact]
        public void Deve_Lancar_Excecao_Ao_Atualizar_Com_Nome_Vazio()
        {
            var permissao = new Permissao(
                "Pedido.Criar",
                "Descrição");

            var action = () =>
                permissao.Atualizar(
                    string.Empty,
                    "Nova descrição");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Nome da permissão é obrigatório");
        }

        [Fact]
        public void Deve_Desativar_Permissao()
        {
            var permissao = new Permissao(
                "Pedido.Criar",
                "Descrição");

            permissao.Desativar();

            permissao.Ativo.Should().BeFalse();
        }

        [Fact]
        public void Deve_Ativar_Permissao()
        {
            var permissao = new Permissao(
                "Pedido.Criar",
                "Descrição");

            permissao.Desativar();

            permissao.Ativo.Should().BeFalse();

            permissao.Ativar();

            permissao.Ativo.Should().BeTrue();
        }
    }
}