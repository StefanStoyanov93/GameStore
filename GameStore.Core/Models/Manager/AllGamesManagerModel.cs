using System.ComponentModel.DataAnnotations;

namespace GameStore.Core.Models.Manager
{
    public class AllGamesManagerModel
    {
        public const int gamesPerPage = 12;

        [Display(Name = "Search by name")]
        public string SearchTerm { get; init; }

        public int IndexPage { get; init; } = 1;

        public int TotalGames { get; set; }

        public IEnumerable<BaseGameModel> Games { get; set; }
    }
}
