
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositores
{
    public class ComandaRepository : IcomandaRepository
    {

        private readonly AppDbContext _comandaContext;

        public ComandaRepository(AppDbContext comandaContext)
        {
            _comandaContext = comandaContext;     
        }

        public async Task<Comanda> Adicionar(Comanda comanda)
        {
           _comandaContext.AddAsync(comanda);
            await _comandaContext.SaveChangesAsync();
            return comanda;
        }

        public async Task<Comanda> Atualizar(Comanda comanda)
        { 
            _comandaContext.Update(comanda);
            await _comandaContext.SaveChangesAsync();
            return comanda;
        }

        public async Task<Comanda> BuscarComandaAbertaPorMesa(int mesaId)
        {
            return await _comandaContext.Comandas.FindAsync(mesaId);
        }

        public async Task<IEnumerable<Comanda>> BuscarComandasFechadas()
        {
            return await _comandaContext.Comandas 
                .Where(c => c.StatusComanda == StatusComandaEnum.Fechada).AsNoTracking().ToListAsync();
        }

        public async Task<Comanda> BuscarPorId(int id)
        {
            return await _comandaContext.Comandas.FindAsync(id);
        }

        public async Task<IEnumerable<Comanda>> BuscarTodos()
        {
            return  await _comandaContext.Comandas.ToListAsync();
        }

        public async Task<Comanda> Remover(int id)
        {
            var remover = await BuscarPorId(id);
            _comandaContext.Remove(remover);
            await _comandaContext.SaveChangesAsync();
            return remover;
        }
    }
}
