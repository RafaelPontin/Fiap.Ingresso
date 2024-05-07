using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Ingresso.API.Data;

public class IngressoContext : DbContext
{
    public IngressoContext(DbContextOptions<IngressoContext> options) : base(options)
    {
    }

    public DbSet<Domain.IngressosDoEvento> IngressosDosEventos { get; set; }
    public DbSet<Domain.Ingresso> Ingressos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IngressoContext).Assembly);
    }
}
