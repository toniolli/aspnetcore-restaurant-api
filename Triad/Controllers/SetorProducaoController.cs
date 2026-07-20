using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Triad.Authorization;

namespace Triad.Controllers
{
    [Route("/[controller]")]
    public class SetorProducaoController : ControllerBase
    {
        private readonly IsetorProducaoService _setorProducaoService;

        public SetorProducaoController(IsetorProducaoService setorProducaoService)
        {
            _setorProducaoService = setorProducaoService;
        }

        //buscar Setor por Id
        [HttpGet("{id}", Name = "GetSetorProducaoPorId")]
        [Authorize]
        [Permissao(
            "ACESSAR_COZINHA",
            "ACESSAR_BAR",
            "GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(SetorProducaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SetorProducaoDto>> BuscarPorId(int id)
        {
            if (id <= 0)
                return BadRequest("Setor de produção inválido");

            var item = await _setorProducaoService.BuscarPorId(id);

            if (item == null)
                return NotFound("Setor de produção não encontrado");

            return Ok(item);
        }

        //buscar setor por nome
        [HttpGet("nome/{nome}")]
        [Authorize]
        [Permissao(
            "ACESSAR_COZINHA",
            "ACESSAR_BAR",
            "GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(SetorProducaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SetorProducaoDto>> BuscarPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest("Nome inválido");

            var item = await _setorProducaoService.BuscarPorNome(nome);

            if (item == null)
                return NotFound("Setor de produção não encontrado");

            return Ok(item);
        }

        //buscar setores ativos
        [HttpGet("ativos")]
        [Authorize]
        [Permissao(
            "ACESSAR_COZINHA",
            "ACESSAR_BAR",
            "GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SetorProducaoDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<SetorProducaoDto>>> BuscarAtivos()
        {
            var itens = await _setorProducaoService.BuscarAtivos();

            if (itens == null)
                return NotFound("Setores de produção não encontrados");

            return Ok(itens);
        }

        //buscar todos os setores
        [HttpGet]
        [Authorize]
        [Permissao(
            "ACESSAR_COZINHA",
            "ACESSAR_BAR",
            "GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SetorProducaoDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<SetorProducaoDto>>> BuscarTodos()
        {
            var itens = await _setorProducaoService.BuscarTodos();

            if (itens == null)
                return NotFound("Setores de produção não encontrados");

            return Ok(itens);
        }

        //adicionar
        [HttpPost]
        [Authorize]
        [Permissao(
            "ACESSAR_COZINHA",
            "ACESSAR_BAR",
            "GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(SetorProducaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Adicionar(
            [FromBody] SetorProducaoDto setorProducaoDto)
        {
            if (setorProducaoDto == null)
                return BadRequest();

            await _setorProducaoService.Adicionar(setorProducaoDto);

            return Ok(setorProducaoDto);
        }

        //atualizar
        [HttpPut("{id}")]
        [Authorize]
        [Permissao(
            "ACESSAR_COZINHA",
            "ACESSAR_BAR",
            "GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(SetorProducaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Atualizar(
            int id,
            [FromBody] SetorProducaoDto setorProducaoDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            if (setorProducaoDto == null)
                return BadRequest();

            await _setorProducaoService.Atualizar(id, setorProducaoDto);

            return Ok(setorProducaoDto);
        }

        //remover setor
        [HttpDelete("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(SetorProducaoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Remover(int id)
        {
            if (id <= 0)
                return BadRequest("Código inválido");

            await _setorProducaoService.Remover(id);

            return Ok();
        }
    }
}