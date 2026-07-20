using Application.Dto;
using Application.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Triad.Controllers;
using Xunit;

namespace Tests.Controllers
{
    public class UsuarioControllerTests
    {
        private readonly Mock<IusuarioService> _serviceMock;
        private readonly UsuarioController _controller;

        public UsuarioControllerTests()
        {
            _serviceMock = new Mock<IusuarioService>();

            _controller = new UsuarioController(
                _serviceMock.Object);
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Nao_Existirem_Usuarios()
        {
            _serviceMock
                .Setup(x => x.BuscarTodos())
                .ReturnsAsync(new List<CreateUserDto>());

            var resultado =
                await _controller.BuscarTodos();

            resultado.Result.Should()
                .BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_Ok_Quando_BuscarTodos()
        {
            var usuarios = new List<CreateUserDto>
            {
                new()
                {
                    Username = "lucas",
                    Email = "lucas@email.com",
                    Password = "",
                    PasswordConfirmation = ""
                }
            };

            _serviceMock
                .Setup(x => x.BuscarTodos())
                .ReturnsAsync(usuarios);

            var resultado =
                await _controller.BuscarTodos();

            resultado.Result.Should()
                .BeOfType<OkObjectResult>();

            _serviceMock.Verify(
                x => x.BuscarTodos(),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_CriarUsuario_Dto_Nulo()
        {
            var resultado =
                await _controller.CriarUsuario(null);

            resultado.Should()
                .BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Deve_Criar_Usuario_Com_Sucesso()
        {
            var dto = new CreateUserDto
            {
                Username = "lucas",
                Email = "lucas@email.com",
                Password = "123456",
                PasswordConfirmation = "123456"
            };

            _serviceMock
                .Setup(x => x.CriarUsuario(dto))
                .ReturnsAsync(dto);

            var resultado =
                await _controller.CriarUsuario(dto);

            resultado.Should()
                .BeOfType<OkObjectResult>();

            _serviceMock.Verify(
                x => x.CriarUsuario(dto),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_Login_Dto_Nulo()
        {
            var resultado =
                await _controller.Login(null);

            resultado.Should()
                .BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Deve_Retornar_Unauthorized_Quando_Login_For_Invalido()
        {
            var dto = new LoginDto
            {
                UserName = "lucas",
                Password = "123456"
            };

            _serviceMock
                .Setup(x => x.Login(dto))
                .ReturnsAsync((string)null);

            var resultado =
                await _controller.Login(dto);

            resultado.Should()
                .BeOfType<UnauthorizedObjectResult>();
        }

        [Fact]
        public async Task Deve_Realizar_Login_Com_Sucesso()
        {
            var dto = new LoginDto
            {
                UserName = "lucas",
                Password = "123456"
            };

            _serviceMock
                .Setup(x => x.Login(dto))
                .ReturnsAsync("token-jwt");

            var resultado =
                await _controller.Login(dto);

            resultado.Should()
                .BeOfType<OkObjectResult>();

            _serviceMock.Verify(
                x => x.Login(dto),
                Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_ChangePassword_Dto_Nulo()
        {
            var resultado =
                await _controller.ChangePassword(null);

            resultado.Should()
                .BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task Deve_Retornar_BadRequest_Quando_Id_For_Invalido_Ao_Excluir()
        {
            var resultado =
                await _controller.Excluir("");

            resultado.Should()
                .BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Deve_Retornar_NotFound_Quando_Usuario_Nao_Existir()
        {
            _serviceMock
                .Setup(x => x.Excluir("1"))
                .ReturnsAsync((CreateUserDto?)null);

            var resultado =
                await _controller.Excluir("1");

            resultado.Should()
                .BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public async Task Deve_Excluir_Usuario_Com_Sucesso()
        {
            var usuario = new CreateUserDto
            {
                Username = "lucas",
                Email = "lucas@email.com",
                Password = "",
                PasswordConfirmation = ""
            };

            _serviceMock
                .Setup(x => x.Excluir("1"))
                .ReturnsAsync(usuario);

            var resultado =
                await _controller.Excluir("1");

            resultado.Should()
                .BeOfType<OkObjectResult>();

            _serviceMock.Verify(
                x => x.Excluir("1"),
                Times.Once);
        }
    }
}