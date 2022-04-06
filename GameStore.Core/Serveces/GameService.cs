using GameStore.Core.Models;
using GameStore.Core.Models.Manager;
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

        public bool Delete(string id)
        {
            var game = data.Games.FirstOrDefault(x => x.Id.ToString() == id);

            if (game == null)
            {
                return false;
            }

            data.Games.Remove(game);
            data.SaveChanges();

            return true;
        }

        public GameDeleteModel GetGameForDelete(string id)
        {
            var game = data.Games.FirstOrDefault(x => x.Id.ToString() == id);

            var model = new GameDeleteModel
            {
                Id = id,
                Developer = game.Developer,
                Publisher = game.Publisher,
                Name = game.Name,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            };

            return model;
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
