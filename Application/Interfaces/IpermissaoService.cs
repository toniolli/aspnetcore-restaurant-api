using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPermissaoService
    {
        Task<PermissaoDto> Adicionar(PermissaoDto permissaoDto);
        Task<PermissaoDto?> BuscarPorId(int id);
        Task<IEnumerable<PermissaoDto>> BuscarTodos();
        Task<PermissaoDto> Atualizar(int id, PermissaoDto permissaoDto);
        Task Remover(int id);
    }

}
