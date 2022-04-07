namespace GameStore.Core.Models
{
	public class GameServiceModel
	{
		public int TotalGames { get; set; }

		public IEnumerable<StoreGamesViewModel> Games { get; set; }
	}
}
