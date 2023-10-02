using Microsoft.EntityFrameworkCore;
using WEB_153504_SIVY.Domain.Entities;

namespace WEB_153504_SIVY.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CarBodyType> CarBodyTypes { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
