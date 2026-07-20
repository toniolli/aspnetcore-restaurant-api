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
    public class ComandaConfiguration : IEntityTypeConfiguration<Comanda>
    {

        public void Configure(EntityTypeBuilder<Comanda> builder)
        {

            builder.ToTable("comanda");
            
            builder.HasKey(c => c.Id_Comanda); //PK

            builder.Property(c => c.Id_Comanda)
                .HasColumnName("Id_Comanda")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            //Propiedades

            builder.Property(c => c.StatusComanda)
                .HasColumnName("StatusComanda")
                .HasColumnType("integer")
                .HasConversion<int>()
                .IsRequired();

            builder.Property(c => c.DataAbertura)
                .HasColumnName("DataAbertura")
                .HasColumnType("timestamptz")
                .IsRequired();

            builder.Property(c => c.DataFechamento)
               .HasColumnName("DataFechamento")
               .HasColumnType("timestamptz");
               
            //Relação com Mesa 1:N
            builder.HasOne(c => c.Mesa)
                .WithMany(m => m.Comandas)
                .HasForeignKey(c => c.MesaId)
                .OnDelete(DeleteBehavior.Restrict);
             
            //Relação 1:N pedidos
            builder.HasMany(c => c.ListaPedidos)
                .WithOne(p => p.Comanda)
                .HasForeignKey(p => p.ComandaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
