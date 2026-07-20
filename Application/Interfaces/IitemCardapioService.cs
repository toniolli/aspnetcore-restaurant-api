using Application.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IitemCardapioService
    {
        Task<ItemCardapioDto> Atualizar(int id,ItemCardapioDto itemDTO);
        Task<IEnumerable<ItemCardapioDto>> BuscarTodos();
        Task<ItemCardapioDto> BuscarPorId(int id);
        Task<ItemCardapioDto> BuscarPorNome(string nome);
        Task<IEnumerable<ItemCardapioDto>> BuscarDisponiveis();
        Task<IEnumerable<ItemCardapioDto>> BuscarPorCategoria(CategoriaEnum categoria);
        Task<ItemCardapioDto> AlterarNome(int id,string nome);
        Task<ItemCardapioDto> AlterarDescricao(int id, string descricao);
        Task<ItemCardapioDto> AlterarPreco(int id,decimal preco);
        Task<ItemCardapioDto> AlterarCategoria(int id, CategoriaEnum categoria); 




    }
}
