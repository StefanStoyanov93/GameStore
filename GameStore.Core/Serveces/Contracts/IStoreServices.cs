using GameStore.Core.Models;
using GameStore.Core.Models.Manager;

namespace GameStore.Core.Serveces.Contracts
{
    public interface IStoreServices
    {
        IEnumerable<GenreSortingModel> GetGenres();

        Task<bool> NameExists(string name);

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
            string userId = null,
            bool isBought = true);

        Task Buy(string id, string userId);

		Task WishlistRemove(string id, string userId);

		Task WishlistAdd(string id, string userId);

        Task<IEnumerable<BaseGameModel>> GetIndexGames();
    }
}
