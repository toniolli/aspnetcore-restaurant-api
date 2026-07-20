using Application.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IcardapioService
    {
        Task<CardapioDto> Adicionar(CardapioDto cardapioDTO);
        Task<CardapioDto> Atualizar(int id ,CardapioDto cardapioDTO);
        Task Remover(int id);
        Task<IEnumerable<CardapioDto>> BuscarTodos();
        Task<CardapioDto> BuscarPorId(int id);
        Task<CardapioDto> BuscarPorNome(string nome);
        Task<CardapioDto> AdicionarItem(int id, ItemCardapioDto itemCardapioDto);
        Task <CardapioDto> RemoverItem(int cardapioId,int itemCardapioId);

    }
}
