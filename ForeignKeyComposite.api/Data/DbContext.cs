using ForeignKeyComposite.api.Domain;
using Microsoft.EntityFrameworkCore;

namespace ForeignKeyComposite.api.Data
{
    public class FKCContext : DbContext
    {
        public DbSet<Entity01> Entity01 { get; set; }
        public DbSet<Entity02> Entity02 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ForeignKeyComposite");

            optionsBuilder.UseSqlServer(connectionString)
                          .LogTo(Console.WriteLine)
                          .EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity01>()
                .HasKey(x => new
                {
                    x.Key01,
                    x.Key02
                });

            modelBuilder.Entity<Entity01>()
                .HasMany(x => x.Entities02)
                .WithOne(x => x.Entity01)
                .HasForeignKey(x => new
                {
                    x.Foreign01,
                    x.Foreign02
                });

            modelBuilder.Entity<Entity02>()
                .HasKey(x => new
                {
                    x.Key01,
                    x.Foreign02,
                    x.Foreign01
                });

            modelBuilder.Entity<Entity02>()
                .Property(x => x.Key01)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();
        }
    }
}
