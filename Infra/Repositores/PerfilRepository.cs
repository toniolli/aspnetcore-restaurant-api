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
    public class PerfilRepository : IperfilRepository
    {


        private readonly AppDbContext _perfilContext;
        public PerfilRepository(AppDbContext perfilContext)
        {
            _perfilContext = perfilContext;
        }


        public async Task<Perfil> Adicionar(Perfil perfil)
        {
            _perfilContext.Perfis.AddAsync(perfil);
            await _perfilContext.SaveChangesAsync();
            return perfil;
        }

        public async Task<Perfil?> BuscarPorId(int id)
        {
            return await _perfilContext.Perfis.FindAsync(id);
        }

        public async Task<IEnumerable<Perfil>> BuscarTodos()
        {
            return  await _perfilContext.Perfis.ToListAsync();
        }

        public async Task<Perfil> Atualizar(Perfil perfil)
        {
            _perfilContext.Perfis.Update(perfil);
            await _perfilContext.SaveChangesAsync();
            return perfil;
        }

        public async Task Remover(int id)
        {
            var perfil = await BuscarPorId(id);

            _perfilContext.Remove(perfil);

            await _perfilContext.SaveChangesAsync();
        }


    }
}
