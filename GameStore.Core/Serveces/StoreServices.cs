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

        public GameServiceModel Browse(string searchTerm = null, 
            int genreId = 0, 
            GameSorting sorting = GameSorting.ReleaseDate, 
            int indexPage = 1, 
            int gamesPerPage = int.MaxValue)
        {

            var gamesQuery = data.Games.AsQueryable();

            if (genreId != 0)
            {
                gamesQuery = gamesQuery.Where((x) => x.Genres.Any(g => g.GenreId == genreId)).AsQueryable();
            }

            if (!String.IsNullOrWhiteSpace(searchTerm))
            {
                gamesQuery = gamesQuery.Where(x => 
                    x.Name.ToLower().Contains(searchTerm.ToLower()) || 
                    x.Developer.ToLower().Contains(searchTerm.ToLower()) || 
                    x.Publisher.ToLower().Contains(searchTerm.ToLower()))
                    .AsQueryable();
            }



			if (sorting == GameSorting.ReleaseDate)
			{
				gamesQuery = gamesQuery.OrderByDescending(c => c.ReleaseDate).AsQueryable();
			}
			else if (sorting == GameSorting.PriceHigh)
			{
				gamesQuery = gamesQuery.OrderByDescending(c => c.Price).AsQueryable();
			}
			else if (sorting == GameSorting.PriceLow)
			{
				gamesQuery = gamesQuery.OrderBy(c => c.Price).AsQueryable();
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

		public bool Buy(string id, string userId)
		{
            var game  = data.Games.Where(x => x.Id.ToString() == id).FirstOrDefault();

            if (game == null)
			{
                return false;
			}

            var ownedGame = new OwnedGame
            {
                UserId = userId,
                Game = game
            };

            data.Add(ownedGame);
            data.SaveChanges();

            return true;
		}

		public GameServiceModel Collection(string searchTerm = null, int genreId = 0, GameSorting sorting = GameSorting.ReleaseDate, int indexPage = 1, int gamesPerPage = int.MaxValue, string userId = null)
		{
            var gamesQuery = data.Games.Where(x => x.GameOwners.Any(g => g.UserId == userId)).AsQueryable();

            bool gameExist = false; 
			if (gamesQuery.Count() == 0)
			{
                gameExist = true;
			}

            if (genreId != 0)
            {
                gamesQuery = gamesQuery.Where((x) => x.Genres.Any(g => g.GenreId == genreId)).AsQueryable();
            }

            if (!String.IsNullOrWhiteSpace(searchTerm))
            {
                gamesQuery = gamesQuery.Where(x =>
                    x.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    x.Developer.ToLower().Contains(searchTerm.ToLower()) ||
                    x.Publisher.ToLower().Contains(searchTerm.ToLower()))
                    .AsQueryable();
            }



            if (sorting == GameSorting.ReleaseDate)
            {
                gamesQuery = gamesQuery.OrderByDescending(c => c.ReleaseDate).AsQueryable();
            }
            else if (sorting == GameSorting.PriceHigh)
            {
                gamesQuery = gamesQuery.OrderByDescending(c => c.Price).AsQueryable();
            }
            else if (sorting == GameSorting.PriceLow)
            {
                gamesQuery = gamesQuery.OrderBy(c => c.Price).AsQueryable();
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
                GameExist = gameExist,
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
