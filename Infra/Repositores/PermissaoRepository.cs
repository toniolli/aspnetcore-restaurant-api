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
    public class PermissaoRepository : IpermissaoRepository
    {

        private readonly AppDbContext _permissaoContext;
        public PermissaoRepository(AppDbContext permissaoContext)
        {
            _permissaoContext = permissaoContext;
        }


        public async Task<Permissao> Adicionar(Permissao permissao)
        {
            _permissaoContext.Permissoes.AddAsync(permissao);
            await _permissaoContext.SaveChangesAsync();
            return permissao;
        }

        public async Task<Permissao?> BuscarPorId(int id)
        {
            return await _permissaoContext.Permissoes.FindAsync(id);
        }

        public async Task<IEnumerable<Permissao>> BuscarTodos()
        {
            return await _permissaoContext.Permissoes.ToListAsync();
        }

        public async Task<Permissao> Atualizar(Permissao permissao)
        {
            _permissaoContext.Permissoes.Update(permissao);
            await _permissaoContext.SaveChangesAsync();
            return permissao;
        }

        public async Task Remover(int id)
        {
            var permissao = await BuscarPorId(id);

            _permissaoContext.Remove(permissao);

            await _permissaoContext.SaveChangesAsync();
        }

    }
}
