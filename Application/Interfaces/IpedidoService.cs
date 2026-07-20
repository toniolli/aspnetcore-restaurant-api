using Application.Dto;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IpedidoService
    {
        Task<PedidoDto> Adicionar(PedidoDto pedidoDTO);
        Task<PedidoDto> Atualizar(int id, PedidoDto pedidoDTO);
        Task Remover(int id);
        Task<IEnumerable<PedidoDto>> BuscarTodos();
        Task<PedidoDto> BuscarPorId(int id);
        Task<IEnumerable<PedidoDto>> BuscarPorComandaId(int comandaId);
        Task<IEnumerable<PedidoDto>> BuscarPorMesaId(int mesaId);
        Task<IEnumerable<PedidoDto>> BuscarPorStatus(StatusPedidoEnum status);
        Task<IEnumerable<PedidoDto>> BuscarPedidosAbertos();


        Task<PedidoDto> AdicionarItemPedido(int pedidoId, ItemPedidoDto itemPedidoDto);
        Task<PedidoDto> RemoverItemPedido(int pedidoId, int itemPedidoDto);
        Task<PedidoDto> FinalizarPedido(int id);
        Task<PedidoDto> CancelarPedido(int id);
        Task<decimal> TotalPedido(int id);




    }
}
