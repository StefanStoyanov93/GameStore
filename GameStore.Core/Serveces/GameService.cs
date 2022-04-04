using GameStore.Core.Models;
using GameStore.Core.Serveces.Contracts;
using GameStore.Data.Data;

namespace GameStore.Core.Serveces
{
    public class GameService : IGameService
    {
        private readonly GameStoreDbContext data;

        public GameService(GameStoreDbContext _data)
        {
            this.data = _data;
        }

        public List<IndexGameModel> GetIndexGames()
            => data.Games
            .OrderByDescending(x => x.Id)
            .Select(x => new IndexGameModel
            {
                Id = x.Id.ToString(),
                Name = x.Name,
                Image = x.ImageUrl
            })
            .Take(3)
            .ToList();
    }
}
