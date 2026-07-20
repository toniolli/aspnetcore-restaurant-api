
using Domain.Entities;
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
    public class ItemCardapioRepository : IitemCardapioRepository
    {

        private readonly AppDbContext _itemCardapioContext;

        public ItemCardapioRepository(AppDbContext itemCardapioContext)
        {
            _itemCardapioContext = itemCardapioContext;
        }

        public async Task<ItemCardapio> Adicionar(ItemCardapio item)
        {
            _itemCardapioContext.ItemCardapios.AddAsync(item);
            await _itemCardapioContext.SaveChangesAsync();  
            return item;
        }

        public async Task<ItemCardapio> Atualizar(ItemCardapio item)
        {
            _itemCardapioContext.ItemCardapios.Update(item);
            await _itemCardapioContext.SaveChangesAsync();
            return item;

        }

        public async Task<IEnumerable<ItemCardapio>> BuscarDisponiveis()
        {
            return await _itemCardapioContext.ItemCardapios
                .Where(i => i.Disponivel == true).ToListAsync();
        }

        public async Task<IEnumerable<ItemCardapio>> BuscarPorCategoria(CategoriaEnum categoria)
        {
            return await _itemCardapioContext.ItemCardapios.Where(i => i.Categoria == categoria).ToListAsync();
        }

        public async Task<ItemCardapio> BuscarPorId(int id)
        {
            return await _itemCardapioContext.ItemCardapios.FindAsync(id);
        }

        public async Task<ItemCardapio> BuscarPorNome(string nome)
        {
           return await _itemCardapioContext.ItemCardapios.Where(i => i.Nome == nome).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ItemCardapio>> BuscarTodos()
        {
           return await _itemCardapioContext.ItemCardapios.ToListAsync();
        }

        public async Task<ItemCardapio> Remover(int id)
        {
           var remover = await BuscarPorId(id);
            _itemCardapioContext.Remove(remover);
            await _itemCardapioContext.SaveChangesAsync();
            return remover;
        }
    }
}
