using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfiguration
{
    public class ItemCardapioConfiguration : IEntityTypeConfiguration<ItemCardapio>
    {
        public void Configure(EntityTypeBuilder<ItemCardapio> builder)
        {

            builder.ToTable("item_cardapio");

            builder.HasKey(i => i.Id_ItemCardapio); //PK

            builder.Property(i => i.Id_ItemCardapio)
                .HasColumnName("Id_ItemCardapio")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(i => i.Descricao)
                .HasColumnName("Descricao")
                .HasColumnType("varchar(150)");
               

            builder.Property(i => i.Preco)
                .HasColumnName("Preco")
                .HasColumnType("numeric(10,2)")
                .IsRequired();

            builder.Property(i => i.Disponivel)
                .HasColumnName("Disponivel")
                .HasColumnType("boolean")
                .IsRequired();

            builder.Property(i => i.Categoria)
                .HasColumnName("Categoria")
                .HasColumnType("integer")
                .HasConversion<int>()
                .IsRequired();

            //Relação com cardapio N:1

            builder.HasOne(i => i.Cardapio)
                .WithMany(i => i.Itens)
                .HasForeignKey(i => i.CardapioId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
