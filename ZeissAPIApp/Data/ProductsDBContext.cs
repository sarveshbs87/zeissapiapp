using Microsoft.EntityFrameworkCore;
using ZeissAPIApp.Models;

namespace ZeissAPIApp.Data
{
    public class ProductsDBContext:DbContext
    {
        public ProductsDBContext(DbContextOptions<ProductsDBContext> options) : base(options) { }
        
        public DbSet<Products> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Products>()
                .HasKey(p => p.Id);  

            modelBuilder.Entity<Products>()
        .Property(p => p.Price)
        .HasColumnType("decimal(10,4)");
        }
    }
    
}
