using Domain.Entities;
using Domain.Validation;
using FluentAssertions;

namespace Tests.Domain.Entities
{
    public class PerfilPermissaoTests
    {
        [Fact]
        public void Deve_Criar_PerfilPermissao_Valido()
        {
            var perfilPermissao = new PerfilPermissao(
                1,
                2);

            perfilPermissao.Should().NotBeNull();

            perfilPermissao.PerfilId.Should().Be(1);

            perfilPermissao.PermissaoId.Should().Be(2);
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_PerfilId_For_Menor_Ou_Igual_A_Zero()
        {
            var action = () =>
                new PerfilPermissao(
                    0,
                    1);

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Perfil inválido");
        }

        [Fact]
        public void Deve_Lancar_Excecao_Quando_PermissaoId_For_Menor_Ou_Igual_A_Zero()
        {
            var action = () =>
                new PerfilPermissao(
                    1,
                    0);

            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Permissão inválida");
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(-1, -1)]
        public void Deve_Lancar_Excecao_Quando_Ids_Forem_Invalidos(
            int perfilId,
            int permissaoId)
        {
            var action = () =>
                new PerfilPermissao(
                    perfilId,
                    permissaoId);

            action.Should()
                .Throw<DomainExceptionValidation>();
        }
    }
}