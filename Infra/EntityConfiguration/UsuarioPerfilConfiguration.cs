using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Data.EntitiesConfiguration
{
    public class UsuarioPerfilConfiguration : IEntityTypeConfiguration<UsuarioPerfil>
    {
        public void Configure(
            EntityTypeBuilder<UsuarioPerfil> builder)
        {
            builder.ToTable("usuario_perfil");

            // PK composta
            builder.HasKey(up => new
            {
                up.UsuarioId,
                up.PerfilId
            });

            builder.Property(up => up.UsuarioId)
                .HasColumnName("UsuarioId")
                .HasColumnType("text");

            builder.Property(up => up.PerfilId)
                .HasColumnName("PerfilId")
                .HasColumnType("integer");

            // Perfil 1:N UsuarioPerfil
            builder.HasOne(up => up.Perfil)
                .WithMany(p => p.UsuariosPerfis)
                .HasForeignKey(up => up.PerfilId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}