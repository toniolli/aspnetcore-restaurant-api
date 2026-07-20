using Application.Dto;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IcomandaService
    {

        Task<ComandaDto> Adicionar(ComandaDto comandaDTO);
        Task<ComandaDto> Atualizar(int id,ComandaDto comandaDTO);
        Task Remover(int id);
        Task<IEnumerable<ComandaDto>> BuscarTodos();
        Task<ComandaDto> BuscarPorId(int id);
        Task<ComandaDto> BuscarComandaAbertaPorMesa(int mesaId);
        Task<IEnumerable<ComandaDto>> BuscarComandasFechadas();
        Task<ComandaDto> FecharComanda(int comandaId);
    }
}
