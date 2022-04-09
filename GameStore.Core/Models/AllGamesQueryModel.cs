using System.ComponentModel.DataAnnotations;

namespace GameStore.Core.Models
{
    public class AllGamesQueryModel
    {
        public const int gamesPerPage = 12;

        public int GenreId { get; init; }

        public string SearchTerm { get; init; }

        public int IndexPage { get; init; } = 1;

        [Display(Name = "Sort By:")]
        public GameSorting Sorting { get; set; }

        public int TotalGames { get; set; }

		public bool GamesExist { get; set; }

		public IEnumerable<GenreSortingModel> Genres { get; set; }

        public IEnumerable<StoreGamesViewModel> Games { get; set; }
    }
}
