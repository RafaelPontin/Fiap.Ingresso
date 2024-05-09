using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Ingresso.Ingresso.API.Data.Mappings;

public class IngressoMapping : IEntityTypeConfiguration<Domain.Ingresso>
{
    public void Configure(EntityTypeBuilder<Domain.Ingresso> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired();
        builder.Property(e => e.UsuarioId).IsRequired();
        builder.Property(e => e.EventoId).IsRequired();
        builder.Property(e => e.DataVenda).HasColumnType("datetime2").IsRequired();
    }
}
