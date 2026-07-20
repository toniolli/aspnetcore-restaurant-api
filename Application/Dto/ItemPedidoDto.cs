using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class ItemPedidoDto
    {

        //[Range(1, int.MaxValue)]
        //public int PedidoId {get; set;}
        [Range(1, int.MaxValue)]
        public int ItemCardapioId {get; set;}
        [Range(1, int.MaxValue, ErrorMessage = "Setor de produção inválido.")]
        public int SetorProducaoId { get; set; }
        [Range(1, int.MaxValue)]
        public int Quantidade {get; set;}
        [StringLength(500)]
        public string? Observacao {get; set;}

    }
}
