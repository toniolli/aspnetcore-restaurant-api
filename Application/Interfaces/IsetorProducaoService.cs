using Application.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public  interface IsetorProducaoService
    {
        Task<SetorProducaoDto> Adicionar(SetorProducaoDto setorDTO);
        Task<SetorProducaoDto> Atualizar(int id ,SetorProducaoDto setorDTO);
        Task Remover(int id);
        Task<IEnumerable<SetorProducaoDto>> BuscarTodos();
        Task<SetorProducaoDto> BuscarPorId(int id);
        Task<SetorProducaoDto> BuscarPorNome(string nome);
        Task<IEnumerable<SetorProducaoDto>> BuscarAtivos();
    }
}
