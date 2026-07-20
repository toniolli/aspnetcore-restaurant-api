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
    public class PerfilServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IperfilRepository> _repositoryMock;

        private readonly PerfilService _service;

        public PerfilServiceTests()
        {
            _mapperMock = new Mock<IMapper>();
            _repositoryMock = new Mock<IperfilRepository>();

            _service = new PerfilService(
                _mapperMock.Object,
                _repositoryMock.Object);
        }

        [Fact]
        public async Task Deve_Adicionar_Perfil()
        {
            var dto = new PerfilDto
            {
                Nome = "Garçom",
                Descricao = "Perfil Garçom"
            };

            var perfil = new Perfil(
                dto.Nome,
                dto.Descricao);

            _mapperMock
                .Setup(x => x.Map<Perfil>(dto))
                .Returns(perfil);

            _repositoryMock
                .Setup(x => x.Adicionar(perfil))
                .ReturnsAsync(perfil);

            _mapperMock
                .Setup(x => x.Map<PerfilDto>(perfil))
                .Returns(dto);

            var resultado =
                await _service.Adicionar(dto);

            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be("Garçom");
        }

        [Fact]
        public async Task Deve_Buscar_Perfil_Por_Id()
        {
            var perfil = new Perfil(
                "Garçom",
                "Descrição");

            var dto = new PerfilDto
            {
                Nome = "Garçom",
                Descricao = "Descrição"
            };

            _repositoryMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync(perfil);

            _mapperMock
                .Setup(x => x.Map<PerfilDto>(perfil))
                .Returns(dto);

            var resultado =
                await _service.BuscarPorId(1);

            resultado.Should().NotBeNull();
            resultado!.Nome.Should().Be("Garçom");
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Perfil_Nao_Existir()
        {
            _repositoryMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync((Perfil?)null);

            _mapperMock
                .Setup(x => x.Map<PerfilDto>(null))
                .Returns((PerfilDto?)null);

            var resultado =
                await _service.BuscarPorId(1);

            resultado.Should().BeNull();
        }

        [Fact]
        public async Task Deve_Buscar_Todos_Perfis()
        {
            var perfis = new List<Perfil>
            {
                new Perfil("Garçom", "Descrição"),
                new Perfil("Gerente", "Descrição")
            };

            var dto = new List<PerfilDto>
            {
                new()
                {
                    Nome = "Garçom",
                    Descricao = "Descrição"
                },
                new()
                {
                    Nome = "Gerente",
                    Descricao = "Descrição"
                }
            };

            _repositoryMock
                .Setup(x => x.BuscarTodos())
                .ReturnsAsync(perfis);

            _mapperMock
                .Setup(x => x.Map<IEnumerable<PerfilDto>>(perfis))
                .Returns(dto);

            var resultado =
                await _service.BuscarTodos();

            resultado.Should().HaveCount(2);
        }

        [Fact]
        public async Task Deve_Atualizar_Perfil()
        {
            var perfil = new Perfil(
                "Garçom",
                "Descrição antiga");

            var dto = new PerfilDto
            {
                Nome = "Garçom Líder",
                Descricao = "Nova descrição"
            };

            _repositoryMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync(perfil);

            _repositoryMock
                .Setup(x => x.Atualizar(perfil))
                .ReturnsAsync(perfil);

            _mapperMock
                .Setup(x => x.Map<PerfilDto>(perfil))
                .Returns(dto);

            var resultado =
                await _service.Atualizar(1, dto);

            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be("Garçom Líder");
        }

        [Fact]
        public async Task Deve_Lancar_Excecao_Quando_Atualizar_Perfil_Inexistente()
        {
            var dto = new PerfilDto
            {
                Nome = "Gerente"
            };

            _repositoryMock
                .Setup(x => x.BuscarPorId(1))
                .ReturnsAsync((Perfil?)null);

            var action =
                () => _service.Atualizar(1, dto);

            await action.Should()
                .ThrowAsync<DomainExceptionValidation>()
                .WithMessage("Perfil não encontrado.");
        }

        [Fact]
        public async Task Deve_Remover_Perfil()
        {
            _repositoryMock
                .Setup(x => x.Remover(1))
                .Returns(Task.CompletedTask);

            var action = () => _service.Remover(1);

            await action.Should()
                .NotThrowAsync();

            _repositoryMock.Verify(
                x => x.Remover(1),
                Times.Once);
        }
    }
}