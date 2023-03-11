using Microsoft.EntityFrameworkCore;
using WebApplicationmvc.Models;
using WebApplicationmvc.Models.ImageView;

namespace WebApplicationmvc.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        {
        }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Image> Images { get; set; }
        public DbSet<Check> Checks { get; set; }
    }
}
