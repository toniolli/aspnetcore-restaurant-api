
using Application.Dto;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{   
    public interface ImesaService
    {
        Task<MesaDto> Adicionar(MesaDto mesa);
        Task<MesaDto> Atualizar(int id,MesaDto mesa);
        Task Remover(int id);
        Task<IEnumerable<MesaDto>> BuscarTodos();
        Task<MesaDto> BuscarPorId(int id);
        Task<MesaDto> BuscarPorNumeroMesa(int numero);
        Task<MesaDto> OcuparMesa(int numeroMesa);
        Task<MesaDto> LiberarMesa(int numeroMesa);
    }
}
