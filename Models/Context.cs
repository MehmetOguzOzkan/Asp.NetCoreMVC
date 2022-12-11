using Microsoft.EntityFrameworkCore;

namespace ProjeDenemesi1.Models
{
    public class Context : DbContext
    {
        public DbSet<Food> Foods { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=Project;Trusted_Connection=True");
        }
    }
}
