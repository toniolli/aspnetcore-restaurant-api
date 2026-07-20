using Domain.Entities;
using Domain.Interfaces;
using Domain.Validation;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Repositores
{
    public class PerfilPermissaoRepository : IperfilPermissaoRepository
    {

        private readonly AppDbContext _permissaoContext;
        public PerfilPermissaoRepository(AppDbContext permissaoContext)
        {
            _permissaoContext = permissaoContext;
        }

        public async Task<IEnumerable<Perfil>> BuscarPerfisPermissao(int permissaoId)
        {
           return   await _permissaoContext.PerfilPermissoes
                .Where(p => p.PermissaoId == permissaoId)
                .Select(p => p.Perfil)
                .ToListAsync();
        }

        public async Task<IEnumerable<Permissao>> BuscarPermissoesPerfil(int perfilId)
        {
            return await _permissaoContext.PerfilPermissoes
               .Where(p => p.PerfilId == perfilId)
               .Select(p => p.Permissao)
               .ToListAsync();
        }

        public async Task<PerfilPermissao?> Desvincular(int perfilId, int permissaoId)
        {

            var perfilPermissao = await _permissaoContext.PerfilPermissoes
                .FirstOrDefaultAsync(p => p.PerfilId == perfilId && p.PermissaoId == permissaoId);

            if (perfilPermissao == null)
                return null;

            _permissaoContext.Remove(perfilPermissao);

            return perfilPermissao; 

        }

        public async Task<bool> ExisteVinculo(int perfilId, int permissaoId)
        {

            return await _permissaoContext.PerfilPermissoes
                .AnyAsync(p => p.PerfilId == perfilId && p.PermissaoId == permissaoId);
        }

        public async Task<PerfilPermissao> Vincular(PerfilPermissao perfilPermissao)
        {
            await _permissaoContext.AddAsync(perfilPermissao);
            await _permissaoContext.SaveChangesAsync();
            return perfilPermissao;

        }
    }
}
