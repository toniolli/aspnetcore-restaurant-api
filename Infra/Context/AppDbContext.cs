using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Context
{

    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // DbSets
        public DbSet<Mesa> Mesas {get; set;}
        public DbSet<Comanda> Comandas {get; set;}
        public DbSet<Pedido> Pedidos {get; set;}
        public DbSet<ItemPedido> ItemPedidos {get; set;}
        public DbSet<Cardapio> Cardapios {get; set;}
        public DbSet<ItemCardapio> ItemCardapios {get; set;}
        public DbSet<SetorProducao> SetorProducaos {get; set;}

        //permissoes e autenticação
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Permissao> Permissoes { get; set; }
        public DbSet<PerfilPermissao> PerfilPermissoes { get; set; }
        public DbSet<UsuarioPerfil> UsuariosPerfis { get; set; }

        // Configuração do modelo
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Aplica todas as EntityConfigurations automaticamente
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);



            // STRING (PostgreSQL)
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                if (property.GetMaxLength() == null)
                {
                    property.SetColumnType("text");
                }
                else
                {
                    property.SetColumnType($"varchar({property.GetMaxLength()})");
                }
            }

            // DECIMAL
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
            {
                property.SetColumnType("numeric(18,2)");
            }
        }
    }

}
