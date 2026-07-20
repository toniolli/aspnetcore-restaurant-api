
using Domain.Entities;
using Domain.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositores
{
    public class MesaRepository : ImesaRepository
    {
        private readonly AppDbContext _mesaContext;

        public MesaRepository(AppDbContext mesaContext)
        {
            _mesaContext = mesaContext;
        }
        public async Task<Mesa> Adicionar(Mesa mesa)
        {
            _mesaContext.AddAsync(mesa);
            await _mesaContext.SaveChangesAsync();
            return mesa;
        }

        public async Task<Mesa> Atualizar(Mesa mesa)
        {
            _mesaContext.Update(mesa);
            await _mesaContext.SaveChangesAsync();
            return mesa;
        }

        public async Task<Mesa> BuscarPorId(int id)
        {
            return await _mesaContext.Mesas.FindAsync(id);
        }

        public async Task<Mesa> BuscarPorNumeroMesa(int numero)
        {

            return await _mesaContext.Mesas
                    .FirstOrDefaultAsync(x => x.NumeroMesa == numero);

        }

        public async Task<IEnumerable<Mesa>> BuscarTodos()
        {
            return await _mesaContext.Mesas.ToListAsync();
        }

        public async Task<Mesa> Remover(int id)
        {
            var remover = await BuscarPorId(id);
            _mesaContext.Remove(remover);
            await _mesaContext.SaveChangesAsync();
            return remover;
        }
    }
}
