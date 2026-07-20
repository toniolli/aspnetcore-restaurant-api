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
    public class PermissaoController : ControllerBase
    {
        private readonly IPermissaoService _permissaoService;

        public PermissaoController(IPermissaoService permissaoService)
        {
            _permissaoService = permissaoService;
        }

        [HttpGet("{id}", Name = "GetPermissaolPorId")]
        [Authorize]
        [Permissao("GERENCIAR_PERMISSOES")]
        [ProducesResponseType(200, Type = typeof(PermissaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PermissaoDto>> BuscarPorId(int id)
        {
            if (id <= 0)
                return BadRequest("permissao inválido");

            var permissao = await _permissaoService.BuscarPorId(id);

            if (permissao == null)
                return NotFound("Permissao não encontrado");

            return Ok(permissao);
        }

        [HttpGet]
        [Authorize]
        [Permissao("GERENCIAR_PERMISSOES")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PermissaoDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<PermissaoDto>>> BuscarTodos()
        {
            var perfil = await _permissaoService.BuscarTodos();

            if (perfil == null)
                return NotFound("Nenhum permissao encontrado");

            return Ok(perfil);
        }

        [HttpPost]
        [Authorize]
        [Permissao("GERENCIAR_PERMISSOES")]
        [ProducesResponseType(200, Type = typeof(PermissaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Adicionar([FromBody] PermissaoDto permissaoDto)
        {
            if (permissaoDto == null)
                return BadRequest();

            await _permissaoService.Adicionar(permissaoDto);

            return Ok(permissaoDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_PERMISSOES")]
        [ProducesResponseType(200, Type = typeof(PermissaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Atualizar(int id, [FromBody] PermissaoDto permissaoDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            if (permissaoDto == null)
                return BadRequest();

            await _permissaoService.Atualizar(id, permissaoDto);

            return Ok(permissaoDto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_PERMISSOES")]
        [ProducesResponseType(200, Type = typeof(PermissaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Remover(int id)
        {
            if (id <= 0)
                return BadRequest("Código inválido");

            await _permissaoService.Remover(id);

            return Ok();
        }
    }
}