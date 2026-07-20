using Application.Dto;
using Application.Interfaces;
using Domain.Enum;
using Microsoft.AspNetCore.Mvc;

namespace Triad.Controllers
{
    [Route("api/[controller]")]
    public class ItemPedidoController : ControllerBase
    {
        private readonly IitemPedidoService _itemPedidoService;

        public ItemPedidoController(IitemPedidoService itemPedidoService)
        {
            _itemPedidoService = itemPedidoService;
        }

        //buscar Pedido por Id
        [HttpGet("{id}", Name = "GetItemPedidoPorId")]
        [ProducesResponseType(200, Type = typeof(ItemPedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ItemPedidoDto>> BuscarPorId(int id)
        {
            if (id <= 0)
                return BadRequest("Item do pedido inválido");

            var item = await _itemPedidoService.BuscarPorId(id);

            if (item == null)
                return NotFound("Item do pedido não encontrado");

            return Ok(item);
        }

        //FinalizarItem
        [HttpPut("{id}/finalizar", Name = "FinalizarItem")]
        [ProducesResponseType(200, Type = typeof(ItemPedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ItemPedidoDto>> FinalizarItem(int id)
        {
            if (id <= 0)
                return BadRequest("Item do pedido inválido");

            var item = await _itemPedidoService.FinalizarItem(id);

            if (item == null)
                return NotFound("Item do pedido não encontrado");

            return Ok(item);
        }

        //IniciarPreparo
        [HttpPut("{id}/iniciarPreparo", Name = "IniciarPreparo")]
        [ProducesResponseType(200, Type = typeof(ItemPedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ItemPedidoDto>> IniciarPreparo(int id)
        {
            if (id <= 0)
                return BadRequest("Item do pedido inválido");

            var item = await _itemPedidoService.IniciarPreparo(id);

            if (item == null)
                return NotFound("Item do pedido não encontrado");

            return Ok(item);
        }

        //MarcarComoPronto
        [HttpPut("{id}/marcarComoPronto", Name = "MarcarComoPronto")]
        [ProducesResponseType(200, Type = typeof(ItemPedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ItemPedidoDto>> MarcarComoPronto(int id)
        {
            if (id <= 0)
                return BadRequest("Item do pedido inválido");

            var item = await _itemPedidoService.MarcarComoPronto(id);

            if (item == null)
                return NotFound("Item do pedido não encontrado");

            return Ok(item);
        }

        //CancelarItem
        [HttpPut("{id}/cancelarItem", Name = "CancelarItem")]
        [ProducesResponseType(200, Type = typeof(ItemPedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ItemPedidoDto>> CancelarItem(int id)
        {
            if (id <= 0)
                return BadRequest("Item do pedido inválido");

            var item = await _itemPedidoService.CancelarItem(id);

            if (item == null)
                return NotFound("Item do pedido não encontrado");

            return Ok(item);
        }

        //ObterTotal
        [HttpGet("{id}/obterTotal", Name = "ObterTotal")]
        [ProducesResponseType(200, Type = typeof(ItemPedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ItemPedidoDto>> ObterTotal(int id)
        {
            if (id <= 0)
                return BadRequest("Item do pedido inválido");

            var item = await _itemPedidoService.ObterTotal(id);

            if (item == null)
                return NotFound("Item do pedido não encontrado");

            return Ok(item);
        }

        //buscar todos os pedidos
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ItemPedidoDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ItemPedidoDto>>> BuscarTodos()
        {
            var itens = await _itemPedidoService.BuscarTodos();

            return Ok(itens);
        }

        //buscar itens abertos
        [HttpGet("BuscarItensAbertos")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ItemPedidoDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<ItemPedidoDto>>> BuscarItensAbertos()
        {
            var itens = await _itemPedidoService.BuscarItensAbertos();

            if (itens == null)
                return NotFound("Itens do pedido não encontrados");

            return Ok(itens);
        }

        //buscar por status
        [HttpGet("status/{status}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ItemPedidoDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> BuscarPorStatus(StatusItemPedidoEnum status)
        {
            if (!Enum.IsDefined(typeof(StatusItemPedidoEnum), status))
                return BadRequest("Status inválido");

            var itens = await _itemPedidoService.BuscarPorStatus(status);

            if (itens == null)
                return NotFound("Itens do pedido não encontrados");

            return Ok(itens);
        }

        //Alterar Observação
        [HttpPut("{id}/observacao")]
        [ProducesResponseType(200, Type = typeof(ItemPedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AlterarObservacao(
            int id,
            [FromBody] ItemPedidoDto itemPedidoDto)
        {
            if (id <= 0)
                return BadRequest("Item do pedido inválido");

            var item = await _itemPedidoService.AlterarObservacao(
                id,
                itemPedidoDto.Observacao);

            if (item == null)
                return NotFound("Item do pedido não encontrado");

            return Ok(item);
        }

        //Alterar Quantidade
        [HttpPut("{id}/quantidade")]
        [ProducesResponseType(200, Type = typeof(ItemPedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AlterarQuantidade(
            int id,
            [FromBody] ItemPedidoDto itemPedidoDto)
        {
            if (id <= 0)
                return BadRequest("Item do pedido inválido");

            var item = await _itemPedidoService.AlterarQuantidade(
                id,
                itemPedidoDto.Quantidade);

            if (item == null)
                return NotFound("Item do pedido não encontrado");

            return Ok(item);
        }

        //atualizar
        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(ItemPedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Atualizar(
            int id,
            [FromBody] ItemPedidoDto pedidoDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            if (pedidoDto == null)
                return BadRequest();

            await _itemPedidoService.Atualizar(id, pedidoDto);

            return Ok(pedidoDto);
        }
    }
}