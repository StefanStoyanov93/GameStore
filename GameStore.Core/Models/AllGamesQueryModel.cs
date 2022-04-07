namespace GameStore.Core.Models
{
    public class AllGamesQueryModel
    {
        public const int gamesPerPage = 12;

        public string GenreName { get; init; }

        public string SearchTerm { get; init; }

        public int IndexPage { get; init; } = 1;

        public GameSorting Sorting { get; init; }

        public int TotalGames { get; set; }

        public IEnumerable<GenreSortingModel> Genres { get; set; }

        public IEnumerable<StoreGamesViewModel> Games { get; set; }
    }
}
