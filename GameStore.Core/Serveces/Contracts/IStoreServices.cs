using GameStore.Core.Models;
using GameStore.Models;

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
    }
}
