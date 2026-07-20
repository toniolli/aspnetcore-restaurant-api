using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class PermissaoConfiguration : IEntityTypeConfiguration<Permissao>
    {
        public void Configure(
            EntityTypeBuilder<Permissao> builder)
        {
            builder.ToTable("permissao");

            // PK
            builder.HasKey(p => p.Id_Permissao);

            builder.Property(p => p.Id_Permissao)
                .HasColumnName("Id_Permissao")
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

            // Relação Permissao 1:N PerfilPermissao
            builder.HasMany(p => p.PerfisPermissoes)
                .WithOne(pp => pp.Permissao)
                .HasForeignKey(pp => pp.PermissaoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}