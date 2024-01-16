using web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public class AzureContext : IdentityDbContext<ApplicationUser>
    {
        public AzureContext(DbContextOptions<AzureContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser>? AppUser { get; set; }
        public DbSet<Worker>? Worker { get; set; }
        public DbSet<Review>? Review { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            modelBuilder.Entity<Worker>().ToTable("Worker");
            modelBuilder.Entity<Review>().ToTable("Review");
           

        }





        public DbSet<web.Models.Job>? Job { get; set; }
    }
}