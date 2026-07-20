using Application.Dto;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Triad.Authorization;

namespace Triad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioPerfilController : ControllerBase
    {
        private readonly IUsuarioPerfilService _usuarioPerfilService;

        public UsuarioPerfilController(IUsuarioPerfilService usuarioPerfilService)
        {
            _usuarioPerfilService = usuarioPerfilService;
        }

        [HttpGet("usuario/{usuarioId}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_USUARIOS",
            "GERENCIAR_PERFIS")]
        [ProducesResponseType(200, Type = typeof(UsuarioPerfilDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UsuarioPerfilDto>> BuscarPerfilUsuario(string perfilUsuarioId)
        {
            if (perfilUsuarioId == null)
                return BadRequest("Usuario perfil inválida");

            var perfilPermissao = await _usuarioPerfilService.BuscarPerfisUsuario(perfilUsuarioId);

            if (perfilPermissao == null)
                return NotFound("Usuario perfil  não encontrado");

            return Ok(perfilPermissao);
        }

        [HttpGet("perfil/{perfilId}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_USUARIOS",
            "GERENCIAR_PERFIS")]
        [ProducesResponseType(200, Type = typeof(UsuarioPerfilDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UsuarioPerfilDto>> BuscarUsuarioPerfill(int usuarioPerfilId)
        {
            if (usuarioPerfilId <= 0)
                return BadRequest("Usuario perfil  inválida");

            var perfilPermissao = await _usuarioPerfilService.BuscarUsuariosPerfil(usuarioPerfilId);

            if (perfilPermissao == null)
                return NotFound("Usuario perfil   não encontrado");

            return Ok(perfilPermissao);
        }

        [HttpPost]
        [Authorize]
        [Permissao(
            "GERENCIAR_USUARIOS",
            "GERENCIAR_PERFIS")]
        [ProducesResponseType(200, Type = typeof(UsuarioPerfilDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Vincular([FromBody] UsuarioPerfilDto usuarioPerfilDto)
        {
            if (usuarioPerfilDto == null)
                return BadRequest();

            await _usuarioPerfilService.Vincular(usuarioPerfilDto);

            return Ok(usuarioPerfilDto);
        }

        [HttpDelete("{usuarioId}/{perfilId}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_USUARIOS",
            "GERENCIAR_PERFIS")]
        [ProducesResponseType(200, Type = typeof(UsuarioPerfilDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Desvincular(string usuarioId, int perfilID)
        {
            if (perfilID <= 0)
                return BadRequest("Código inválido");

            if (usuarioId == null)
                return BadRequest("Código inválido");

            await _usuarioPerfilService.Desvincular(usuarioId, perfilID);

            return Ok();
        }
    }
}