using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Triad.Authorization;

namespace Triad.Controllers
{
    [Route("api/[controller]")]
    public class CardapioController : ControllerBase
    {
        private readonly IcardapioService _cardapioService;

        public CardapioController(IcardapioService cardapioService)
        {
            _cardapioService = cardapioService;
        }

        //buscar cardapio por Id
        [HttpGet("{id}", Name = "GetCardapioPorId")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(CardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CardapioDto>> BuscarPorId(int id)
        {
            if (id <= 0)
                return BadRequest("Cardapio inválido");

            var cardapio = await _cardapioService.BuscarPorId(id);

            if (cardapio == null)
                return NotFound("Cardapio não encontrado");

            return Ok(cardapio);
        }

        //buscar cardapio por nome
        [HttpGet("nome/{nome}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(CardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CardapioDto>> BuscarPorNome(string nome)
        {
            var cardapio = await _cardapioService.BuscarPorNome(nome);

            if (cardapio == null)
                return NotFound("Cardapio não encontrado");

            return Ok(cardapio);
        }

        //buscar todos os cardapio
        [HttpGet]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CardapioDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<CardapioDto>>> BuscarTodos()
        {
            var cardapios = await _cardapioService.BuscarTodos();

            return Ok(cardapios);
        }

        //adicionar
        [HttpPost]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA")]
        [ProducesResponseType(200, Type = typeof(CardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Adicionar([FromBody] CardapioDto cardapioDto)
        {
            if (cardapioDto == null)
                return BadRequest();

            await _cardapioService.Adicionar(cardapioDto);

            return Ok(cardapioDto);
        }

        //adicionar itemCardapio
        [HttpPost("{id}/itens")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA")]
        [ProducesResponseType(200, Type = typeof(CardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AdicionarItemCardapio(
            int id,
            [FromBody] ItemCardapioDto itemCardapioDto)
        {
            if (itemCardapioDto == null)
                return BadRequest();

            var cardapio = await _cardapioService.BuscarPorId(id);

            if (cardapio == null)
                return NotFound("Cardapio não encontrado");

            await _cardapioService.AdicionarItem(id, itemCardapioDto);

            return Ok(itemCardapioDto);
        }

        //atualizar
        [HttpPut("{id}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA")]
        [ProducesResponseType(200, Type = typeof(CardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Atualizar(
            int id,
            [FromBody] CardapioDto cardapioDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            if (cardapioDto == null)
                return BadRequest();

            await _cardapioService.Atualizar(id, cardapioDto);

            return Ok(cardapioDto);
        }

        //Remover cardapio
        [HttpDelete("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(CardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Remover(int id)
        {
            if (id <= 0)
                return BadRequest("Codigo invalido");

            await _cardapioService.Remover(id);

            return Ok();
        }

        //Remover Item do cardapio
        [HttpDelete("{cardapioId}/itens/{itemCardapioId}")]
        [Authorize]
        [Permissao("GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(CardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> RemoverItem(
            int cardapioId,
            int itemCardapioId)
        {
            if (cardapioId <= 0 || itemCardapioId <= 0)
                return BadRequest("Id inválido");

            var resultado = await _cardapioService.RemoverItem(
                cardapioId,
                itemCardapioId);

            return Ok(resultado);
        }
    }
}