using Fora.Challenge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Fora.Challenge.Persistence
{
    public class CompanyDataDbContext : DbContext
    {
        public CompanyDataDbContext(DbContextOptions<CompanyDataDbContext> options) : base(options)
        { }

        public DbSet<Company> Companies { get; set; }
        public DbSet<NetIncomeLossData> NetIncomeLossData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Company
            modelBuilder.Entity<Company>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Cik)
                .HasDatabaseName("IX_Company_Cik")
                .IsUnique(true);

            modelBuilder.Entity<Company>()
                .HasMany(c => c.NetIncomeLossData)
                .WithOne(i => i.Company)
                .HasForeignKey(i => i.CompanyId);

            // Configure NetIncomeLossData
            modelBuilder.Entity<NetIncomeLossData>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<NetIncomeLossData>()
                .Property(p => p.Val)
                .HasPrecision(18, 2);
        }
    }
}
