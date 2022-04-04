using GameStore.Core.Models;

namespace GameStore.Core.Serveces.Contracts
{
    public interface IGameService
    {
        List<IndexGameModel> GetIndexGames();
    }
}
