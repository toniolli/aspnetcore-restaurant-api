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
    public class PerfilPermissaoController : ControllerBase
    {
        private readonly IperfilPermissaoService _perfilPermissaoService;

        public PerfilPermissaoController(IperfilPermissaoService perfilPermissaoService)
        {
            _perfilPermissaoService = perfilPermissaoService;
        }

        [HttpGet("permissao/{permissaoId}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PERFIS",
            "GERENCIAR_PERMISSOES")]
        [ProducesResponseType(200, Type = typeof(PerfilPermissaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PerfilPermissaoDto>> BuscarPermissaoPerfil(int permissaoId)
        {
            if (permissaoId <= 0)
                return BadRequest("Perfil Permissao inválida");

            var perfilPermissao = await _perfilPermissaoService.BuscarPermissoesPerfil(permissaoId);

            if (perfilPermissao == null)
                return NotFound("Perfil Permissao  não encontrado");

            return Ok(perfilPermissao);
        }

        [HttpGet("perfil/{perfilId}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PERFIS",
            "GERENCIAR_PERMISSOES")]
        [ProducesResponseType(200, Type = typeof(PerfilPermissaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PerfilPermissaoDto>> BuscarPerfilPermissao(int perfilId)
        {
            if (perfilId <= 0)
                return BadRequest("Perfil Permissao inválida");

            var perfilPermissao = await _perfilPermissaoService.BuscarPerfisPermissao(perfilId);

            if (perfilPermissao == null)
                return NotFound("Perfil Permissao  não encontrado");

            return Ok(perfilPermissao);
        }

        [HttpPost]
        [Authorize]
        [Permissao(
            "GERENCIAR_PERFIS",
            "GERENCIAR_PERMISSOES")]
        [ProducesResponseType(200, Type = typeof(PerfilPermissaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Vincular([FromBody] PerfilPermissaoDto perfilPermissaoDto)
        {
            if (perfilPermissaoDto == null)
                return BadRequest();

            await _perfilPermissaoService.Vincular(perfilPermissaoDto);

            return Ok(perfilPermissaoDto);
        }

        [HttpDelete("{perfilId}/{permissaoId}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PERFIS",
            "GERENCIAR_PERMISSOES")]
        [ProducesResponseType(200, Type = typeof(PerfilPermissaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Desvincular(int perfilId, int permissaoId)
        {
            if (perfilId <= 0 && permissaoId <= 0)
                return BadRequest("Código inválido");

            await _perfilPermissaoService.Desvincular(perfilId, permissaoId);

            return Ok();
        }
    }
}