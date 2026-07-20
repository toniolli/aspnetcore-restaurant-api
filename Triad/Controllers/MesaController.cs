using Application.Dto;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Triad.Authorization;

namespace Triad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {
        private readonly ImesaService _mesaService;

        public MesaController(ImesaService mesaService)
        {
            _mesaService = mesaService;
        }

        [HttpGet("{id}", Name = "GetMesaPorId")]
        [Authorize]
        [Permissao(
            "GERENCIAR_MESAS",
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_RESERVAS")]
        [ProducesResponseType(200, Type = typeof(MesaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<MesaDto>> BuscarPorId(int id)
        {
            if (id <= 0)
                return BadRequest("Mesa inválida");

            var mesa = await _mesaService.BuscarPorId(id);

            if (mesa == null)
                return NotFound("Mesa não encontrada");

            return Ok(mesa);
        }

        [HttpGet("numero/{numeroMesa}", Name = "GetMesaPorNumero")]
        [Authorize]
        [Permissao(
            "GERENCIAR_MESAS",
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_RESERVAS")]
        [ProducesResponseType(200, Type = typeof(MesaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<MesaDto>> BuscarPorNumeroMesa(int numeroMesa)
        {
            if (numeroMesa <= 0)
                return BadRequest("Número da mesa inválido");

            var mesa = await _mesaService.BuscarPorNumeroMesa(numeroMesa);

            if (mesa == null)
                return NotFound("Mesa não encontrada");

            return Ok(mesa);
        }

        [HttpGet]
        [Authorize]
        [Permissao(
            "GERENCIAR_MESAS",
            "GERENCIAR_PEDIDOS",
            "GERENCIAR_RESERVAS")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<MesaDto>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<MesaDto>>> BuscarTodos()
        {
            var mesas = await _mesaService.BuscarTodos();

            if (mesas == null)
                return NotFound("Nenhuma mesa encontrada");

            return Ok(mesas);
        }

        [HttpPost]
        [Authorize]
        [Permissao("GERENCIAR_MESAS")]
        [ProducesResponseType(200, Type = typeof(MesaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Adicionar([FromBody] MesaDto mesaDto)
        {
            if (mesaDto == null)
                return BadRequest();

            await _mesaService.Adicionar(mesaDto);

            return Ok(mesaDto);
        }

        [HttpPost("{numeroMesa}/ocupar")]
        [Authorize]
        [Permissao(
            "GERENCIAR_MESAS",
            "GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(MesaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> OcuparMesa(int numeroMesa)
        {
            if (numeroMesa <= 0)
                return BadRequest();

            var mesa = await _mesaService.BuscarPorNumeroMesa(numeroMesa);

            if (mesa == null)
                return NotFound();

            await _mesaService.OcuparMesa(numeroMesa);

            return Ok();
        }

        [HttpPost("{numeroMesa}/liberar")]
        [Authorize]
        [Permissao(
            "GERENCIAR_MESAS",
            "GERENCIAR_PEDIDOS")]
        [ProducesResponseType(200, Type = typeof(MesaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> LiberarMesa(int numeroMesa)
        {
            if (numeroMesa <= 0)
                return BadRequest();

            var mesa = await _mesaService.BuscarPorNumeroMesa(numeroMesa);

            if (mesa == null)
                return NotFound();

            await _mesaService.LiberarMesa(numeroMesa);

            return Ok();
        }

        [HttpPut("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_MESAS")]
        [ProducesResponseType(200, Type = typeof(MesaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Atualizar(int id, [FromBody] MesaDto mesaDto)
        {
            if (id <= 0)
                return BadRequest("Id inválido");

            if (mesaDto == null)
                return BadRequest();

            await _mesaService.Atualizar(id, mesaDto);

            return Ok(mesaDto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [Permissao("GERENCIAR_MESAS")]
        [ProducesResponseType(200, Type = typeof(MesaDto))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Remover(int id)
        {
            if (id <= 0)
                return BadRequest("Código inválido");

            await _mesaService.Remover(id);

            return Ok();
        }
    }
}