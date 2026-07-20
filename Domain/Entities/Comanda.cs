using Domain.Enum;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comanda
    {
        public int Id_Comanda { get; private set; }
        public int MesaId { get; private set;}
        public StatusComandaEnum StatusComanda { get; private set; }
        public DateTime DataAbertura { get; private set;}
        public DateTime? DataFechamento { get; private set;}
       

        //Navegação
        public Mesa Mesa {get; private set;}
        public List<Pedido> ListaPedidos { get; private set; } = new();

        protected Comanda(){}

        public Comanda(int mesaId)
        {
            MesaId = mesaId;
            StatusComanda = StatusComandaEnum.Aberta;
            DataAbertura = DateTime.UtcNow;
       
        }

        public void ValidateDomain(StatusComandaEnum statusComanda, DateTime dataAbertura)
        {
            DomainExceptionValidation.when(dataAbertura == DateTime.MinValue, "Data Invalida");
        }


        public void AtualizarComanda(int mesaId)
        {
            MesaId = mesaId;
            
        }

        public void FecharComanda()
        {
            if (ListaPedidos.Count > 0)
            {
                if (ListaPedidos.Any(p => p.Status != StatusPedidoEnum.Finalizado))
                {
                    throw new DomainExceptionValidation("Não pode finalizar a comanda com pedidos em andamento!");
                }
            }

            StatusComanda = StatusComandaEnum.Fechada;
            DataFechamento = DateTime.UtcNow;
        }
        public void CancelarPedidosNaoFinalizados()
        {
            foreach (var pedido in ListaPedidos)
            {
                if (pedido.Status != StatusPedidoEnum.Finalizado)
                {
                    pedido.CancelarPedido();
                }
            }
        }

        public decimal TotalComanda()
        {
            return ListaPedidos.Sum(p => p.TotalPedido());
        }
    }
}
