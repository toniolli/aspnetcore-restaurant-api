using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IitemCardapioRepository
    {
        Task<ItemCardapio> Adicionar(ItemCardapio item);
        Task<ItemCardapio> Atualizar(ItemCardapio item);
        Task<ItemCardapio> Remover(int id);
        Task<IEnumerable<ItemCardapio>> BuscarTodos();
        Task<ItemCardapio> BuscarPorId(int id);
        Task<ItemCardapio> BuscarPorNome(string nome);
        Task<IEnumerable<ItemCardapio>> BuscarDisponiveis();
        Task<IEnumerable<ItemCardapio>> BuscarPorCategoria(CategoriaEnum categoria);

    }
}
