using GameStore.Core.Models;
using GameStore.Core.Models.Manager;

namespace GameStore.Core.Serveces.Contracts
{
    public interface IGameService
    {
        Task Create(string name,
            string developer,
            string publisher,
            string description,
            decimal price, string imageUrl,
            DateTime releasedate,
            List<int> genreIds);

        Task<bool> Edit(string id,
            string name,
            string developer,
            string publisher,
            string description,
            string imageUrl,
            decimal price,
            DateTime releaseDate,
            ICollection<int> genreIds);



        Task<bool> Delete(string id);

        Task<GameDeleteModel> GetGameForDelete(string id);

        Task<GamesDetailsListModel> Details(string id);


        Task<OwnershipSorting> GetOwnership(string id, string userId);
    }
}
