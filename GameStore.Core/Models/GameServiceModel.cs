namespace GameStore.Core.Models
{
	public class GameServiceModel
	{
		public int TotalGames { get; set; }

		public bool GameExist { get; set; }

		public IEnumerable<StoreGamesViewModel> Games { get; set; }
	}
}
