using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Ingresso.Pagamento.API.Data.Mappings;

public class PagamentoMapping : IEntityTypeConfiguration<Domain.Pagamento>
{
    public void Configure(EntityTypeBuilder<Domain.Pagamento> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.IngressoId)
            .HasColumnType("varchar(50)");

        builder.Property(e => e.TipoPagamento)
               .IsRequired()
               .HasColumnType("int");

        builder.Property(e => e.ValorPagamento)
               .IsRequired()
               .HasColumnType("numeric");

        builder.Property(e => e.NumeroCartao)
               .HasColumnType("varchar(40)");

        builder.Property(e => e.NomeCartao)
              .HasColumnType("varchar(60)");

        builder.Property(e => e.VencimentoCartao)
             .HasColumnType("varchar(5)");

        builder.Property(e => e.CodigoVerificador)
             .HasColumnType("varchar(3)");

        builder.Property(e => e.LinhaDigitavel)
            .HasColumnType("varchar(150)");

        builder.Property(e => e.DataPagamento)
               .IsRequired()
               .HasColumnType("Date");


        builder.Property(e => e.PagamentoValido)
               .IsRequired()
               .HasColumnType("int");

    }
}
