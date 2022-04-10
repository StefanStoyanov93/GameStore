using GameStore.Core.Models;
using GameStore.Core.Models.Manager;

namespace GameStore.Core.Serveces.Contracts
{
    public interface IGameService
    {
        void Create(string name,
            string developer,
            string publisher,
            string description,
            decimal price, string imageUrl,
            DateTime releasedate,
            List<int> genreIds);

        bool Edit(string id,
            string name,
            string developer,
            string publisher,
            string description,
            string imageUrl,
            decimal price,
            DateTime releaseDate,
            ICollection<int> genreIds);

       

        bool Delete(string id);

        GameDeleteModel GetGameForDelete(string id);

        GamesDetailsListModel Details(string id);


        OwnershipSorting GetOwnership(string id, string userId);
    }
}
