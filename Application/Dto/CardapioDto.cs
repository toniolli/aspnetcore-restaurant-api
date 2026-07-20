using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class CardapioDto
    {

        // public int Id_Cardapio {get; set;}
        [Required]
        [StringLength(100)]
        public string Nome {get; set;}
        //public List<ItemCardapio>? Itens {get; set;}
    }
}
