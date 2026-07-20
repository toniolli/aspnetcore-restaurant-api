using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IsetorProducaoRepository
    {
        Task<SetorProducao> Adicionar(SetorProducao setor);
        Task<SetorProducao> Atualizar(SetorProducao setor);
        Task<SetorProducao> Remover(int id);
        Task<IEnumerable<SetorProducao>> BuscarTodos();
        Task<SetorProducao> BuscarPorId(int id);
        Task<SetorProducao> BuscarPorNome(string nome);
        Task<IEnumerable<SetorProducao>> BuscarAtivos();
    }
}
