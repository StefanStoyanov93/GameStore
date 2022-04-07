namespace GameStore.Core.Models.Manager
{
    public class GameServiceManagerModel
    {
        public int TotalGames { get; set; }

        public IEnumerable<BaseGameModel> Games { get; set; }
    }
}
