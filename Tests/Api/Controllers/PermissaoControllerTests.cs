using Application.Dto;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Triad.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class PermissaoControllerTests
    {
        private readonly Mock<IPermissaoService> _permissaoServiceMock;
        private readonly PermissaoController _controller;

        public PermissaoControllerTests()
        {
            _permissaoServiceMock = new Mock<IPermissaoService>();

            _controller = new PermissaoController(
                _permissaoServiceMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_Id_For_Invalido()
        {
            var resultado = await _controller.BuscarPorId(0);

            resultado.Result.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Permissao_Nao_Existir()
        {
            _permissaoServiceMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync((PermissaoDto?)null);

            var resultado = await _controller.BuscarPorId(1);

            resultado.Result.Should()
                .BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_Ok_Quando_BuscarPorId()
        {
            var permissao = new PermissaoDto
            {
                Nome = "Consultar",
                Descricao = "Permissão de consulta",
                Ativo = true
            };

            _permissaoServiceMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync(permissao);

            var resultado = await _controller.BuscarPorId(1);

            resultado.Result.Should()
                .BeOfType<OkObjectResult>();

            var okResult = resultado.Result as OkObjectResult;

            okResult!.Value.Should()
                .BeEquivalentTo(permissao);
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Nao_Existirem_Permissoes()
        {
            _permissaoServiceMock
                .Setup(x => x.BuscarTodos())
                .ReturnsAsync((IEnumerable<PermissaoDto>)null!);

            var resultado = await _controller.BuscarTodos();

            resultado.Result.Should()
                .BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_Ok_Quando_BuscarTodos()
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

            _permissaoServiceMock
                .Setup(x => x.BuscarTodos())
                .ReturnsAsync(permissoes);

            var resultado = await _controller.BuscarTodos();

            resultado.Result.Should()
                .BeOfType<OkObjectResult>();

            _permissaoServiceMock.Verify(
                x => x.BuscarTodos(),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_Adicionar_Dto_Nulo()
        {
            var resultado = await _controller.Adicionar(null);

            resultado.Should()
                .BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Deve_Adicionar_Permissao_Com_Sucesso()
        {
            var dto = new PermissaoDto
            {
                Nome = "Consultar",
                Descricao = "Permissão de consulta",
                Ativo = true
            };

            _permissaoServiceMock
                .Setup(x => x.Adicionar(dto))
                .ReturnsAsync(dto);

            var resultado = await _controller.Adicionar(dto);

            resultado.Should()
                .BeOfType<OkObjectResult>();

            _permissaoServiceMock.Verify(
                x => x.Adicionar(dto),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_Atualizar_Id_Invalido()
        {
            var dto = new PermissaoDto
            {
                Nome = "Consultar",
                Descricao = "Permissão de consulta",
                Ativo = true
            };

            var resultado = await _controller.Atualizar(0, dto);

            resultado.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_Atualizar_Dto_Nulo()
        {
            var resultado = await _controller.Atualizar(1, null);

            resultado.Should()
                .BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Deve_Atualizar_Permissao_Com_Sucesso()
        {
            var dto = new PermissaoDto
            {
                Nome = "Consultar",
                Descricao = "Permissão de consulta",
                Ativo = true
            };

            _permissaoServiceMock
                .Setup(x => x.Atualizar(1, dto))
                .ReturnsAsync(dto);

            var resultado = await _controller.Atualizar(1, dto);

            resultado.Should()
                .BeOfType<OkObjectResult>();

            _permissaoServiceMock.Verify(
                x => x.Atualizar(1, dto),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_Remover_Id_Invalido()
        {
            var resultado = await _controller.Remover(0);

            resultado.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Remover_Permissao_Com_Sucesso()
        {
            _permissaoServiceMock
                .Setup(x => x.Remover(1))
                .Returns(Task.CompletedTask);

            var resultado = await _controller.Remover(1);

            resultado.Should()
                .BeOfType<OkResult>();

            _permissaoServiceMock.Verify(
                x => x.Remover(1),
                Times.Once);
        }
    }
}