using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Context
{
    public class MyContext : DbContext
    {

        public DbSet<UserEntity> Users { get; set; } // Representação da tabela Users no DB

        // Construtor que recebe opções do DbContext (opções configuradas em Program.cs)
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }

        // Esse método usa as configurações padrão fornecidas pelo DbContext. É usado para configurar o mapeamento das entidades
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
