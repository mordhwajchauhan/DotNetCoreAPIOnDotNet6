using JWTToken_API.Model;
using Microsoft.EntityFrameworkCore;

namespace JWTToken_API.DatabaseContext
{
    public class MyDBContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "JWTAuthDB");
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TodoModel> Todos { get; set; }
    }
}
