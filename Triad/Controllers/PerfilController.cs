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
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilService _perfilService;

        public PerfilController(IPerfilService perfilService)
        {
            _perfilService = perfilService;
        }

        [HttpGet("{id}", Name = "GetPerfilPorId")]
        [Authorize]
        [Permissao("GERENCIAR_PERFIS")]
        [ProducesResponseType(200, Type = typeof(PerfilDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PerfilDto>> BuscarPorId(int id)
        {
            if (id <= 0)
                return BadRequest("perfil inválido");

            var perfil = await _perfilService.BuscarPorId(id);

            if (perfil == null)
                return NotFound("Perfil não encontrado");

            return Ok(perfil);
        }

        [HttpGet]
        [Authorize]
        [Permissao("GERENCIAR_PERFIS")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PerfilDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<PerfilDto>>> BuscarTodos()
        {
            var perfil = await _perfilService.BuscarTodos();

            if (perfil == null)
                return NotFound("Nenhum perfil encontrado");

            return Ok(perfil);
        }

        [HttpPost]
        [Authorize]
        [Permissao("GERENCIAR_PERFIS")]
        [ProducesResponseType(200, Type = typeof(PerfilDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Adicionar([FromBody] PerfilDto perfilDto)
        {
            if (perfilDto == null)
                return BadRequest();

            await _perfilService.Adicionar(perfilDto);

            return Ok(perfilDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_PERFIS")]
        [ProducesResponseType(200, Type = typeof(PerfilDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Atualizar(int id, [FromBody] PerfilDto perfilDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            if (perfilDto == null)
                return BadRequest();

            await _perfilService.Atualizar(id, perfilDto);

            return Ok(perfilDto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_PERFIS")]
        [ProducesResponseType(200, Type = typeof(PerfilDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Remover(int id)
        {
            if (id <= 0)
                return BadRequest("Código inválido");

            await _perfilService.Remover(id);

            return Ok();
        }
    }
}