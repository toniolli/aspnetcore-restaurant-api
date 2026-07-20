using Domain.Entities;
using Domain.Validation;
using FluentAssertions;

namespace Tests.Domain.Entities
{
    public class PerfilTests
    {
        [Fact]
        public void Deve_Criar_Perfil_Valido()
        {
            var perfil = new Perfil(
                "Garçom",
                "Responsável pelo atendimento");

            perfil.Should().NotBeNull();

            perfil.Nome.Should().Be("Garçom");

            perfil.Descricao.Should()
                .Be("Responsável pelo atendimento");

            perfil.Ativo.Should().BeTrue();
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Nome_For_Vazio()
        {
            var action = () =>
                new Perfil(
                    string.Empty,
                    "Descrição");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Nome do perfil é obrigatório");
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_Nome_For_Null()
        {
            var action = () =>
                new Perfil(
                    null!,
                    "Descrição");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Nome do perfil é obrigatório");
        }

        [Fact]
        public void Deve_Atualizar_Perfil()
        {
            var perfil = new Perfil(
                "Garçom",
                "Descrição antiga");

            perfil.Atualizar(
                "Garçom Líder",
                "Nova descrição");

            perfil.Nome.Should()
                .Be("Garçom Líder");

            perfil.Descricao.Should()
                .Be("Nova descrição");
        }

        [Fact]
        public void Deve_Lancar_Excecao_Ao_Atualizar_Com_Nome_Vazio()
        {
            var perfil = new Perfil(
                "Garçom",
                "Descrição");

            var action = () =>
                perfil.Atualizar(
                    string.Empty,
                    "Nova descrição");

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Nome do perfil é obrigatório");
        }

        [Fact]
        public void Deve_Desativar_Perfil()
        {
            var perfil = new Perfil(
                "Garçom",
                "Descrição");

            perfil.Desativar();

            perfil.Ativo.Should().BeFalse();
        }

        [Fact]
        public void Deve_Ativar_Perfil()
        {
            var perfil = new Perfil(
                "Garçom",
                "Descrição");

            perfil.Desativar();

            perfil.Ativo.Should().BeFalse();

            perfil.Ativar();

            perfil.Ativo.Should().BeTrue();
        }
    }
}