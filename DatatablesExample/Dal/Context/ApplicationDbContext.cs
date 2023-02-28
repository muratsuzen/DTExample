using Entities;
using Microsoft.EntityFrameworkCore;

namespace Dal.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Name = "Product 1", Code = "P001", Id = Guid.NewGuid() },
                new Product { Name = "Product 2", Code = "P002", Id = Guid.NewGuid() },
                new Product { Name = "Product 3", Code = "P003", Id = Guid.NewGuid() }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
