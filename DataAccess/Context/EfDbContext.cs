using Microsoft.EntityFrameworkCore;
using Entity.Models;
namespace DataAccess
{
    public class EfDbContext : DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options):base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
