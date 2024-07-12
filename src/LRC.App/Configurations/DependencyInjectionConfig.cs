using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using LRC.Business.Notificacoes;
using LRC.Business.Servicos;
using LRC.Data.Context;
using LRC.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;

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
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            services.AddScoped<IEntregadorRepository, EntregadorRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IFormaPagamentoRepository, FormaPagamentoRepository>();
            services.AddScoped<IContaPagarRepository, ContaPagarRepository>();
            services.AddScoped<IContaReceberRepository, ContaReceberRepository>();

            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IGrupoService, GrupoService>();
            services.AddScoped<ISubGrupoService, SubGrupoService>();
            services.AddScoped<ILogAlteracaoService, LogAlteracaoService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IColaboradorService, ColaboradorService>();
            services.AddScoped<IEntregadorService, EntregadorService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IFormaPagamentoService, FormaPagamentoService>();
            services.AddScoped<IContaPagarService, ContaPagarService>();
            services.AddScoped<IContaReceberService, ContaReceberService>();
            return services;
        }
    }
}
