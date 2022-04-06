using GameStore.Core.Models;
using GameStore.Core.Models.Manager;
using GameStore.Core.Serveces.Contracts;
using GameStore.Data.Data;
using GameStore.Data.Models;

namespace GameStore.Core.Serveces
{
    public class StoreServices : IStoreServices
    {
        private readonly GameStoreDbContext data;

        public StoreServices(GameStoreDbContext _data)
        {
            this.data = _data;
        }

        public GameServiceModel All(string searchTerm = null, int indexPage = 1, int gamesPerPage = int.MaxValue)
        {
            var gamesQuery = data.Games.ToList();

            if (!String.IsNullOrWhiteSpace(searchTerm))
            {
                gamesQuery = gamesQuery.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            var totalGames = gamesQuery.Count();
            var games = gamesQuery
                .Skip((indexPage - 1) * gamesPerPage)
                .Take(gamesPerPage)
                .Select(x => new StoreGamesViewModel
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price
            })
                .ToList();

            var model = new GameServiceModel()
            {
                
                TotalGames = totalGames,
                Games = games
            };

            return model;
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

        public List<StoreGamesViewModel> GetGames()
            => data.Games
            .Select(x => new StoreGamesViewModel
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                ImageUrl = x.ImageUrl,
                Price = x.Price,
            })
            .ToList();

        public IEnumerable<GenreSortingModel> GetGenres()
            => this.data.Genres
                .Select(x => new GenreSortingModel
                {
                    GenreName = x.GenreName,
                    Id = x.Id,
                })
                .ToList();

        public bool NameExists(string name)
            => data.Games
            .Any(x => x.Name == name);
    }
}
