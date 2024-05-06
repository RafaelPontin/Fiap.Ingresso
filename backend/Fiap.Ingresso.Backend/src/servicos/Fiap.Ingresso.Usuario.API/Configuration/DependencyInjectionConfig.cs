using Fiap.Ingresso.Usuario.API.Data;
using Fiap.Ingresso.Usuario.API.Infra.Repository;
using Fiap.Ingresso.Usuario.API.Services;
using Fiap.Ingresso.Usuario.API.Services.Contracts;

namespace Fiap.Ingresso.Usuario.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUsuarioService, UsuarioService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            
            services.AddScoped<UsuarioContext>();

        }
    }
}
