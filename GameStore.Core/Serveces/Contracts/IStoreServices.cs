using GameStore.Core.Models;
using GameStore.Core.Models.Manager;

namespace GameStore.Core.Serveces.Contracts
{
    public interface IStoreServices
    {
        IEnumerable<GenreSortingModel> GetGenres();

        bool NameExists(string name);

        List<StoreGamesViewModel> GetGames();

        void Create(string name, 
            string developer, 
            string publisher, 
            string description, 
            decimal price, string imageUrl, 
            DateTime releasedate, 
            List<int> genreIds);

        GameServiceModel All(string searchTerm = null, int indexPage = 1, int gamesPerPage = int.MaxValue);
    }
}
