using Application.Dto;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Triad.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class PerfilControllerTests
    {
        private readonly Mock<IPerfilService> _perfilServiceMock;
        private readonly PerfilController _controller;

        public PerfilControllerTests()
        {
            _perfilServiceMock = new Mock<IPerfilService>();

            _controller = new PerfilController(
                _perfilServiceMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_Id_For_Invalido()
        {
            var resultado = await _controller.BuscarPorId(0);

            resultado.Result.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Perfil_Nao_Existir()
        {
            _perfilServiceMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync((PerfilDto?)null);

            var resultado = await _controller.BuscarPorId(1);

            resultado.Result.Should()
                .BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_Ok_Quando_BuscarPorId()
        {
            var perfil = new PerfilDto
            {
                Nome = "Administrador",
                Descricao = "Perfil Administrador",
                Ativo = true
            };

            _perfilServiceMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync(perfil);

            var resultado = await _controller.BuscarPorId(1);

            resultado.Result.Should()
                .BeOfType<OkObjectResult>();

            var okResult = resultado.Result as OkObjectResult;

            okResult!.Value.Should()
                .BeEquivalentTo(perfil);
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Nao_Existirem_Perfis()
        {
            _perfilServiceMock
                .Setup(x => x.BuscarTodos())
                .ReturnsAsync((IEnumerable<PerfilDto>)null!);

            var resultado = await _controller.BuscarTodos();

            resultado.Result.Should()
                .BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_Ok_Quando_BuscarTodos()
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

            _perfilServiceMock
                .Setup(x => x.BuscarTodos())
                .ReturnsAsync(perfis);

            var resultado = await _controller.BuscarTodos();

            resultado.Result.Should()
                .BeOfType<OkObjectResult>();

            _perfilServiceMock.Verify(
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
        public async Task Deve_Adicionar_Perfil_Com_Sucesso()
        {
            var dto = new PerfilDto
            {
                Nome = "Administrador",
                Descricao = "Perfil Administrador",
                Ativo = true
            };

            _perfilServiceMock
                .Setup(x => x.Adicionar(dto))
                .ReturnsAsync(dto);

            var resultado = await _controller.Adicionar(dto);

            resultado.Should()
                .BeOfType<OkObjectResult>();

            _perfilServiceMock.Verify(
                x => x.Adicionar(dto),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_Atualizar_Id_Invalido()
        {
            var dto = new PerfilDto
            {
                Nome = "Administrador",
                Descricao = "Perfil Administrador",
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
        public async Task Deve_Atualizar_Perfil_Com_Sucesso()
        {
            var dto = new PerfilDto
            {
                Nome = "Administrador",
                Descricao = "Perfil Administrador",
                Ativo = true
            };

            _perfilServiceMock
                .Setup(x => x.Atualizar(1, dto))
                .ReturnsAsync(dto);

            var resultado = await _controller.Atualizar(1, dto);

            resultado.Should()
                .BeOfType<OkObjectResult>();

            _perfilServiceMock.Verify(
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
        public async Task Deve_Remover_Perfil_Com_Sucesso()
        {
            _perfilServiceMock
                .Setup(x => x.Remover(1))
                .Returns(Task.CompletedTask);

            var resultado = await _controller.Remover(1);

            resultado.Should()
                .BeOfType<OkResult>();

            _perfilServiceMock.Verify(
                x => x.Remover(1),
                Times.Once);
        }
    }
}