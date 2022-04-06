using GameStore.Core.Models;
using GameStore.Core.Models.Manager;

namespace GameStore.Core.Serveces.Contracts
{
    public interface IGameService
    {
        List<IndexGameModel> GetIndexGames();

        bool Delete(string id);

        GameDeleteModel GetGameForDelete(string id);
    }
}
