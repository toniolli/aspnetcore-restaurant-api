using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        public string? PasswordAtual { get; set; }
        [Required]
        public string? NovaSenha { get; set; }
        [Required]
        [Compare(nameof(NovaSenha))]
        public string? ConfirmacaoNovaSenha { get; set; }



    }
}
