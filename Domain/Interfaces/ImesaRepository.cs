using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ImesaRepository
    {
        Task<Mesa> Adicionar (Mesa mesa);
        Task<Mesa> Atualizar(Mesa mesa);
        Task<Mesa> Remover (int id);
        Task<IEnumerable<Mesa>> BuscarTodos();
        Task<Mesa> BuscarPorId (int id);
        Task<Mesa> BuscarPorNumeroMesa(int numero);

    }
}
