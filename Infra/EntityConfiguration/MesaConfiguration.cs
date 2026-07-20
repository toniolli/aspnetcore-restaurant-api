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
    public class MesaConfiguration : IEntityTypeConfiguration<Mesa>
    {

        public void Configure(EntityTypeBuilder<Mesa> builder)
        {
            builder.ToTable("mesa");
            builder.HasKey(p => p.Id_Mesa);//PK

            builder.Property(p => p.Id_Mesa)
                .HasColumnName("Id_Mesa")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.NumeroMesa)
                .HasColumnName("NumeroMesa")
                .HasColumnType("integer")
                .IsRequired();

            builder.Property(p => p.StatusMesa)
                .HasColumnName("status")
                .HasColumnType("integer")
                .HasConversion<int>()
                .IsRequired();

            //Relacionamento com comanda 1:N

            builder.HasMany(p => p.Comandas)
                .WithOne(c => c.Mesa)
                .HasForeignKey(c => c.MesaId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
