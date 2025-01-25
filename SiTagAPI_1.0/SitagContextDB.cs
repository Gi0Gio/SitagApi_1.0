using Microsoft.EntityFrameworkCore;
using SiTagAPI_1._0.Models;

namespace SiTagAPI_1._0
{
    public class SitagContextDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Farm> Farms { get; set; }

        public SitagContextDB(DbContextOptions<SitagContextDB> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Farms)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.userId);
        }
    }
}
