using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Triad.Authorization;

namespace Triad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
        private readonly IcomandaService _comandaService;

        public ComandaController(IcomandaService comandaService)
        {
            _comandaService = comandaService;
        }

        //buscar comanda por Id
        [HttpGet("{id}", Name = "GetComandaPorId")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_MESAS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(ComandaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ComandaDto>> BuscarPorId(int id)
        {
            if (id <= 0)
                return BadRequest("Comanda inválida");

            var comanda = await _comandaService.BuscarPorId(id);

            if (comanda == null)
                return NotFound("Comanda não encontrada");

            return Ok(comanda);
        }

        //buscar comandas aberta por mesa
        [HttpGet("mesa/{mesaId}", Name = "GetComandaAbertaPorMesa")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_MESAS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(ComandaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ComandaDto>> BuscarComandaAbertaPorMesa(int mesaId)
        {
            if (mesaId <= 0)
                return BadRequest("Comanda aberta por mesa inválida");

            var comanda = await _comandaService.BuscarComandaAbertaPorMesa(mesaId);

            if (comanda == null)
                return NotFound("Comanda aberta por mesa não encontrada");

            return Ok(comanda);
        }

        //buscar todas as comandas
        [HttpGet]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_MESAS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ComandaDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ComandaDto>>> BuscarTodos()
        {
            var comandas = await _comandaService.BuscarTodos();

            if (comandas == null)
                return NotFound("Nenhuma comanda encontrada");

            return Ok(comandas);
        }

        //buscar comandas fechadas
        [HttpGet("fechadas")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_MESAS")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ComandaDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<ComandaDto>>> BuscarComandasFechadas()
        {
            var comandas = await _comandaService.BuscarComandasFechadas();

            if (comandas == null)
                return NotFound("Nenhuma comanda fechada encontrada");

            return Ok(comandas);
        }

        //Adicionar
        [HttpPost]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_MESAS")]
        [ProducesResponseType(200, Type = typeof(ComandaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Adicionar([FromBody] ComandaDto comandaDto)
        {
            if (comandaDto == null)
                return BadRequest();

            await _comandaService.Adicionar(comandaDto);

            return Ok(comandaDto);
        }

        //fechar comanda
        [HttpPost("{id}/fechar")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_MESAS")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> FecharComanda(int id)
        {
            if (id <= 0)
                return BadRequest("Comanda não encontrada");

            var comanda = await _comandaService.BuscarPorId(id);

            if (comanda == null)
                return NotFound();

            await _comandaService.FecharComanda(id);

            return Ok("Comanda fechada com sucesso");
        }

        //Atualizar comanda quando cliente quiser trocar de mesa
        [HttpPut("{id}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_MESAS")]
        [ProducesResponseType(200, Type = typeof(ComandaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Atualizar(int id, [FromBody] ComandaDto comandaDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            if (comandaDto == null)
                return BadRequest();

            await _comandaService.Atualizar(id, comandaDto);

            return Ok(comandaDto);
        }

        //Remover
        [HttpDelete("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(ComandaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Remover(int id)
        {
            if (id <= 0)
                return BadRequest("Codigo invalido");

            await _comandaService.Remover(id);

            return Ok();
        }
    }
}