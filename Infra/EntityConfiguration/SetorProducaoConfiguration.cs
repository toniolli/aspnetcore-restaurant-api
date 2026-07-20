using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfiguration
{
    public class SetorProducaoConfiguration : IEntityTypeConfiguration<SetorProducao>
    {
        public void Configure(EntityTypeBuilder<SetorProducao> builder)
        {

            builder.ToTable("setorProducao");

            builder.HasKey(s => s.Id_SetorProducao); // pk

            builder.Property(s => s.Id_SetorProducao)
                .HasColumnName("Id_SetorProducao")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.Property(s => s.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(s => s.Ativo)
                   .HasColumnName("Ativo")
                   .HasColumnType("boolean")
                   .IsRequired();

            //Relação com pedido 1:N

            builder.HasMany(s => s.Pedidos)
                        .WithOne(p => p.SetorProducao)
                        .HasForeignKey(p => p.SetorProducaoId)
                        .OnDelete(DeleteBehavior.Restrict);



        }
    }
}
