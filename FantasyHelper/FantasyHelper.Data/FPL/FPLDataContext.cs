using FantasyHelper.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyHelper.Data.FPL
{
    public class FPLDataContext : DbContext
    {
        public FPLDataContext(DbContextOptions<FPLDataContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Gameweek> Gameweeks { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set default schema
            modelBuilder.HasDefaultSchema("FPL");

            // A team can have many players, and a player can only have one team
            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId);

            // A team can have many home fixtures, each fixture can only have one home team
            modelBuilder.Entity<Team>()
                .HasMany(t => t.HomeFixtures)
                .WithOne(f => f.HomeTeam)
                .HasForeignKey(f => f.HomeTeamId);

            // A team can have many away fixtures, each fixture can only have one away team
            modelBuilder.Entity<Team>()
                .HasMany(t => t.AwayFixtures)
                .WithOne(f => f.AwayTeam)
                .HasForeignKey(f => f.AwayTeamId);

            // A gameweek can have many fixtures, each fixture can only belong to one gameweek
            modelBuilder.Entity<Gameweek>()
                .HasMany(gw => gw.Fixtures)
                .WithOne(f => f.Gameweek)
                .HasForeignKey(f => f.GameweekId);
        }
    }
}
