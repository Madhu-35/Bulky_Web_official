using Bulky_Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky_Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        // This class would typically inherit from DbContext and include DbSet properties for your models.
        // For example:
        // public DbSet<Category> Categories { get; set; }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("YourConnectionStringHere");
        // }

        // Additional configurations and methods would go here.

        // For example, you might include methods for seeding the database or configuring entity relationships.
        // However, since this is a placeholder, no specific implementation is provided here.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            // Constructor logic, if any, would go here.
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // You can configure your model here, such as setting up relationships, constraints, etc.
            // For example, you might want to set a default value for a property or configure a composite key.
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "SciFi", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 }
            );
            
        }
    }

} // End of namespace Bulky_Web.Data
