using Domain.Validation;
using Domain.Enum;
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
    public class Perfil
    {
        public int Id_Perfil {get; private set;}
        public string Nome {get; private set;}
        public string? Descricao {get; private set;}
        public bool Ativo {get; private set;}
        public ICollection<UsuarioPerfil> UsuariosPerfis { get; private set; } = new List<UsuarioPerfil>();
        public ICollection<PerfilPermissao> PerfisPermissoes { get; private set; } = new List<PerfilPermissao>();

        protected Perfil() { }

        public Perfil(string nome, string? descricao)
        {
            ValidateDomain(nome);

            Nome = nome;
            Descricao = descricao;
            Ativo = true;
        }

        private void ValidateDomain(string nome)
        {
            DomainExceptionValidation.when(string.IsNullOrWhiteSpace(nome), "Nome do perfil é obrigatório");
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