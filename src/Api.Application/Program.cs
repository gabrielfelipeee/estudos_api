using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Api.Application
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();

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

                // Adicionar o botão de Authorize
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Entre com o token Jwt",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                             {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, new List<string>()
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
        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Donfigurações de dependências
            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);



            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);


            // Configuração da Autenticação
            services.AddAuthentication(authOptions =>
            {
                // Define o esquema de autenticação padrão como JWT
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                // Define o esquema de desafio padrão como JWT, será usado quando a aplicação precisar desafiar (pedir autenticação do usuário) 
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;

                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true; // Verifica se a chave usada é válida
                paramsValidation.ValidateLifetime = true; // Verifica se o token ainda está dentro do tempo  de validação
                paramsValidation.ClockSkew = TimeSpan.Zero; // Não há margem de tolerância adicional
            });

            // Configuração da Autorização
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder() // Define uma política chamada Bearer
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme) // Especifica o esquema que a autenticação deve usar, nesse caso o JwtBearer
                .RequireAuthenticatedUser().Build()); // Apenas usuários autenticados podem fazer requisições 
            });


            // Configurações  autoMapper
            var config = new AutoMapper.MapperConfiguration(config =>
            {
                config.AddProfile(new DtoToModelProfile());
                config.AddProfile(new EntityToDtoProfile());
                config.AddProfile(new ModelToEntityProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
