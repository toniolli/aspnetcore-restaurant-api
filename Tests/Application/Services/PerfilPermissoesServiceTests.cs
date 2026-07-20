using Application.Dto;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Validation;
using FluentAssertions;
using Moq;

namespace Tests.Application.Services
{
    public class PerfilPermissaoServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IperfilPermissaoRepository> _repositoryMock;

        private readonly PerfilPermissaoService _service;

        public PerfilPermissaoServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IperfilPermissaoRepository>();

            _service = new PerfilPermissaoService(
                _mapperMock.Object,
                _repositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Vincular_Permissao_Ao_Perfil()
        {
            var dto = new PerfilPermissaoDto
            {
                PerfilId = 1,
                PermissaoId = 2
            };

            var entidade =
                new PerfilPermissao(
                    dto.PerfilId,
                    dto.PermissaoId);

            _repositoryMock
                .Setup(x => x.ExisteVinculo(
                    dto.PerfilId,
                    dto.PermissaoId))
                .ReturnsAsync(false);

            _mapperMock
                .Setup(x => x.Map<PerfilPermissao>(dto))
                .Returns(entidade);

            _repositoryMock
                .Setup(x => x.Vincular(It.IsAny<PerfilPermissao>()))
                .ReturnsAsync(entidade);

            _mapperMock
                .Setup(x => x.Map<PerfilPermissaoDto>(entidade))
                .Returns(dto);

            var resultado =
                await _service.Vincular(dto);

            resultado.Should().NotBeNull();

            resultado.PerfilId.Should().Be(1);

            resultado.PermissaoId.Should().Be(2);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Vinculo_Ja_Existir()
        {
            var dto = new PerfilPermissaoDto
            {
                PerfilId = 1,
                PermissaoId = 2
            };

            _repositoryMock
                .Setup(x => x.ExisteVinculo(
                    dto.PerfilId,
                    dto.PermissaoId))
                .ReturnsAsync(true);

            var action =
                () => _service.Vincular(dto);

            await action.Should()
                .ThrowAsync<DomainExceptionValidation>()
                .WithMessage(
                    "Prmissao ja vinculada ao perfil");
        }

        [Fact]
        public async Task Deve_Desvincular_Permissao_Do_Perfil()
        {
            var entidade =
                new PerfilPermissao(1, 2);

            var dto = new PerfilPermissaoDto
            {
                PerfilId = 1,
                PermissaoId = 2
            };

            _repositoryMock
                .Setup(x => x.Desvincular(1, 2))
                .ReturnsAsync(entidade);

            _mapperMock
                .Setup(x => x.Map<PerfilPermissaoDto>(entidade))
                .Returns(dto);

            var resultado =
                await _service.Desvincular(
                    1,
                    2);

            resultado.Should().NotBeNull();

            resultado!.PerfilId.Should().Be(1);

            resultado.PermissaoId.Should().Be(2);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Vinculo_Nao_Existir()
        {
            _repositoryMock
                .Setup(x => x.Desvincular(1, 2))
                .ReturnsAsync((PerfilPermissao?)null);

            var resultado =
                await _service.Desvincular(
                    1,
                    2);

            resultado.Should().BeNull();
        }

        [Fact]
        public async Task Deve_Buscar_Permissoes_De_Um_Perfil()
        {
            var permissoes =
                new List<Permissao>
                {
                    new(
                        "Pedido.Criar",
                        "Descrição")
                };

            var dto =
                new List<PermissaoDto>
                {
                    new()
                    {
                        Nome = "Pedido.Criar",
                        Descricao = "Descrição"
                    }
                };

            _repositoryMock
                .Setup(x => x
                    .BuscarPermissoesPerfil(1))
                .ReturnsAsync(permissoes);

            _mapperMock
                .Setup(x => x
                    .Map<IEnumerable<PermissaoDto>>(
                        permissoes))
                .Returns(dto);

            var resultado =
                await _service
                    .BuscarPermissoesPerfil(1);

            resultado.Should().HaveCount(1);
        }

        [Fact]
        public async Task Deve_Buscar_Perfis_De_Uma_Permissao()
        {
            var perfis =
                new List<Perfil>
                {
                    new(
                        "Garçom",
                        "Descrição")
                };

            var dto =
                new List<PerfilDto>
                {
                    new()
                    {
                        Nome = "Garçom",
                        Descricao = "Descrição"
                    }
                };

            _repositoryMock
                .Setup(x => x
                    .BuscarPerfisPermissao(1))
                .ReturnsAsync(perfis);

            _mapperMock
                .Setup(x => x
                    .Map<IEnumerable<PerfilDto>>(
                        perfis))
                .Returns(dto);

            var resultado =
                await _service
                    .BuscarPerfisPermissao(1);

            resultado.Should().HaveCount(1);
        }
    }
}