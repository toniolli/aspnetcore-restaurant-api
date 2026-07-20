using Application.Dto;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Domain.Validation;
using FluentAssertions;
using Infra.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Infra.Identity
{
    public class UsuarioServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly Mock<IConfiguration> _configurationMock;

        private readonly UsuarioService _usuarioService;

        public UsuarioServiceTests()
        {
            _userManagerMock = CreateUserManagerMock();

            _signInManagerMock =
                CreateSignInManagerMock(_userManagerMock);

            _configurationMock =
                new Mock<IConfiguration>();

            var jwtSection =
                new Mock<IConfigurationSection>();

            jwtSection.Setup(x => x["Key"])
                .Returns("MINHA_CHAVE_SUPER_SECRETA_JWT_1234567890_ABCDEF");

            jwtSection.Setup(x => x["Issuer"])
                .Returns("Triad");

            jwtSection.Setup(x => x["Audience"])
                .Returns("Triad");

            _configurationMock
                .Setup(x => x.GetSection("Jwt"))
                .Returns(jwtSection.Object);

            _usuarioService = new UsuarioService(
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _configurationMock.Object);
        }

        private Mock<UserManager<ApplicationUser>> CreateUserManagerMock()
        {
            var store =
                new Mock<IUserStore<ApplicationUser>>();

            return new Mock<UserManager<ApplicationUser>>(
                store.Object,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);
        }

        private Mock<SignInManager<ApplicationUser>> CreateSignInManagerMock(
            Mock<UserManager<ApplicationUser>> userManagerMock)
        {
            var contextAccessor =
                new Mock<IHttpContextAccessor>();

            var claimsFactory =
                new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();

            return new Mock<SignInManager<ApplicationUser>>(
                userManagerMock.Object,
                contextAccessor.Object,
                claimsFactory.Object,
                null,
                null,
                null,
                null);
        }




        [Fact]
        public async Task Deve_Criar_Usuario()
        {
            var dto = new CreateUserDto
            {
                Username = "Lucas",
                Email = "lucas@email.com",
                Password = "Senha@123",
                PasswordConfirmation = "Senha@123"
            };

            _userManagerMock
                .Setup(x => x.CreateAsync(
                    It.IsAny<ApplicationUser>(),
                    dto.Password))
                .ReturnsAsync(IdentityResult.Success);

            var resultado =
                await _usuarioService.CriarUsuario(dto);

            resultado.Should().NotBeNull();
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Senhas_Diferentes()
        {
            var dto = new CreateUserDto
            {
                Username = "Lucas",
                Email = "lucas@email.com",
                Password = "123",
                PasswordConfirmation = "456"
            };

            var action =
                () => _usuarioService.CriarUsuario(dto);

            await action.Should()
                .ThrowAsync<Exception>()
                .WithMessage("As senhas não são iguais");
        }


        [Fact]
        public async Task Deve_Retornar_Token_Quando_Login_Valido()
        {
            var usuario = new ApplicationUser
            {
                Id = "1",
                UserName = "admin",
                Email = "admin@email.com"
            };

            var dto = new LoginDto
            {
                UserName = "admin",
                Password = "Senha@123"
            };

            _userManagerMock
                .Setup(x => x.FindByNameAsync(dto.UserName))
                .ReturnsAsync(usuario);

            _signInManagerMock
                .Setup(x => x.CheckPasswordSignInAsync(
                    usuario,
                    dto.Password,
                    false))
                .ReturnsAsync(SignInResult.Success);

            _userManagerMock
                .Setup(x => x.GetRolesAsync(usuario))
                .ReturnsAsync(new List<string> { "User" });

            var token =
                await _usuarioService.Login(dto);

            token.Should().NotBeNullOrEmpty();
        }


        [Fact]
        public async Task Deve_Retornar_Null_Quando_Usuario_Nao_Encontrado()
        {
            _userManagerMock
                .Setup(x => x.FindByNameAsync(
                    It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            var resultado =
                await _usuarioService.Login(
                    new LoginDto());

            resultado.Should().BeNull();
        }



        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Usuario_Nao_Encontrado()
        {
            _userManagerMock
                .Setup(x => x.FindByIdAsync(
                    It.IsAny<string>()))
                .ReturnsAsync((ApplicationUser)null);

            var dto = new ChangePasswordDto
            {
                PasswordAtual = "123",
                NovaSenha = "456",
                ConfirmacaoNovaSenha = "456"
            };

            var action =
                () => _usuarioService.ChangePassword(
                    "1",
                    dto,
                    false);

            await action.Should()
                .ThrowAsync<Exception>()
                .WithMessage("Usuário não encontrado");
        }


        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Confirmacao_For_Diferente()
        {
            var usuario = new ApplicationUser
            {
                Id = "1"
            };

            _userManagerMock
                .Setup(x => x.FindByIdAsync("1"))
                .ReturnsAsync(usuario);

            var dto = new ChangePasswordDto
            {
                PasswordAtual = "123",
                NovaSenha = "456",
                ConfirmacaoNovaSenha = "789"
            };

            var action =
                () => _usuarioService.ChangePassword(
                    "1",
                    dto,
                    false);

            await action.Should()
                .ThrowAsync<Exception>()
                .WithMessage("Senha e confirmação não conferem");
        }


        [Fact]
        public async Task Deve_Alterar_Senha()
        {
            var usuario = new ApplicationUser
            {
                Id = "1"
            };

            var dto = new ChangePasswordDto
            {
                PasswordAtual = "SenhaAntiga",
                NovaSenha = "SenhaNova@123",
                ConfirmacaoNovaSenha = "SenhaNova@123"
            };

            _userManagerMock
                .Setup(x => x.FindByIdAsync("1"))
                .ReturnsAsync(usuario);

            _userManagerMock
                .Setup(x => x.ChangePasswordAsync(
                    usuario,
                    dto.PasswordAtual,
                    dto.NovaSenha))
                .ReturnsAsync(IdentityResult.Success);

            var resultado =
                await _usuarioService.ChangePassword(
                    "1",
                    dto,
                    false);

            resultado.Should()
                .Be("Senha alterada com sucesso");
        }


        [Fact]
        public async Task Deve_Excluir_Usuario()
        {
            var usuario = new ApplicationUser
            {
                Id = "1",
                UserName = "Lucas",
                Email = "lucas@email.com"
            };

            _userManagerMock
                .Setup(x => x.FindByIdAsync("1"))
                .ReturnsAsync(usuario);

            _userManagerMock
                .Setup(x => x.DeleteAsync(usuario))
                .ReturnsAsync(IdentityResult.Success);

            var resultado =
                await _usuarioService.Excluir("1");

            resultado.Should().NotBeNull();
            resultado!.Username.Should().Be("Lucas");
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Usuario_Nao_Existir()
        {
            _userManagerMock
                .Setup(x => x.FindByIdAsync("1"))
                .ReturnsAsync((ApplicationUser)null);

            var resultado =
                await _usuarioService.Excluir("1");

            resultado.Should().BeNull();
        }


    }
}
