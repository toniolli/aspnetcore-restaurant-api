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
    public class UsuarioPerfilRepository : IusuarioPerfilRepository
    {

        private readonly AppDbContext _usuarioPerfilContext;

        public UsuarioPerfilRepository(AppDbContext usuarioPerfilContext)
        {
            _usuarioPerfilContext = usuarioPerfilContext;
        }


        public async Task<IEnumerable<Perfil>> BuscarPerfisUsuario(string usuarioId)
        {
            
            return await _usuarioPerfilContext.UsuariosPerfis
                .Where(p => p.UsuarioId == usuarioId)
                .Select(p => p.Perfil)
                .ToListAsync();

        }

        public async Task<IEnumerable<UsuarioPerfil>> BuscarUsuariosPerfil(int perfilId)
        {
            return await _usuarioPerfilContext.UsuariosPerfis
                .Where(p => p.PerfilId == perfilId)
                .ToListAsync();
        }

        public async Task<UsuarioPerfil?> Desvincular(string usuarioId, int perfilId)
        {
            var usuarioPerfil = await _usuarioPerfilContext.UsuariosPerfis
                .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.PerfilId == perfilId);

            if(usuarioPerfil == null)
                return null;

            _usuarioPerfilContext.Remove(usuarioPerfil);
            return usuarioPerfil;
        }

        public async Task<bool> ExisteVinculo(string usuarioId, int perfilId)
        {
           return await _usuarioPerfilContext.UsuariosPerfis
                .AnyAsync(p => p.UsuarioId == usuarioId && p.PerfilId==perfilId);

        }

        public async Task<UsuarioPerfil> Vincular(UsuarioPerfil usuarioPerfil)
        {
            await _usuarioPerfilContext.AddAsync(usuarioPerfil);
            await _usuarioPerfilContext.SaveChangesAsync();
            return usuarioPerfil;
        }
    }
}
