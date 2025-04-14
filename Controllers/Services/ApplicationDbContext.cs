using Microsoft.EntityFrameworkCore;
using SellerMVC.Models;


namespace sellerMVC.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {
            
        }


        public DbSet<sellers> sellers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.Entity<sellers>().HasKey(s => s.sellerbp);
            base.OnModelCreating(modelBuilder);
        }
    }


}
