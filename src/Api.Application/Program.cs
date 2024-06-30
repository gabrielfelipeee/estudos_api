using Api.CrossCutting.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Api.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();


            // Informaações da API
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "ASP.NET CORE 8 C# | API REST com arquitetura DDD",
                    Version = "v1",
                    Description = "API REST",
                    Contact = new OpenApiContact
                    {
                        Name = "Gabriel Felipe",
                        Email = "gabrielfelipe0722@gmail.com",
                        Url = new Uri("https://github.com/gabrielfelipeee")
                    }
                });
            });


            var app = builder.Build();

            // Swagger só é habilitado em ambiente de desenvolvimento
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        private static void ConfigureServices(IServiceCollection services)
        {
            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);
        }
    }
}
