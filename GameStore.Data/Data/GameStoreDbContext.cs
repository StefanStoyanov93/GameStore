using GameStore.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Data
{
    public class GameStoreDbContext : IdentityDbContext<User>
    {
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<OwnedGame> OwnedGames { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<GameGenre>().HasKey(x => new { x.GenreId, x.GameId });
            builder.Entity<OwnedGame>().HasKey(x => new { x.UserId, x.GameId });
            builder.Entity<Wishlist>().HasKey(x => new { x.UserId, x.GameId });

            base.OnModelCreating(builder);
        }
    }
}