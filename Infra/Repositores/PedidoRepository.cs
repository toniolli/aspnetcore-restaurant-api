
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositores
{
    public class PedidoRepository : IpedidoRepository
    {
        private readonly AppDbContext _pedidoContext;

        public PedidoRepository(AppDbContext pedidoContext)
        {
            _pedidoContext = pedidoContext;
        }


        public async Task<Pedido> Adicionar(Pedido pedido)
        {
            _pedidoContext.Pedidos.AddAsync(pedido);
            await _pedidoContext.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> Atualizar(Pedido pedido)
        {
            _pedidoContext.Pedidos.Update(pedido);
            await _pedidoContext.SaveChangesAsync();
            return pedido;
        }

        public async Task<IEnumerable<Pedido>> BuscarPedidosAbertos()
        {
            return await _pedidoContext.Pedidos
                .Where(p => p.Status != StatusPedidoEnum.Finalizado &&
                            p.Status != StatusPedidoEnum.Cancelado).ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> BuscarPorComandaId(int comandaId)
        {
            return await _pedidoContext.Pedidos.AsNoTracking().Where(p => p.ComandaId == comandaId).ToListAsync();
        }

        public async Task<Pedido> BuscarPorId(int id)
        {
            return await _pedidoContext.Pedidos.FindAsync(id);
        }

        public async Task<IEnumerable<Pedido>> BuscarPorMesaId(int mesaId)
        {
            return await _pedidoContext.Pedidos.Where(p => p.MesaId == mesaId).ToListAsync();
        }


        public async Task<IEnumerable<Pedido>> BuscarPorStatus(StatusPedidoEnum status)
        {
            return await _pedidoContext.Pedidos.Where(p => p.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> BuscarTodos()
        {
            return await  _pedidoContext.Pedidos.ToListAsync();    
        }

        public async Task<Pedido> Remover(int id)
        {
            var remover = await BuscarPorId(id);
            _pedidoContext.Remove(remover);
            await _pedidoContext.SaveChangesAsync();
            return remover;
        }
    }
}
