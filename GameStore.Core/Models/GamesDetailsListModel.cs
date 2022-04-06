namespace GameStore.Core.Models
{
    public class GamesDetailsListModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Developer { get; init; }

        public string Publisher { get; init; }

        public string Description { get; init; }

        public string ImageUrl { get; init; }

        public decimal Price { get; init; }

        public DateTime ReleaseDate { get; init; }

        public IEnumerable<GenreSortingModel> Genres { get; set; } = new List<GenreSortingModel>();
    }
}
