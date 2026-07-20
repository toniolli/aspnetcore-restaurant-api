using Application.Dto;
using Application.Interfaces;
using Domain.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Triad.Authorization;

namespace Triad.Controllers
{
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IpedidoService _pedidoService;

        public PedidoController(IpedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        //buscar Pedido por Id
        [HttpGet("{id}", Name = "GetPedidoPorId")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(PedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<PedidoDto>> BuscarPorId(int id)
        {
            if (id <= 0)
                return BadRequest("Pedido inválido");

            var pedido = await _pedidoService.BuscarPorId(id);

            if (pedido == null)
                return NotFound("Pedido não encontrado");

            return Ok(pedido);
        }

        //buscar todos os pedidos
        [HttpGet]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PedidoDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> BuscarTodos()
        {
            var pedidos = await _pedidoService.BuscarTodos();

            if (pedidos == null)
                return NotFound("Nenhum pedido encontrado");

            return Ok(pedidos);
        }

        //buscar pedidos abertos
        [HttpGet("abertos")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PedidoDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> BuscarPedidosAberto()
        {
            var pedidos = await _pedidoService.BuscarPedidosAbertos();

            if (pedidos == null)
                return NotFound("Pedidos abertos não encontrados");

            return Ok(pedidos);
        }

        //buscar por status
        [HttpGet("status/{status}")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PedidoDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> BuscarPorStatus(StatusPedidoEnum status)
        {
            var pedidos = await _pedidoService.BuscarPorStatus(status);

            if (pedidos == null)
                return NotFound("Nenhum pedido com o status informado foi encontrado");

            return Ok(pedidos);
        }

        //adicionar
        [HttpPost]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_MESAS")]
        [ProducesResponseType(200, Type = typeof(PedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Adicionar([FromBody] PedidoDto pedidoDto)
        {
            if (pedidoDto == null)
                return BadRequest();

            await _pedidoService.Adicionar(pedidoDto);

            return Ok(pedidoDto);
        }

        //adicionar item pedido
        [HttpPost("{pedidoId}/itens")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_MESAS")]
        [ProducesResponseType(200, Type = typeof(PedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AdicionarItemPedido(
            int pedidoId,
            [FromBody] ItemPedidoDto itemPedidoDto)
        {
            if (pedidoId <= 0)
                return BadRequest("Pedido inválido");

            if (itemPedidoDto == null)
                return BadRequest();

            var pedido = await _pedidoService.BuscarPorId(pedidoId);

            if (pedido == null)
                return NotFound("Pedido não encontrado");

            await _pedidoService.AdicionarItemPedido(
                pedidoId,
                itemPedidoDto);

            return Ok(itemPedidoDto);
        }

        //atualizar
        [HttpPut("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(PedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Atualizar(
            int id,
            [FromBody] PedidoDto pedidoDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            if (pedidoDto == null)
                return BadRequest();

            await _pedidoService.Atualizar(id, pedidoDto);

            return Ok(pedidoDto);
        }

        //cancelar pedido
        [HttpPost("{id}/cancelar")]
        [Authorize]
        [Permissao("GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CancelarPedido(int id)
        {
            if (id <= 0)
                return BadRequest("Pedido não encontrado");

            var pedido = await _pedidoService.BuscarPorId(id);

            if (pedido == null)
                return NotFound();

            await _pedidoService.CancelarPedido(id);

            return Ok("Pedido cancelado com sucesso");
        }

        //finalizar pedido
        [HttpPost("{id}/finalizar")]
        [Authorize]
        [Permissao(
            "GERENCIAR_PEDIDOS",
            "ACESSAR_COZINHA",
            "ACESSAR_BAR")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> FinalizarPedido(int id)
        {
            if (id <= 0)
                return BadRequest("Pedido não encontrado");

            var pedido = await _pedidoService.BuscarPorId(id);

            if (pedido == null)
                return NotFound();

            await _pedidoService.FinalizarPedido(id);

            return Ok("Pedido finalizado com sucesso");
        }

        //remover pedido
        [HttpDelete("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(PedidoDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Remover(int id)
        {
            if (id <= 0)
                return BadRequest("Código inválido");

            await _pedidoService.Remover(id);

            return Ok();
        }

        //remover item do pedido
        [HttpDelete("{pedidoId}/itens/{itemPedidoId}")]
        [Authorize]
        [Permissao("GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> RemoverItemPedido(
            int pedidoId,
            int itemPedidoId)
        {
            if (pedidoId <= 0 || itemPedidoId <= 0)
                return BadRequest("Id inválido");

            var resultado = await _pedidoService.RemoverItemPedido(
                pedidoId,
                itemPedidoId);

            return Ok(resultado);
        }
    }
}