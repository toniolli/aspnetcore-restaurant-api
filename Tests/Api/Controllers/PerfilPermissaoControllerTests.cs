using Application.Dto;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Triad.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class PerfilPermissaoControllerTests
    {
        private readonly Mock<IperfilPermissaoService> _serviceMock;
        private readonly PerfilPermissaoController _controller;

        public PerfilPermissaoControllerTests()
        {
            _serviceMock = new Mock<IperfilPermissaoService>();

            _controller = new PerfilPermissaoController(
                _serviceMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_PermissaoId_For_Invalido()
        {
            var resultado =
                await _controller.BuscarPermissaoPerfil(0);

            resultado.Result.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Nao_Existir_PermissaoPerfil()
        {
            _serviceMock
                .Setup(x => x.BuscarPermissoesPerfil(1))
                .ReturnsAsync((IEnumerable<PermissaoDto>)null!);

            var resultado =
                await _controller.BuscarPermissaoPerfil(1);

            resultado.Result.Should()
                .BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_Ok_Quando_BuscarPermissaoPerfil()
        {
            var permissoes = new List<PermissaoDto>
            {
                new()
                {
                    Nome = "Consultar",
                    Descricao = "Permissão de consulta",
                    Ativo = true
                }
            };

            _serviceMock
                .Setup(x => x.BuscarPermissoesPerfil(1))
                .ReturnsAsync(permissoes);

            var resultado =
                await _controller.BuscarPermissaoPerfil(1);

            resultado.Result.Should()
                .BeOfType<OkObjectResult>();

            _serviceMock.Verify(
                x => x.BuscarPermissoesPerfil(1),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_PerfilId_For_Invalido()
        {
            var resultado =
                await _controller.BuscarPerfilPermissao(0);

            resultado.Result.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Nao_Existir_PerfilPermissao()
        {
            _serviceMock
                .Setup(x => x.BuscarPerfisPermissao(1))
                .ReturnsAsync((IEnumerable<PerfilDto>)null!);

            var resultado =
                await _controller.BuscarPerfilPermissao(1);

            resultado.Result.Should()
                .BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_Ok_Quando_BuscarPerfilPermissao()
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
                .Setup(x => x.BuscarPerfisPermissao(1))
                .ReturnsAsync(perfis);

            var resultado =
                await _controller.BuscarPerfilPermissao(1);

            resultado.Result.Should()
                .BeOfType<OkObjectResult>();

            _serviceMock.Verify(
                x => x.BuscarPerfisPermissao(1),
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
            var dto = new PerfilPermissaoDto
            {
                PerfilId = 1,
                PermissaoId = 2
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
        public async Task Deve_Retornar_BadRequest_Quando_Ids_Forem_Invalidos()
        {
            var resultado =
                await _controller.Desvincular(0, 0);

            resultado.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Desvincular_Com_Sucesso()
        {
            var dto = new PerfilPermissaoDto
            {
                PerfilId = 1,
                PermissaoId = 2
            };

            _serviceMock
                .Setup(x => x.Desvincular(1, 2))
                .ReturnsAsync(dto);

            var resultado =
                await _controller.Desvincular(1, 2);

            resultado.Should()
                .BeOfType<OkResult>();

            _serviceMock.Verify(
                x => x.Desvincular(1, 2),
                Times.Once);
        }
    }
}