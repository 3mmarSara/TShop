using Microsoft.EntityFrameworkCore;
using TShop.API.Models;

namespace TShop.API.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Category> Categories { get; set; }

    }

}
