using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ItemCardapio
    {
        public int Id_ItemCardapio {get; private set;}
        public int CardapioId {get; private set;}
        public string Nome {get; private set;}
        public string Descricao {get; private set;}
        public decimal Preco {get; private set;}
        public bool Disponivel {get; private set;}
        public CategoriaEnum Categoria { get; private set;}

        public Cardapio Cardapio {get; private set;}

        protected ItemCardapio() { }

        public ItemCardapio(string nome, string descricao, decimal preco, CategoriaEnum categoria)
        {
            ValidateDomain(nome, descricao, preco, categoria);

            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Categoria = categoria;

            Disponivel = true;
        }

        private void ValidateDomain(string nome, string descricao, decimal preco, CategoriaEnum categoria)
        {
            DomainExceptionValidation.when(string.IsNullOrWhiteSpace(nome), "Nome inválido");
            DomainExceptionValidation.when(string.IsNullOrWhiteSpace(descricao), "Descrição inválida");
            DomainExceptionValidation.when(preco <= 0, "Preço deve ser maior que zero");
        }


        public void AtualizarItemCardapio(string nome, string descricao,decimal preco,CategoriaEnum categoroia)
        {
            
            Nome = nome;
            Descricao  = descricao;
            Preco = preco;
            Categoria = categoroia;
        }


        public void AlterarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new DomainExceptionValidation("Nome inválido");

            Nome = novoNome;
        }

        public void AlterarDescricao(string novaDescricao)
        {
            if (string.IsNullOrWhiteSpace(novaDescricao))
                throw new DomainExceptionValidation("Descrição inválida");

            Descricao = novaDescricao;
        }

        public void AlterarPreco(decimal novoPreco)
        {
            if (novoPreco <= 0)
                throw new DomainExceptionValidation("Preço deve ser maior que zero");

            Preco = novoPreco;
        }

        public void AlterarCategoria(CategoriaEnum novaCategoria)
        {
            Categoria = novaCategoria;
        }

        public void Ativar()
        {
            Disponivel = true;
        }

        public void Desativar()
        {
            Disponivel = false;
        }
    }

}
