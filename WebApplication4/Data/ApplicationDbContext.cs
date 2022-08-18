using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using WebApplication4.Models;

namespace WebApplication4.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Category> Categorys { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Image>().HasKey(pk => pk.Id);
            modelBuilder.Entity<Category>().HasKey(pk => pk.Id);

            modelBuilder.Entity<Image>().HasOne(m => m.Product).WithMany(am => am.Images).HasForeignKey(m => m.ProductId);
            modelBuilder.Entity<Category>().HasOne(m => m.Product).WithMany(am => am.Categorys).HasForeignKey(m => m.ProductId);

        }
    }

    
}