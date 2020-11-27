using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    /**
     * ADD FIRST (name: init) MIGRATION: 
     * dotnet ef migrations add init -s ../ConsoleApp
     */
    public class SamuraiContext: DbContext
    {
     //   private readonly string ConnectionString = "Server=127.0.0.1,1433;Database=SamuraiAppData;User Id=sa;Password=Secret!123;";

        public SamuraiContext(DbContextOptions<SamuraiContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }
        public DbSet<SamuraiBattle> SamuraiBattles { get; set; }
        public DbSet<Battle> Battles { get; set; }
        public DbSet<Horse> Horses { get; set; }
        public DbSet<SamuraiBattleStat> SamuraiBattleStats { get; set; }

      
        
        

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder
        //         .UseLoggerFactory(ConsoleLoggerFactory).EnableSensitiveDataLogging()
        //         .UseSqlServer(ConnectionString);
        //     base.OnConfiguring(optionsBuilder);
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SamuraiBattle>().HasKey(s => new {s.SamuraiId, s.BattleId});
            modelBuilder.Entity<SamuraiBattleStat>().HasNoKey().ToView("SamuraiBattleStats");
        }
    }
}