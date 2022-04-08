using GameStore.Core.Models;
using GameStore.Core.Models.Manager;

namespace GameStore.Core.Serveces.Contracts
{
    public interface IStoreServices
    {
        IEnumerable<GenreSortingModel> GetGenres();

        bool NameExists(string name);

        List<StoreGamesViewModel> GetGames();

        GameServiceManagerModel All(string searchTerm = null, 
            int indexPage = 1, 
            int gamesPerPage = int.MaxValue);

        GameServiceModel Browse(string searchTerm = null,
            int genreId = 0,
            GameSorting sorting = GameSorting.ReleaseDate, 
            int indexPage = 1, 
            int gamesPerPage = int.MaxValue);

        GameServiceModel Collection(string searchTerm = null,
            int genreId = 0, 
            GameSorting sorting = GameSorting.ReleaseDate, 
            int indexPage = 1,
            int gamesPerPage = int.MaxValue,
            string userId = null);

		bool Buy(string id, string userId);
	}
}
