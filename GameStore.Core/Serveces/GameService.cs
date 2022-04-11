using GameStore.Core.Models;
using GameStore.Core.Models.Manager;
using GameStore.Core.Serveces.Contracts;
using GameStore.Data.Data;
using GameStore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Core.Serveces
{
    public class GameService : IGameService
    {
        private readonly GameStoreDbContext data;

        public GameService(GameStoreDbContext _data)
        {
            this.data = _data;
        }

        public async Task Create(string name, string developer, string publisher, string description, decimal price, string imageUrl, DateTime releasedate, List<int> genreIds)
        {
            var gameData = new Game
            {
                Name = name,
                Developer = developer,
                Publisher = publisher,
                Description = description,
                Price = price,
                ImageUrl = imageUrl,
                ReleaseDate = releasedate
            };

            List<GameGenre> genreList = new List<GameGenre>();

            foreach (var id in genreIds)
            {
                var gameGenre = new GameGenre
                {
                    Game = gameData,
                    GenreId = id
                };

                genreList.Add(gameGenre);
            }

            await data.AddAsync(gameData);
            await data.AddRangeAsync(genreList);

            await data.SaveChangesAsync();
        }

        public async Task<bool> Delete(string id)
        {
            var game = await data.Games.FirstOrDefaultAsync(x => x.Id.ToString() == id);

            if (game == null)
            {
                return false;
            }

            data.Games.Remove(game);
            await data.SaveChangesAsync();

            return true;
        }

        public async Task<GamesDetailsListModel> Details(string id)
            => await data
            .Games
            .Where(x => x.Id.ToString() == id)
            .Select(x => new GamesDetailsListModel
            {
                Id = x.Id.ToString(),
                Description = x.Description,
                Developer = x.Developer,
                Publisher = x.Publisher,
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
                ReleaseDate = x.ReleaseDate,
                Genres = x.Genres.Select(x => new GenreSortingModel
                {
                    Id = x.Genre.Id,
                    GenreName = x.Genre.GenreName
                }).ToList()
            })
            .FirstAsync();

        public async Task<bool> Edit(string id, string name, string developer, string publisher, string description, string imageUrl, decimal price, DateTime releaseDate, ICollection<int> genreIds)
        {
            var game = await data.Games.FirstOrDefaultAsync(x => x.Id.ToString() == id);

            if (game == null)
            {
                return false;
            }

            List<GameGenre> oldGameGenres = await data.GameGenres.Where(x => x.GameId.ToString() == id).ToListAsync();

            game.Name = name;
            game.Developer = developer;
            game.Price = price;
            game.Publisher = publisher;
            game.Description = description;
            game.ReleaseDate = releaseDate;
            game.ImageUrl = imageUrl;

            var newGameGenres = new List<GameGenre>();

            foreach (var genreId in genreIds)
            {
                var gameGenre = new GameGenre
                {
                    GenreId = genreId,
                    Game = game
                };

                newGameGenres.Add(gameGenre);
            }

            data.GameGenres.RemoveRange(oldGameGenres);
            await data.AddRangeAsync(newGameGenres);
            await data.SaveChangesAsync();

            return true;
        }

        public async Task<GameDeleteModel> GetGameForDelete(string id)
        {
            var game = await data.Games.FirstOrDefaultAsync(x => x.Id.ToString() == id);

            var model = new GameDeleteModel
            {
                Id = id,
                Developer = game.Developer,
                Publisher = game.Publisher,
                Name = game.Name,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            };

            return model;
        }

        public async Task<OwnershipSorting> GetOwnership(string id, string userId)
        {

            if (await data.OwnedGames.AnyAsync(x => x.UserId == userId && x.GameId.ToString() == id))
            {
                return OwnershipSorting.Bought;
            }
            else if (await data.Wishlists.AnyAsync(x => x.UserId == userId && x.GameId.ToString() == id))
            {
                return OwnershipSorting.Wishlisted;
            }
            else
            {
                return OwnershipSorting.NotBought;
            }
        }
    }
}
