using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Triad.Authorization;

namespace Triad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IusuarioService _usuarioService;

        public UsuarioController(
            IusuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        [Authorize]
        [Permissao("GERENCIAR_USUARIOS")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CreateUserDto>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<CreateUserDto>>> BuscarTodos()
        {
            var usuarios = await _usuarioService.BuscarTodos();

            if (usuarios == null || !usuarios.Any())
                return NotFound("Nenhum usuário encontrado");

            return Ok(usuarios);
        }

        [HttpPost("registrar")]
        [Authorize]
        [Permissao("GERENCIAR_USUARIOS")]
        [ProducesResponseType(200, Type = typeof(CreateUserDto))]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CriarUsuario(
            [FromBody] CreateUserDto createUserDto)
        {
            if (createUserDto == null)
                return BadRequest();

            var usuario =
                await _usuarioService.CriarUsuario(
                    createUserDto);

            return Ok(usuario);
        }

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Login(
            [FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
                return BadRequest();

            var token =
                await _usuarioService.Login(loginDto);

            if (string.IsNullOrEmpty(token))
                return Unauthorized(
                    "Usuário ou senha inválidos");

            return Ok(token);
        }

        [Authorize]
        [Permissao("GERENCIAR_USUARIOS")]
        [HttpPost("alterar-senha")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> ChangePassword(
            [FromBody] ChangePasswordDto dto)
        {
            if (dto == null)
                return BadRequest();

            var userId =
                User.FindFirstValue(
                    ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized();

            var isAdmin =
                User.IsInRole("Admin");

            var resultado =
                await _usuarioService.ChangePassword(
                    userId,
                    dto,
                    isAdmin);

            return Ok(resultado);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_USUARIOS")]
        [ProducesResponseType(200, Type = typeof(CreateUserDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Excluir(
            string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return BadRequest("Código inválido");

            var usuario =
                await _usuarioService.Excluir(id);

            if (usuario == null)
                return NotFound(
                    "Usuário não encontrado");

            return Ok(usuario);
        }
    }
}