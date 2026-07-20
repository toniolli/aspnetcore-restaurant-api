using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class SetorProducaoDto
    {
        //public int Id_SetorProducao {get; set;}
        [Required]
        [StringLength(100)]
        public string Nome {get; set;}
        // public bool Ativo {get; set;} = true;
        // public List<Pedido> Pedidos {get; set;}
    }
}
