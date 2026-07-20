using Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SetorProducao
    {
        public int Id_SetorProducao { get; private set; }
        public string Nome { get; private set; }
        public bool Ativo { get; private set; } = true;
        public List<ItemPedido> Pedidos { get; private set; } = new();

        protected SetorProducao() { }

        public SetorProducao(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainExceptionValidation("Nome do setor é obrigatório");

            Nome = nome;
        }

        public void AtualizarSetorProducao(string nome)
        {
            Nome = nome;
        }


        public void AlterarNome(string novoNome)
        {
            if (string.IsNullOrWhiteSpace(novoNome))
                throw new DomainExceptionValidation("Nome inválido");

            Nome = novoNome;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }
    }
}
