using Domain.Enum;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Pedido
    {
        public int Id_Pedido {get; private set;}
        public int MesaId {get; private set;}
        public int ComandaId {get; private set;}
         // pode virar um enum 
        public StatusPedidoEnum Status {get; private set;}
        public DateTime DataCriacao {get; private set;}
        //Navegação
        
        public Comanda Comanda {get; private set;}
        public Mesa Mesa {get; private set;}
        public List<ItemPedido> ListaItemPedidos { get; private set; } = new();

        protected Pedido(){}
        public Pedido(int mesaId,int comandaId)
        {
            MesaId = mesaId;
            ComandaId = comandaId;
            Status = StatusPedidoEnum.Pendente;
            DataCriacao = DateTime.UtcNow;  
        }

        public void ValidateDomain(DateTime dataCriacao)
        {
            DomainExceptionValidation.when(dataCriacao == DateTime.MinValue, "Data Invalida");
        }

        public void AdicionarItem(ItemPedido itemPedido)
        {
            if (itemPedido == null)
                throw new DomainExceptionValidation("Item do pedido é obrigatório");

            if (Status != StatusPedidoEnum.Pendente)
                throw new DomainExceptionValidation(
                    "Não é possível adicionar itens a um pedido finalizado ou cancelado");

            ListaItemPedidos.Add(itemPedido);
        }

        public void AtualizarPedido(int mesaId,int comandaId)
        {
            MesaId = mesaId;
            ComandaId = comandaId;
        }


        public void FinalizarPedido()
        {
            if (!ListaItemPedidos.Any())
                throw new DomainExceptionValidation("Pedido não possui itens");

            if(Status != StatusPedidoEnum.Pronto)
                throw new DomainExceptionValidation
                      ("Só é possoivel finalizar um pedido se ele estiver pronto ");
           
            Status = StatusPedidoEnum.Finalizado;
        }
        public void CancelarPedido()
        {
            if(Status != StatusPedidoEnum.Pendente)
                throw new DomainExceptionValidation("O pedido só pode ser cancelado se estiver pendente");
           
            Status = StatusPedidoEnum.Cancelado;
        }

        public decimal TotalPedido()
        {
            return (decimal)ListaItemPedidos.Sum(p => p.ValorUnitario * p.Quantidade);
        }

        public void RemoverItem(int itemPedidoId)
        {
            var item = ListaItemPedidos.FirstOrDefault(i => i.Id_ItemPedido == itemPedidoId);

            if (item == null)
                throw new DomainExceptionValidation("Item do pedido não encontrado");

            ListaItemPedidos.Remove(item);
        }
    }
}
