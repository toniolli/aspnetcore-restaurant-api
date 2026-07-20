using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class MesaDto
    {
        //public int Id_Mesa {get; set;}
        [Required]
        [Range(1, int.MaxValue)]
        public int NumeroMesa {get;set;}
       // public StatusMesaEnum StatusMesa {get; set;}

    }
}
