using Domain.Validation;

namespace Domain.Entities
{
    public class Permissao
    {
        public int Id_Permissao {get; private set;}
        public string Nome {get; private set;}
        public string? Descricao {get; private set;}
        public bool Ativo {get; private set;}
        public ICollection<PerfilPermissao> PerfisPermissoes {get; private set;}
        protected Permissao() { }

        public Permissao(string nome, string? descricao)
        {
            ValidateDomain(nome);

            Nome = nome;
            Descricao = descricao;
            Ativo = true;
        }

        private void ValidateDomain(string nome)
        {
            DomainExceptionValidation.when(string.IsNullOrWhiteSpace(nome), "Nome da permissão é obrigatório");
        }

        public void Atualizar(string nome, string? descricao)
        {
            ValidateDomain(nome);

            Nome = nome;
            Descricao = descricao;
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