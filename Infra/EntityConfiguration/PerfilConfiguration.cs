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
    public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("perfil");

            // PK
            builder.HasKey(p => p.Id_Perfil);

            builder.Property(p => p.Id_Perfil)
                .HasColumnName("Id_Perfil")
                .HasColumnType("integer")
                .ValueGeneratedOnAdd();

            // Propriedades
            builder.Property(p => p.Nome)
                .HasColumnName("Nome")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnName("Descricao")
                .HasColumnType("varchar(255)");

            builder.Property(p => p.Ativo)
                .HasColumnName("Ativo")
                .HasColumnType("boolean")
                .IsRequired();

            // Relação Perfil 1:N PerfilPermissao
            builder.HasMany(p => p.PerfisPermissoes)
                .WithOne(pp => pp.Perfil)
                .HasForeignKey(pp => pp.PerfilId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relação Perfil 1:N UsuarioPerfil
            builder.HasMany(p => p.UsuariosPerfis)
                .WithOne(up => up.Perfil)
                .HasForeignKey(up => up.PerfilId)
                .OnDelete(DeleteBehavior.Restrict);





        }
    }
}
