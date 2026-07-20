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
    public class ItemPedidoRepository : IitemPedidoRepository
    {

        private readonly AppDbContext _ItemPedidoContext;

        public ItemPedidoRepository(AppDbContext itemPedidoContext)
        {

            _ItemPedidoContext = itemPedidoContext;
            
        }


        public async Task<ItemPedido> Adicionar(ItemPedido itemPedido)
        {
            _ItemPedidoContext.ItemPedidos.Add(itemPedido);
            await _ItemPedidoContext.SaveChangesAsync();
            return itemPedido;
        }

        public async Task<ItemPedido> Atualizar(ItemPedido itemPedido)
        {
            _ItemPedidoContext.ItemPedidos.Update(itemPedido);
            await _ItemPedidoContext.SaveChangesAsync();
            return itemPedido;
        }

        public async Task<IEnumerable<ItemPedido>> BuscarItensAbertos()
        {
            return await _ItemPedidoContext.ItemPedidos
                .Where(i => i.Status != StatusItemPedidoEnum.Finalizado && 
                            i.Status != StatusItemPedidoEnum.Cancelado).ToListAsync();
        }

        public async Task<ItemPedido> BuscarPorId(int id)
        {
            return await _ItemPedidoContext.ItemPedidos.FindAsync(id);
        }

        public async Task<IEnumerable<ItemPedido>> BuscarPorPedidoId(int pedidoId)
        {
            return await _ItemPedidoContext.ItemPedidos
                .Where(i => i.PedidoId == pedidoId).ToListAsync();
        }

        public async Task<IEnumerable<ItemPedido>> BuscarPorStatus(StatusItemPedidoEnum status)
        {
            return await _ItemPedidoContext.ItemPedidos
                .Where(i => i.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<ItemPedido>> BuscarTodos()
        {
            return await _ItemPedidoContext.ItemPedidos.ToListAsync();
        }

        public async Task<ItemPedido> Remover(int id)
        {
            var remover = await BuscarPorId(id);
            _ItemPedidoContext.Remove(id);
            await _ItemPedidoContext.SaveChangesAsync();
            return remover;
        }
    }
}
