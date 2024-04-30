using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Ingresso.Usuario.API.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Domain.Usuario>
    {
        public void Configure(EntityTypeBuilder<Domain.Usuario> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasColumnType("varchar(12)");

            builder.Property(c => c.Senha)
                .IsRequired()
                .HasColumnType("varchar(100)");
        }
    }
}
