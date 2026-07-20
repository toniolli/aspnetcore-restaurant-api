using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IpedidoRepository
    {

        Task<Pedido> Adicionar(Pedido pedido);
        Task<Pedido> Atualizar(Pedido pedido);
        Task<Pedido> Remover(int id);
        Task<IEnumerable<Pedido>> BuscarTodos();
        Task<Pedido> BuscarPorId(int id);
        Task<IEnumerable<Pedido>> BuscarPorComandaId(int comandaId);
        Task<IEnumerable<Pedido>> BuscarPorMesaId(int mesaId);
        Task<IEnumerable<Pedido>> BuscarPorStatus(StatusPedidoEnum status);
        Task<IEnumerable<Pedido>> BuscarPedidosAbertos();



    }
}
