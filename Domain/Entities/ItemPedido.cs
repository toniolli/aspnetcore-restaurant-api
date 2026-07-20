using Domain.Enum;
using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public  class ItemPedido
    {
        public int Id_ItemPedido {get; private set;}
        public int PedidoId {get; private set;}
        public int ItemCardapioId {get; private set;}
        public int SetorProducaoId { get; private set; }
        public string? Nome {get; private set;}
        public int Quantidade {get; private set;}
        public decimal? ValorUnitario {get; private set;}
        public StatusItemPedidoEnum Status {get; private set;}
        public string? Observacao { get; private set;}
        public DateTime DataCriacao {get; private set;}
        public DateTime? DataAtualizacao {get; private set;}
        //navegação

        public Pedido Pedido {get; private set;}
        public ItemCardapio ItemCardapio {get; private set;}
        public SetorProducao SetorProducao { get; private set; }
        protected ItemPedido(){}

        public ItemPedido(int itemCardapioId,int setorProducaoId,string nome,decimal valorUnitario,int quantidade,string? observacao)
        {

            ValidateDomain(itemCardapioId, quantidade, setorProducaoId);

            ItemCardapioId = itemCardapioId;
            SetorProducaoId = setorProducaoId;
            Nome = nome;
            ValorUnitario = valorUnitario;
            Quantidade = quantidade;
            Observacao = observacao;
            Status = StatusItemPedidoEnum.Pendente;
            DataCriacao = DateTime.UtcNow;
            
            
        }

        public void ValidateDomain(int itemCardapioId,  int quantidade,int setorProducaoId)
        {
            DomainExceptionValidation.when(itemCardapioId <= 0, "Cardapio não pode ser vazio!");
            DomainExceptionValidation.when(setorProducaoId <= 0, "Cardapio não pode ser vazio!");
            DomainExceptionValidation.when(quantidade <= 0, "Quantidade não pode vim vazio!");
           
        }

        
        public void AdicionarQuantidade(int quantidade)
        {
            if (quantidade <= 0)
                throw new DomainExceptionValidation("Quantidade não pode ser negativa");

            if (Status == StatusItemPedidoEnum.Finalizado || Status == StatusItemPedidoEnum.Cancelado)
                throw new DomainExceptionValidation("Não é possível alterar um item finalizado ou cancelado");

            Quantidade += quantidade;
        }

        public void AlterarQuantidade(int novaQuantidade)
        {
            if (novaQuantidade <= 0)
                throw new DomainExceptionValidation("Quantidade não pode ser negativa");

            if (Status == StatusItemPedidoEnum.Finalizado || Status == StatusItemPedidoEnum.Cancelado)
                throw new DomainExceptionValidation("Não é possível alterar um item finalizado ou cancelado");

            Quantidade = novaQuantidade;
        }

        public void IniciarPreparo()
        {
            if (Status != StatusItemPedidoEnum.Pendente)
                throw new DomainExceptionValidation("Só é possível iniciar preparo se estiver pendente");

            Status = StatusItemPedidoEnum.EmPreparo;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void MarcarComoPronto()
        {
            if (Status != StatusItemPedidoEnum.EmPreparo)
                throw new DomainExceptionValidation("Só é possível marcar como pronto se estiver em preparo");

            Status = StatusItemPedidoEnum.Pronto;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Finalizar()
        {
            if (Status != StatusItemPedidoEnum.Pronto)
                throw new DomainExceptionValidation("Só é possível finalizar se estiver pronto");

            Status = StatusItemPedidoEnum.Finalizado;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void Cancelar()
        {
            if (Status != StatusItemPedidoEnum.Pendente)
                throw new DomainExceptionValidation("Só é possível cancelar se estiver pendente");

            Status = StatusItemPedidoEnum.Cancelado;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void AlterarObservacao(string? novaObservacao)
        {
            //pode vim null mas não vazio
            Observacao = string.IsNullOrWhiteSpace(novaObservacao)
                  ? null
                  : novaObservacao;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void AtualizarItemPedido( int itemCardapioId, int setorProducaoId,int quntidade,string observacao)
        {
            ItemCardapioId = itemCardapioId;
            SetorProducaoId = setorProducaoId;
            Quantidade = quntidade;
            Observacao = observacao;
        }

        public decimal ObterTotal()
        {
            return (decimal)(ValorUnitario * Quantidade);
        }

    }
}
