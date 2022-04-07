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

        GameServiceModel BrowseAll(string searchTerm = null,
            string genreName = null,
            GameSorting sorting = GameSorting.ReleaseDate, 
            int indexPage = 1, 
            int gamesPerPage = int.MaxValue);
    }
}
