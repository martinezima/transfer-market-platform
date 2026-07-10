using Microsoft.EntityFrameworkCore;
using TransferMarketPlatform.Domain.Entities;

namespace Infrastructure.Data
{
    public class TransferMarketDbContext : DbContext
    {
        public TransferMarketDbContext(DbContextOptions<TransferMarketDbContext> options)
            : base(options) { }

        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Player entity if needed
            modelBuilder.Entity<Player>(entity =>
            {
                // entity.HasNoKey(); // Specify that the Player entity has no primary key
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CurrentClub).HasMaxLength(100);
                entity.Property(e => e.TransferCost).HasColumnType("decimal(18,2)");
                // Additional configurations can be added here
                entity.ToTable("PLAYERS"); // Specify the table name if different from the entity name
            });
        }
    }
}
