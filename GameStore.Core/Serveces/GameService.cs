using GameStore.Core.Models;
using GameStore.Core.Models.Manager;
using GameStore.Core.Serveces.Contracts;
using GameStore.Data.Data;
using GameStore.Data.Models;

namespace GameStore.Core.Serveces
{
    public class GameService : IGameService
    {
        private readonly GameStoreDbContext data;

        public GameService(GameStoreDbContext _data)
        {
            this.data = _data;
        }

        public void Create(string name, string developer, string publisher, string description, decimal price, string imageUrl, DateTime releasedate, List<int> genreIds)
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

            data.Add(gameData);
            data.AddRange(genreList);

            data.SaveChanges();
        }

        public bool Delete(string id)
        {
            var game = data.Games.FirstOrDefault(x => x.Id.ToString() == id);

            if (game == null)
            {
                return false;
            }

            data.Games.Remove(game);
            data.SaveChanges();

            return true;
        }

        public GamesDetailsListModel Details(string id)
            => data
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
            .First();

        public bool Edit(string id, string name, string developer, string publisher, string description, string imageUrl, decimal price, DateTime releaseDate, ICollection<int> genreIds)
        {
            var game = data.Games.FirstOrDefault(x => x.Id.ToString() == id);

            if (game == null)
            {
                return false;
            }

            List<GameGenre> oldGameGenres = data.GameGenres.Where(x => x.GameId.ToString() == id).ToList();

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
            data.AddRange(newGameGenres);
            data.SaveChanges();

            return true;
        }

        public GameDeleteModel GetGameForDelete(string id)
        {
            var game = data.Games.FirstOrDefault(x => x.Id.ToString() == id);

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

        public List<BaseGameModel> GetIndexGames()
            => data.Games
            .OrderByDescending(x => x.Id)
            .Select(x => new BaseGameModel
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Image = x.ImageUrl
            })
            .Take(3)
            .ToList();

        public OwnershipSorting GetOwnership(string id, string userId)
        {

            if (data.OwnedGames.Any(x => x.UserId == userId && x.GameId.ToString() == id))
            {
                return OwnershipSorting.Bought;
            }
            else if (data.Wishlists.Any(x => x.UserId == userId && x.GameId.ToString() == id))
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
