using PloomesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PloomesAPI.Data
{
    public class AppDataContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=app.db;Cache=Shared");
        }
    }
}