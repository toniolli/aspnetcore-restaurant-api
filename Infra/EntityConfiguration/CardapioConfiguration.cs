using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Infra.EntityConfiguration
{
    public class CardapioConfiguration : IEntityTypeConfiguration<Cardapio>
    {
        public void Configure(EntityTypeBuilder<Cardapio> builder)
        {

            builder.ToTable("cardapio");

            builder.HasKey(c => c.Id_Cardapio); //PK

            builder.Property(c => c.Id_Cardapio)
                .HasColumnName("Id_Cardapio")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar(150)")
                .IsRequired();

            //Relação com itemCardapio 1:N

            builder.HasMany(c => c.Itens)
                .WithOne(i => i.Cardapio)
                .HasForeignKey(i => i.CardapioId)
                .OnDelete(DeleteBehavior.Cascade);
                


        }

    }
}
