using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using LRC.Business.Notificacoes;
using LRC.Business.Servicos;
using LRC.Data.Context;
using LRC.Data.Repository;

namespace LRC.App.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<IGrupoRepository, GrupoRepository>();
            services.AddScoped<ISubGrupoRepository, SubGrupoRepository>();
            services.AddScoped<ILogAlteracaoRepository, LogAlteracaoRepository>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IGrupoService, GrupoService>();
            services.AddScoped<ISubGrupoService, SubGrupoService>();
            services.AddScoped<ILogAlteracaoService, LogAlteracaoService>();
            return services;
        }
    }
}
