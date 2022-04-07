namespace GameStore.Core.Models.Manager
{
    public class GameServiceModel
    {
        public int TotalGames { get; set; }

        public IEnumerable<BaseGameModel> Games { get; set; }
    }
}
