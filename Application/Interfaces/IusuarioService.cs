using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IusuarioService
    {
        Task<CreateUserDto> CriarUsuario(CreateUserDto createDTO);
        Task<string> Login(LoginDto dto);
        Task<IEnumerable<CreateUserDto>> BuscarTodos();
        Task<CreateUserDto?> Excluir(string id);
        Task<string> ChangePassword(string userIdLogado, ChangePasswordDto dto, bool isAdmin);
    }

}





