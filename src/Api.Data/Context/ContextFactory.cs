using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            // Usado para criar as migrações
            var connectionString = "Server=localhost;Port=3306;Database=dbApi;Uid=root;Pwd=14589632";
            var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 35));

            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();

            optionsBuilder.UseMySql(connectionString, mySqlServerVersion);

            return new MyContext(optionsBuilder.Options);

        }
    }
}
