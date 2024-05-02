using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fiap.Ingresso.Evento.API.Data.Mappings;

public class EventoMapping : IEntityTypeConfiguration<Domain.Evento>
{
    public void Configure(EntityTypeBuilder<Domain.Evento> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

        builder.Property(c => c.DataInicio)
               .IsRequired()
               .HasColumnType("Date");

        builder.Property(c => c.DataFim)
               .IsRequired()
               .HasColumnType("Date");

        builder.Property(c => c.DataEvento)
               .IsRequired()
               .HasColumnType("Date");

        builder.Property(c => c.PublicoMaximo)
               .IsRequired()
               .HasColumnType("int");

        builder.Property(c => c.Ativo)
               .IsRequired()
               .HasColumnType("int");

        builder.Property(e => e.Logradouro)
               .IsRequired()
                .HasColumnType("varchar(300)");

        builder.Property(e => e.Numero)
               .IsRequired()
                .HasColumnType("varchar(20)");


        builder.Property(e => e.Cidade)
               .IsRequired()
                .HasColumnType("varchar(50)");


        builder.Property(e => e.Bairro)
              .IsRequired()
               .HasColumnType("varchar(50)");


        builder.Property(e => e.Cep)
             .IsRequired()
              .HasColumnType("varchar(20)");

        builder.Property(e => e.Descricao)
             .HasColumnType("varchar(1000)");

        builder.Property(e => e.SiteEvento)
             .HasColumnType("varchar(300)");

        builder.Property(e => e.Valor)
            .IsRequired()
             .HasColumnType("numeric");
    }
}
