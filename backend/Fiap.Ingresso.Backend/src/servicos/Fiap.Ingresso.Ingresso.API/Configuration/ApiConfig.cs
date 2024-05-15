using Fiap.Ingresso.Ingresso.API.Data;
using Fiap.Ingresso.Ingresso.API.Infra;
using Fiap.Ingresso.Ingresso.API.Infra.Contratos;
using Fiap.Ingresso.Ingresso.API.Services;
using Fiap.Ingresso.Ingresso.API.Services.Contratos;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace Fiap.Ingresso.Ingresso.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IngressoContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddHttpClient();

        services.AddScoped<IngressoContext>();
        services.AddScoped<IValidarPagamentoService, ValidarPagamentoService>();
        services.AddScoped<IIngressosDoEventoRepository, IngressosDoEventoRepository>();
        services.AddScoped<IIngressosDoEventoService, IngressosDoEventoService>();
        services.AddScoped<IIngressoRepository, IngressoRepository>();
        services.AddScoped<IIngressoService, IngressoService>();

        services.AddControllers();
        services.AddSwaggerGen();
        services.AddSwagger();
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
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MigrateDatabase();
    }

    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
        });
    }

    public static void MigrateDatabase(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<IngressoContext>();

            dbContext.Database.Migrate();
        }
    }

}
