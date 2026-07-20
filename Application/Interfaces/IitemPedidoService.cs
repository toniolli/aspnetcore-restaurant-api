using Application.Dto;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IitemPedidoService
    {
        
        Task<ItemPedidoDto> Atualizar(int id, ItemPedidoDto itemPedidoDto);
        Task<ItemPedidoDto> BuscarPorId(int id);
        Task<IEnumerable<ItemPedidoDto>> BuscarTodos();
        Task<IEnumerable<ItemPedidoDto>> BuscarPorPedidoId(int pedidoId);
        Task<IEnumerable<ItemPedidoDto>> BuscarPorStatus(StatusItemPedidoEnum status);
        Task<IEnumerable<ItemPedidoDto>> BuscarItensAbertos();
        Task<ItemPedidoDto> AlterarQuantidade(int id, int quantidade);
        Task<ItemPedidoDto> AlterarObservacao(int id, string? observacao);
        Task<ItemPedidoDto> IniciarPreparo(int id);
        Task<ItemPedidoDto> MarcarComoPronto(int id);
        Task<ItemPedidoDto> FinalizarItem(int id);
        Task<ItemPedidoDto> CancelarItem(int id);
        Task<decimal> ObterTotal(int id);
    }
}
