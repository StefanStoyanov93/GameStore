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

        public GameServiceManagerModel All(string searchTerm = null, int indexPage = 1, int gamesPerPage = int.MaxValue)
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
                .Select(x => new BaseGameModel
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Image = x.ImageUrl,
            })
                .ToList();

            var model = new GameServiceManagerModel()
            {
                
                TotalGames = totalGames,
                Games = games
            };

            return model;
        }

        public GameServiceModel BrowseAll(string searchTerm = null, string genreName = null, GameSorting sorting = GameSorting.ReleaseDate, int indexPage = 1, int gamesPerPage = 0)
        {
            var gamesQuery = data.Games.ToList();

            if (!String.IsNullOrWhiteSpace(searchTerm))
            {
                gamesQuery = gamesQuery.Where(x => 
                    x.Name.ToLower().Contains(searchTerm.ToLower()) || 
                    x.Developer.ToLower().Contains(searchTerm.ToLower()) || 
                    x.Publisher.ToLower().Contains(searchTerm.ToLower()))
                    .ToList();
            }

            if(!string.IsNullOrWhiteSpace(genreName))
            {
                gamesQuery = gamesQuery.FindAll(x => x.Genres.All(x => x.Genre.GenreName == genreName)).ToList();
            }

            if (sorting == GameSorting.ReleaseDate)
            {
                gamesQuery = gamesQuery.OrderByDescending(c => c.ReleaseDate).ToList();
            }
            else if (sorting == GameSorting.PriceHigh)
            {
                gamesQuery = gamesQuery.OrderByDescending(c => c.Price).ToList();
            }
            else if (sorting == GameSorting.PriceLow)
            {
                gamesQuery = gamesQuery.OrderBy(c => c.Price).ToList();
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
