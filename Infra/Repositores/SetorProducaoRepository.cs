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
    public class SetorProducaoRepository : IsetorProducaoRepository
    {

        private readonly AppDbContext _setorProducaoContext;

        public SetorProducaoRepository(AppDbContext setorProducaoContext)
        {

            _setorProducaoContext = setorProducaoContext;
            
        }



        public async Task<SetorProducao> Adicionar(SetorProducao setor)
        {
            _setorProducaoContext.SetorProducaos.AddAsync(setor);
            await _setorProducaoContext.SaveChangesAsync();
            return setor;
        }

        public async Task<SetorProducao> Atualizar(SetorProducao setor)
        {
           _setorProducaoContext.SetorProducaos.Update(setor);
            await _setorProducaoContext.SaveChangesAsync();
            return setor;

        }

        public async Task<IEnumerable<SetorProducao>> BuscarAtivos()
        {
            return await _setorProducaoContext.SetorProducaos
                .Where(s => s.Ativo == true).ToListAsync();
        }

        public async Task<SetorProducao> BuscarPorId(int id)
        {
            return await _setorProducaoContext.SetorProducaos.FindAsync(id);
        }

        public async Task<SetorProducao> BuscarPorNome(string nome)
        {
            return await _setorProducaoContext.SetorProducaos
                .Where(s => s.Nome == nome).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SetorProducao>> BuscarTodos()
        {
           return await _setorProducaoContext.SetorProducaos.ToListAsync();
        }

        public async Task<SetorProducao> Remover(int id)
        {
            var remover = await BuscarPorId(id);
            _setorProducaoContext.Remove(remover);
            await _setorProducaoContext.SaveChangesAsync();
            return remover;

        }
    }
}
