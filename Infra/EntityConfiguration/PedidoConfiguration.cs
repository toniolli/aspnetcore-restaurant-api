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
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {

            builder.ToTable("pedido");

            builder.HasKey(p => p.Id_Pedido); //PK

            builder.Property(p => p.Id_Pedido)
                .HasColumnName("Id_Pedido")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            //propiedades
            builder.Property(p => p.Status)
                .HasColumnName("status")
                .HasColumnType("integer")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(p => p.DataCriacao)
                .HasColumnName("DataCriacao")
                .HasColumnType("timestamptz")
                .IsRequired();

            //Relação com Comanda N:1
            builder.HasOne(p => p.Comanda)
                .WithMany(c => c.ListaPedidos)
                .HasForeignKey(p => p.ComandaId)
                .OnDelete(DeleteBehavior.Restrict);

            //Relação com mesa N:1
            builder.HasOne(p => p.Mesa)
                .WithMany()
                .HasForeignKey(p => p.MesaId)
                .OnDelete(DeleteBehavior.Restrict);

           
            // Relação com itens de pedido de 1:N
            builder.HasMany(p => p.ListaItemPedidos)
                        .WithOne(i => i.Pedido)
                        .HasForeignKey(i => i.PedidoId)
                        .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
