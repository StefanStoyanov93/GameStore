namespace GameStore.Core.Models.Manager
{
    public class GameServiceModel
    {
        public int TotalGames { get; set; }

        public IEnumerable<StoreGamesViewModel> Games { get; set; }
    }
}
