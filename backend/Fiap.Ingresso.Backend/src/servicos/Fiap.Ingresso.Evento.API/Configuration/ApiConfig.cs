using Fiap.Ingresso.Evento.API.Data;
using Fiap.Ingresso.Evento.API.Data.Seed;
using Fiap.Ingresso.Evento.API.Infra;
using Fiap.Ingresso.Evento.API.Services;
using Fiap.Ingresso.Evento.API.Services.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Fiap.Ingresso.Evento.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<EventoContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<EventoContext>();
        services.AddScoped<IEventoRepository, EventoRepository>();
        services.AddScoped<IEventoService, EventoService>();

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
        app.MigrateDatabase();
    }

    public static void MigrateDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<EventoContext>();

            dbContext.Database.Migrate();

            EventoSeed.Seed(dbContext);
        }
    }

}
