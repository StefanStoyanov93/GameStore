using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedGenres(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre() { Id = 1, GenreName = "Action" },
                new Genre() { Id = 2, GenreName = "Adventure" },
                new Genre() { Id = 3, GenreName = "RPG" },
                new Genre() { Id = 4, GenreName = "Shooter" },
                new Genre() { Id = 5, GenreName = "Puzzle" },
                new Genre() { Id = 6, GenreName = "Strategy" },
                new Genre() { Id = 7, GenreName = "JRPG" },
                new Genre() { Id = 8, GenreName = "Simulation" },
                new Genre() { Id = 9, GenreName = "Sports" },
                new Genre() { Id = 10, GenreName = "Racing" },
                new Genre() { Id = 11, GenreName = "Open World" },
                new Genre() { Id = 12, GenreName = "First Person" },
                new Genre() { Id = 13, GenreName = "Horror" },
                new Genre() { Id = 14, GenreName = "Survival" },
                new Genre() { Id = 15, GenreName = "Comedy" },
                new Genre() { Id = 16, GenreName = "Fighting" },
                new Genre() { Id = 17, GenreName = "Card game" },
                new Genre() { Id = 18, GenreName = "Platformer" },
                new Genre() { Id = 19, GenreName = "Casual" },
                new Genre() { Id = 20, GenreName = "Turn-based" }
            );
        }
    }
}
