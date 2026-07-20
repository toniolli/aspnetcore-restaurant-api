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
    public class CardapioRepository : IcardapioRepository
    {

        private readonly AppDbContext _cardapioContext;

        public CardapioRepository(AppDbContext cardapioContext)
        {
            
            _cardapioContext = cardapioContext;

        }

        public async Task<Cardapio> Adicionar(Cardapio cardapio)
        {
            _cardapioContext.Cardapios.AddAsync(cardapio);
            await _cardapioContext.SaveChangesAsync();
            return cardapio;
        }

        public async Task<Cardapio> Atualizar(Cardapio cardapio)
        {
            _cardapioContext.Cardapios.Update(cardapio);
            await _cardapioContext.SaveChangesAsync();
            return cardapio;
        }

        public async Task<Cardapio> BuscarPorId(int id)
        {
            return await _cardapioContext.Cardapios
                    .Include(c => c.Itens)
                    .FirstOrDefaultAsync(c => c.Id_Cardapio == id);
        }

        public async Task<Cardapio> BuscarPorNome(string nome)
        {
            return await _cardapioContext.Cardapios.Where(c => c.Nome == nome).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cardapio>> BuscarTodos()
        {
            return await _cardapioContext.Cardapios.ToListAsync();
        }

        public async Task<Cardapio> Remover(int id)
        {
            var remover = await BuscarPorId(id);
            _cardapioContext.Remove(remover);
            await _cardapioContext.SaveChangesAsync();
            return remover;
        }
    }
}
