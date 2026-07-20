using Application.Dto;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Triad.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class UsuarioPerfilControllerTests
    {
        private readonly Mock<IUsuarioPerfilService> _serviceMock;
        private readonly UsuarioPerfilController _controller;

        public UsuarioPerfilControllerTests()
        {
            _serviceMock = new Mock<IUsuarioPerfilService>();

            _controller = new UsuarioPerfilController(
                _serviceMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_UsuarioId_For_Nulo()
        {
            var resultado =
                await _controller.BuscarPerfilUsuario(null);

            resultado.Result.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Nao_Existir_PerfilUsuario()
        {
            _serviceMock
                .Setup(x => x.BuscarPerfisUsuario("user1"))
                .ReturnsAsync((IEnumerable<PerfilDto>)null!);

            var resultado =
                await _controller.BuscarPerfilUsuario("user1");

            resultado.Result.Should()
                .BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_Ok_Quando_BuscarPerfilUsuario()
        {
            var perfis = new List<PerfilDto>
            {
                new()
                {
                    Nome = "Administrador",
                    Descricao = "Perfil Administrador",
                    Ativo = true
                }
            };

            _serviceMock
                .Setup(x => x.BuscarPerfisUsuario("user1"))
                .ReturnsAsync(perfis);

            var resultado =
                await _controller.BuscarPerfilUsuario("user1");

            resultado.Result.Should()
                .BeOfType<OkObjectResult>();

            _serviceMock.Verify(
                x => x.BuscarPerfisUsuario("user1"),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_PerfilId_For_Invalido()
        {
            var resultado =
                await _controller.BuscarUsuarioPerfill(0);

            resultado.Result.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Nao_Existir_UsuarioPerfil()
        {
            _serviceMock
                .Setup(x => x.BuscarUsuariosPerfil(1))
                .ReturnsAsync((IEnumerable<UsuarioPerfilDto>)null!);

            var resultado =
                await _controller.BuscarUsuarioPerfill(1);

            resultado.Result.Should()
                .BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_Ok_Quando_BuscarUsuarioPerfil()
        {
            var usuarios = new List<UsuarioPerfilDto>
            {
                new()
                {
                    UsuarioId = "user1",
                    PerfilId = 1
                }
            };

            _serviceMock
                .Setup(x => x.BuscarUsuariosPerfil(1))
                .ReturnsAsync(usuarios);

            var resultado =
                await _controller.BuscarUsuarioPerfill(1);

            resultado.Result.Should()
                .BeOfType<OkObjectResult>();

            _serviceMock.Verify(
                x => x.BuscarUsuariosPerfil(1),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_Vincular_Dto_Nulo()
        {
            var resultado =
                await _controller.Vincular(null);

            resultado.Should()
                .BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Deve_Vincular_Com_Sucesso()
        {
            var dto = new UsuarioPerfilDto
            {
                UsuarioId = "user1",
                PerfilId = 1
            };

            _serviceMock
                .Setup(x => x.Vincular(dto))
                .ReturnsAsync(dto);

            var resultado =
                await _controller.Vincular(dto);

            resultado.Should()
                .BeOfType<OkObjectResult>();

            _serviceMock.Verify(
                x => x.Vincular(dto),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_PerfilId_Remover_For_Invalido()
        {
            var resultado =
                await _controller.Desvincular("user1", 0);

            resultado.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_UsuarioId_Remover_For_Nulo()
        {
            var resultado =
                await _controller.Desvincular(null, 1);

            resultado.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Desvincular_Com_Sucesso()
        {
            var dto = new UsuarioPerfilDto
            {
                UsuarioId = "user1",
                PerfilId = 1
            };

            _serviceMock
                .Setup(x => x.Desvincular("user1", 1))
                .ReturnsAsync(dto);

            var resultado =
                await _controller.Desvincular("user1", 1);

            resultado.Should()
                .BeOfType<OkResult>();

            _serviceMock.Verify(
                x => x.Desvincular("user1", 1),
                Times.Once);
        }
    }
}