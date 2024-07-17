using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            // Usado para criar as migrações

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            string database = "postgre";
            string connectionStringPostgreSQL = "Host=localhost;Port=5432;Database=dbApi;Username=postgres;Password=14589632";
            string connectionStringMySQL = "Server=localhost;Port=3306;Database=dbApi;Uid=root;Pwd=14589632@Gg";

            if (database == "mysql")
            {
                optionsBuilder.UseMySql(connectionStringMySQL, ServerVersion.AutoDetect(connectionStringMySQL));
            }
            optionsBuilder.UseNpgsql(connectionStringPostgreSQL);

            return new MyContext(optionsBuilder.Options);

        }
    }
}
