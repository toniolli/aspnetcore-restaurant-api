using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Triad.Authorization;

namespace Triad.Controllers
{
    [Route("api/[controller]")]
    public class ItemCardapioController : ControllerBase
    {
        private readonly IitemCardapioService _itemCardapioService;

        public ItemCardapioController(IitemCardapioService itemCardapioService)
        {
            _itemCardapioService = itemCardapioService;
        }

        //buscar item por Id
        [HttpGet("{id}", Name = "GetItemCardapioPorId")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(ItemCardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ItemCardapioDto>> BuscarPorId(int id)
        {
            if (id <= 0)
                return BadRequest("Item do cardápio inválido");

            var item = await _itemCardapioService.BuscarPorId(id);

            if (item == null)
                return NotFound("Item do cardápio não encontrado");

            return Ok(item);
        }

        //buscar item por nome
        [HttpGet("nome/{nome}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(ItemCardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ItemCardapioDto>> BuscarPorNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest("Nome inválido");

            var item = await _itemCardapioService.BuscarPorNome(nome);

            if (item == null)
                return NotFound("Item do cardápio não encontrado");

            return Ok(item);
        }

        //buscar itens disponíveis
        [HttpGet("disponiveis")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ItemCardapioDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ItemCardapioDto>>> BuscarDisponiveis()
        {
            var itens = await _itemCardapioService.BuscarDisponiveis();

            if (itens == null)
                return NotFound("Itens não encontrados");

            return Ok(itens);
        }

        //buscar por categoria
        [HttpGet("categoria/{categoria}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ItemCardapioDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ItemCardapioDto>>> BuscarPorCategoria(CategoriaEnum categoria)
        {
            var itens = await _itemCardapioService.BuscarPorCategoria(categoria);

            if (itens == null)
                return NotFound("Itens não encontrados");

            return Ok(itens);
        }

        //buscar todos os itens
        [HttpGet]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ItemCardapioDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ItemCardapioDto>>> BuscarTodos()
        {
            var itens = await _itemCardapioService.BuscarTodos();

            if (itens == null)
                return NotFound("Itens do cardápio não encontrados");

            return Ok(itens);
        }

        //atualizar
        [HttpPut("{id}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA")]
        [ProducesResponseType(200, Type = typeof(ItemCardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Atualizar(
            int id,
            [FromBody] ItemCardapioDto itemCardapioDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            if (itemCardapioDto == null)
                return BadRequest();

            await _itemCardapioService.Atualizar(id, itemCardapioDto);

            return Ok(itemCardapioDto);
        }

        //alterar nome
        [HttpPut("{id}/nome")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA")]
        [ProducesResponseType(200, Type = typeof(ItemCardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AlterarNome(
            int id,
            [FromBody] ItemCardapioDto itemCardapioDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            var item = await _itemCardapioService.AlterarNome(
                id,
                itemCardapioDto.Nome);

            if (item == null)
                return NotFound("Item do cardápio não encontrado");

            return Ok(item);
        }

        //alterar descrição
        [HttpPut("{id}/descricao")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA")]
        [ProducesResponseType(200, Type = typeof(ItemCardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AlterarDescricao(
            int id,
            [FromBody] ItemCardapioDto itemCardapioDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            var item = await _itemCardapioService.AlterarDescricao(
                id,
                itemCardapioDto.Descricao);

            if (item == null)
                return NotFound("Item do cardápio não encontrado");

            return Ok(item);
        }

        //alterar preço
        [HttpPut("{id}/preco")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA")]
        [ProducesResponseType(200, Type = typeof(ItemCardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AlterarPreco(
            int id,
            [FromBody] ItemCardapioDto itemCardapioDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            var item = await _itemCardapioService.AlterarPreco(
                id,
                itemCardapioDto.Preco);

            if (item == null)
                return NotFound("Item do cardápio não encontrado");

            return Ok(item);
        }

        //alterar categoria
        [HttpPut("{id}/categoria")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA")]
        [ProducesResponseType(200, Type = typeof(ItemCardapioDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AlterarCategoria(
            int id,
            [FromBody] ItemCardapioDto itemCardapioDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            var item = await _itemCardapioService.AlterarCategoria(
                id,
                itemCardapioDto.Categoria);

            if (item == null)
                return NotFound("Item do cardápio não encontrado");

            return Ok(item);
        }
    }
}