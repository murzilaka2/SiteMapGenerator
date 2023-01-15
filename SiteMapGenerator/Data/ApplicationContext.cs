using Microsoft.EntityFrameworkCore;
using SiteMapGenerator.Interfaces;
using SiteMapGenerator.Models;

namespace SiteMapGenerator.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> context) : base(context)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                    new Product { Id = Guid.NewGuid(), Name = "Apple", Category = "Fruits"},
                    new Product { Id = Guid.NewGuid(), Name = "Orange", Category = "Fruits" },
                    new Product { Id = Guid.NewGuid(), Name = "Cherry", Category = "Fruits" }
            );
        }
    }

}
