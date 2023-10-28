using Microsoft.OpenApi.Models;

namespace Ponta.Identidade.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Ponta - Identidade API",
                    Description = "Desafio: Ponta Tarefas API.",
                    Contact = new OpenApiContact() { Name = "Thiago Moreira de Souza Arrais", Email = "thiagomds.scientist@gmail.com" }
                });

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            return app;
        }
    }
}
