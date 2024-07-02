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
            
            var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 35));
            serviceCollection.AddDbContext<MyContext>(options =>
             options.UseMySql("Server=localhost;Port=3306;Database=dbApi;Uid=root;Pwd=14589632", mySqlServerVersion)
    );
        }
    }
}
