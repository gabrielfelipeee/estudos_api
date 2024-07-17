using Api.Data.Context;
using Api.Data.Implementation;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();
            /*
             var databaseType = Environment.GetEnvironmentVariable("DATABASE");
             var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");
             if (string.IsNullOrEmpty(databaseType) || string.IsNullOrEmpty(connectionString))
             {
                throw new ArgumentException(nameof(connectionString), "A(s) variável(is) de ambiente DB_CONNECTION ou DATABASE não está(ão) definida(s).");
             }
             else if (databaseType.ToLower() == "MYSQL".ToLower())
             {
                serviceCollection.AddDbContext<MyContext>(options =>
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                );
             }
             serviceCollection.AddDbContext<MyContext>(options =>
                options.UseNpgsql(connectionString)
             );
            */

            string database = "postgre";
            string connectionStringPostgreSQL = "Host=localhost;Port=5432;Database=dbApi;Username=postgres;Password=14589632";
            string connectionStringMySQL = "Server=localhost;Port=3306;Database=dbApi;Uid=root;Pwd=14589632@Gg";

            if (database == "mysql")
            {
                serviceCollection.AddDbContext<MyContext>(options =>
                {
                    options.UseMySql(connectionStringMySQL, ServerVersion.AutoDetect(connectionStringMySQL));
                });
            }
            serviceCollection.AddDbContext<MyContext>(options =>
            {
                options.UseNpgsql(connectionStringPostgreSQL);
            });
        }
    }
}
