using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class ItemCardapioDto
    {

        //public int Id_ItemCardapio {get; set;}
       //[Range(1, int.MaxValue)]
        //public int CardapioId {get;  set;}
        [Required]
        [StringLength(100)]
        public string Nome {get; set; }
        [StringLength(500)]
        public string? Descricao {get; set;}
        [Range(0.01, double.MaxValue)]
        public decimal Preco {get; set;}
        // public bool Disponivel { get; set;}
        public CategoriaEnum Categoria {get; set;}

    }
}
