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
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {

        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.HasKey(i => i.Id_ItemPedido); //PK

            builder.Property(i => i.Id_ItemPedido)
                       .HasColumnName("Id_ItemPedido")
                       .HasColumnType("integer")
                       .ValueGeneratedOnAdd();

            //propiedades
            builder.Property(i => i.Nome)
                       .HasColumnName("Nome")
                       .HasColumnType("varchar(150)")
                        .IsRequired();

            builder.Property(i => i.Quantidade)
                .HasColumnName("Quantidade")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(i => i.ValorUnitario)
                .HasColumnName("ValorUnitario")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(i => i.Status)
                .HasColumnName("Status")
                .HasColumnType("integer")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(i => i.Observacao)
                .HasColumnName("Observacao")
                .HasColumnType("varchar(500)")
                .IsRequired(false);

            builder.Property(i => i.DataCriacao)
                .HasColumnName("DataCriacao")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(i => i.DataAtualizacao)
                .HasColumnName("DataAtualizacao")
                .HasColumnType("timestamptz")
                .IsRequired(false);


            // Relação com pedido N:1
            builder.HasOne(i => i.Pedido)
                .WithMany(p => p.ListaItemPedidos)
                .HasForeignKey(i => i.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            //  Relação com cardapio N:1
            builder.HasOne(i => i.ItemCardapio)
                .WithMany()
                .HasForeignKey(i => i.ItemCardapioId)
                .OnDelete(DeleteBehavior.Restrict);

            //Relação com Setor de N:1
            builder.HasOne(p => p.SetorProducao)
                .WithMany(s => s.Pedidos)
                .HasForeignKey(p => p.SetorProducaoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
