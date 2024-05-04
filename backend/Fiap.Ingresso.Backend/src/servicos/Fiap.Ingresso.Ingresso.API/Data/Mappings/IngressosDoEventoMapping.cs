using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Ingresso.Ingresso.API.Data.Mappings;

public class IngressosDoEventoMapping : IEntityTypeConfiguration<Domain.IngressosDoEvento>
{
    public void Configure(EntityTypeBuilder<Domain.IngressosDoEvento> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).IsRequired();
        builder.Property(e => e.EventoId).IsRequired();
        builder.Property(e => e.Total).HasColumnType("int").IsRequired();
        builder.Property(e => e.Disponiveis).HasColumnType("int").IsRequired();
        builder.Property(e => e.Preco).HasColumnType("decimal").IsRequired();
        builder.Property(e => e.DataInicio).HasColumnType("datetime2").IsRequired();
        builder.Property(e => e.DataFim).HasColumnType("datetime2").IsRequired();
        builder.Property(e => e.Ativo).HasColumnType("bit").IsRequired();
        builder.HasMany(e => e.IngressosVendidos).WithOne().HasForeignKey("IngressosDoEventoId");
    }
}
