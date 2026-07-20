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
    public class PermissaoServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IpermissaoRepository> _repositoryMock;

        private readonly PermissaoService _service;

        public PermissaoServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IpermissaoRepository>();

            _service = new PermissaoService(
                _mapperMock.Object,
                _repositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Adicionar_Permissao()
        {
            var dto = new PermissaoDto
            {
                Nome = "Pedido.Criar",
                Descricao = "Permite criar pedidos"
            };

            var permissao = new Permissao(
                dto.Nome,
                dto.Descricao);

            _mapperMock
                .Setup(x => x.Map<Permissao>(dto))
                .Returns(permissao);

            _repositoryMock
                .Setup(x => x.Adicionar(permissao))
                .ReturnsAsync(permissao);

            _mapperMock
                .Setup(x => x.Map<PermissaoDto>(permissao))
                .Returns(dto);

            var resultado =
                await _service.Adicionar(dto);

            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be("Pedido.Criar");
        }

        [Fact]
        public async Task Deve_Buscar_Permissao_Por_Id()
        {
            var permissao = new Permissao(
                "Pedido.Criar",
                "Descrição");

            var dto = new PermissaoDto
            {
                Nome = "Pedido.Criar",
                Descricao = "Descrição"
            };

            _repositoryMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync(permissao);

            _mapperMock
                .Setup(x => x.Map<PermissaoDto>(permissao))
                .Returns(dto);

            var resultado =
                await _service.BuscarPorId(1);

            resultado.Should().NotBeNull();
            resultado!.Nome.Should().Be("Pedido.Criar");
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Permissao_Nao_Existir()
        {
            _repositoryMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync((Permissao?)null);

            _mapperMock
                .Setup(x => x.Map<PermissaoDto>(null))
                .Returns((PermissaoDto?)null);

            var resultado =
                await _service.BuscarPorId(1);

            resultado.Should().BeNull();
        }

        [Fact]
        public async Task Deve_Buscar_Todas_Permissoes()
        {
            var permissoes = new List<Permissao>
            {
                new("Pedido.Criar", "Descrição"),
                new("Pedido.Cancelar", "Descrição")
            };

            var dto = new List<PermissaoDto>
            {
                new()
                {
                    Nome = "Pedido.Criar",
                    Descricao = "Descrição"
                },
                new()
                {
                    Nome = "Pedido.Cancelar",
                    Descricao = "Descrição"
                }
            };

            _repositoryMock
                .Setup(x => x.BuscarTodos())
                .ReturnsAsync(permissoes);

            _mapperMock
                .Setup(x => x.Map<IEnumerable<PermissaoDto>>(permissoes))
                .Returns(dto);

            var resultado =
                await _service.BuscarTodos();

            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task Deve_Atualizar_Permissao()
        {
            var permissao = new Permissao(
                "Pedido.Criar",
                "Descrição antiga");

            var dto = new PermissaoDto
            {
                Nome = "Pedido.Cancelar",
                Descricao = "Nova descrição"
            };

            _repositoryMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync(permissao);

            _mapperMock
                .Setup(x => x.Map<PermissaoDto>(permissao))
                .Returns(dto);

            var resultado =
                await _service.Atualizar(1, dto);

            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be("Pedido.Cancelar");
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Atualizar_Permissao_Inexistente()
        {
            var dto = new PermissaoDto
            {
                Nome = "Pedido.Cancelar"
            };

            _repositoryMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync((Permissao?)null);

            var action =
                () => _service.Atualizar(1, dto);

            await action.Should()
                .ThrowAsync<DomainExceptionValidation>()
                .WithMessage("permissao não encontrada.");
        }

        [Fact]
        public async Task Deve_Remover_Permissao()
        {
            _repositoryMock
                .Setup(x => x.Remover(1))
                .Returns(Task.CompletedTask);
            var action =
                () => _service.Remover(1);
            await action.Should()
                .NotThrowAsync();

            _repositoryMock.Verify(
                x => x.Remover(1),
                Times.Once);
        }
    }
}