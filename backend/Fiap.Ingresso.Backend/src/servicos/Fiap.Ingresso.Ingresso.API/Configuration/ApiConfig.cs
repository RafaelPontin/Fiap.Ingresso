using Fiap.Ingresso.Ingresso.API.Data;
using Fiap.Ingresso.Ingresso.API.Infra;
using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Fiap.Ingresso.Ingresso.API.Services;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Ingresso.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IngressoContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IngressoContext>();
        services.AddScoped<IIngressosDoEventoRepository, IngressosDoEventoRepository>();
        services.AddScoped<IIngressosDoEventoService, IngressosDoEventoService>();
        services.AddScoped<IIngressoRepository, IngressoRepository>();
        services.AddScoped<IIngressoService, IngressoService>();

        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddPolicy("Total",
                builder =>
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
        });
    }

    public static void UseApiConfiguration(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("Total");

        app.MapControllers();
    }

}
