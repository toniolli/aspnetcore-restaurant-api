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
    public class UsuarioPerfilServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IusuarioPerfilRepository> _repositoryMock;

        private readonly UsuarioPerfilService _service;

        public UsuarioPerfilServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IusuarioPerfilRepository>();

            _service = new UsuarioPerfilService(
                _mapperMock.Object,
                _repositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Vincular_Perfil_Ao_Usuario()
        {
            var dto = new UsuarioPerfilDto
            {
                UsuarioId = "user-1",
                PerfilId = 1
            };

            var entidade = new UsuarioPerfil(
                dto.UsuarioId,
                dto.PerfilId);

            _repositoryMock
                .Setup(x => x.ExisteVinculo(
                    dto.UsuarioId,
                    dto.PerfilId))
                .ReturnsAsync(false);

            _mapperMock
                .Setup(x => x.Map<UsuarioPerfil>(dto))
                .Returns(entidade);

            _repositoryMock
                .Setup(x => x.Vincular(entidade))
                .ReturnsAsync(entidade);

            _mapperMock
                .Setup(x => x.Map<UsuarioPerfilDto>(entidade))
                .Returns(dto);

            var resultado =
                await _service.Vincular(dto);

            resultado.Should().NotBeNull();

            resultado.UsuarioId.Should()
                .Be("user-1");

            resultado.PerfilId.Should()
                .Be(1);
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Vinculo_Ja_Existir()
        {
            var dto = new UsuarioPerfilDto
            {
                UsuarioId = "user-1",
                PerfilId = 1
            };

            _repositoryMock
                .Setup(x => x.ExisteVinculo(
                    dto.UsuarioId,
                    dto.PerfilId))
                .ReturnsAsync(true);

            var action =
                () => _service.Vincular(dto);

            await action.Should()
                .ThrowAsync<DomainExceptionValidation>()
                .WithMessage(
                    "Perfil já vinculado ao usuário");
        }

        [Fact]
        public async Task Deve_Desvincular_Perfil_Do_Usuario()
        {
            var entidade = new UsuarioPerfil(
                "user-1",
                1);

            var dto = new UsuarioPerfilDto
            {
                UsuarioId = "user-1",
                PerfilId = 1
            };

            _repositoryMock
                .Setup(x => x.Desvincular(
                    "user-1",
                    1))
                .ReturnsAsync(entidade);

            _mapperMock
                .Setup(x => x.Map<UsuarioPerfilDto>(entidade))
                .Returns(dto);

            var resultado =
                await _service.Desvincular(
                    "user-1",
                    1);

            resultado.Should().NotBeNull();

            resultado!.UsuarioId.Should()
                .Be("user-1");

            resultado.PerfilId.Should()
                .Be(1);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Vinculo_Nao_Existir()
        {
            _repositoryMock
                .Setup(x => x.Desvincular(
                    "user-1",
                    1))
                .ReturnsAsync((UsuarioPerfil?)null);

            var resultado =
                await _service.Desvincular(
                    "user-1",
                    1);

            resultado.Should().BeNull();
        }

        [Fact]
        public async Task Deve_Buscar_Perfis_Do_Usuario()
        {
            var perfis = new List<Perfil>
            {
                new Perfil(
                    "Garçom",
                    "Descrição")
            };

            var dto = new List<PerfilDto>
            {
                new()
                {
                    Nome = "Garçom",
                    Descricao = "Descrição"
                }
            };

            _repositoryMock
                .Setup(x => x.BuscarPerfisUsuario(
                    "user-1"))
                .ReturnsAsync(perfis);

            _mapperMock
                .Setup(x => x.Map<IEnumerable<PerfilDto>>(
                    perfis))
                .Returns(dto);

            var resultado =
                await _service.BuscarPerfisUsuario(
                    "user-1");

            resultado.Should().HaveCount(1);
        }

        [Fact]
        public async Task Deve_Buscar_Usuarios_De_Um_Perfil()
        {
            var usuarios = new List<UsuarioPerfil>
            {
                new UsuarioPerfil(
                    "user-1",
                    1),
                new UsuarioPerfil(
                    "user-2",
                    1)
            };

            var dto = new List<UsuarioPerfilDto>
            {
                new()
                {
                    UsuarioId = "user-1",
                    PerfilId = 1
                },
                new()
                {
                    UsuarioId = "user-2",
                    PerfilId = 1
                }
            };

            _repositoryMock
                .Setup(x => x.BuscarUsuariosPerfil(1))
                .ReturnsAsync(usuarios);

            _mapperMock
                .Setup(x => x.Map<IEnumerable<UsuarioPerfilDto>>(
                    usuarios))
                .Returns(dto);

            var resultado =
                await _service.BuscarUsuariosPerfil(1);

            resultado.Should().HaveCount(2);
        }
    }
}