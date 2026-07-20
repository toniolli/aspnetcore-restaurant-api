using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IcomandaRepository
    {

        Task<Comanda> Adicionar(Comanda comanda);
        Task<Comanda> Atualizar(Comanda comanda);
        Task<Comanda> Remover(int id);
        Task<IEnumerable<Comanda>> BuscarTodos();
        Task<Comanda> BuscarPorId(int id);
        Task<Comanda> BuscarComandaAbertaPorMesa(int mesaId);
        Task<IEnumerable<Comanda>> BuscarComandasFechadas();
    }
}
