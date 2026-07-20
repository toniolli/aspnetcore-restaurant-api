using Domain.Entities;
using Domain.Validation;
using FluentAssertions;

namespace Tests.Domain.Entities
{
    public class UsuarioPerfilTests
    {
        [Fact]
        public void Deve_Criar_UsuarioPerfil_Valido()
        {
            var usuarioPerfil = new UsuarioPerfil(
                "usuario-123",
                1);

            usuarioPerfil.Should().NotBeNull();

            usuarioPerfil.UsuarioId.Should()
                .Be("usuario-123");

            usuarioPerfil.PerfilId.Should()
                .Be(1);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_UsuarioId_For_Vazio()
        {
            var action = () =>
                new UsuarioPerfil(
                    string.Empty,
                    1);

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Usuário inválido");
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_UsuarioId_For_Null()
        {
            var action = () =>
                new UsuarioPerfil(
                    null!,
                    1);

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Usuário inválido");
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_PerfilId_For_Menor_Ou_Igual_A_Zero()
        {
            var action = () =>
                new UsuarioPerfil(
                    "usuario-123",
                    0);

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Perfil inválido");
        }

        [Theory]
        [InlineData("", 0)]
        [InlineData("", -1)]
        [InlineData(" ", 0)]
        [InlineData(" ", -1)]
        public void Deve_Lancar_Excecao_Quando_Dados_Forem_Invalidos(
            string usuarioId,
            int perfilId)
        {
            var action = () =>
                new UsuarioPerfil(
                    usuarioId,
                    perfilId);

            action.Should()
                .Throw<DomainExceptionValidation>();
        }
    }
}