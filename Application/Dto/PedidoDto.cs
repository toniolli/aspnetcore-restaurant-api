using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class PedidoDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "Mesa inválida.")]
        public int MesaId {get; set;}
        [Range(1, int.MaxValue, ErrorMessage = "Comanda inválida.")]
        public int ComandaId {get; set;}
    }
}
