using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data
{
    /**
     * ADD FIRST (name: init) MIGRATION: 
     * dotnet ef migrations add init -s ../ConsoleApp
     */
    public class SamuraiContext: DbContext
    {
        private readonly string ConnectionString = "Server=127.0.0.1,1433;Database=SamuraiAppData;User Id=sa;Password=Secret!123;";
        
        public DbSet<Samurai> Samurais { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Clan> Clans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}