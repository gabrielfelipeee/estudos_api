using Api.CrossCutting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Api.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();

            ConfigureServices(builder.Services, builder.Configuration);

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
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddSwaggerGen(c =>
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
                }
                );
            });
        }
    }
}
