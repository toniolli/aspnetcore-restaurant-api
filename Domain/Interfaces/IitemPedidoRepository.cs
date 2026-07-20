using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IitemPedidoRepository
    {

        Task<ItemPedido> Adicionar(ItemPedido itemPedido);
        Task<ItemPedido> Atualizar(ItemPedido itemPedido);
        Task<ItemPedido> Remover(int id);
        Task<IEnumerable<ItemPedido>> BuscarTodos();
        Task<ItemPedido> BuscarPorId(int id);
        Task<IEnumerable<ItemPedido>> BuscarPorPedidoId(int pedidoId);
        Task<IEnumerable<ItemPedido>> BuscarPorStatus(StatusItemPedidoEnum status);
        Task<IEnumerable<ItemPedido>> BuscarItensAbertos();


    }
}
