using Ponta.Tarefas.Domain.Interfaces;
using Ponta.Tarefas.Domain.Notificacoes;
using Ponta.Tarefas.Domain.Services;
using Ponta.Tarefas.Infra.Data;
using Ponta.Tarefas.Infra.Data.Repository;

namespace Ponta.Tarefas.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            // Domain
            services.AddScoped<ITarefaRepository, TarefaRepository>();

            // Domain Service
            services.AddScoped<ITarefaService, TarefaService>();

            // Context
            services.AddScoped<TarefaContext>();

            // Notifições
            services.AddScoped<INotificador, Notificador>();

        }
    }
}
