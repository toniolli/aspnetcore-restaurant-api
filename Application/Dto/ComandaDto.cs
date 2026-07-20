using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class ComandaDto
    {
        //public int Id_Comanda {get; set;}
        [Range(1, int.MaxValue, ErrorMessage = "Mesa inválida.")]
        public int MesaId {get; set;}
         // public StatusComandaEnum StatusComanda {get; set;}
         //  public DateTime DataAbertura {get; set;}
         //  public DateTime? DataFechamento {get; set;}


    }
}
