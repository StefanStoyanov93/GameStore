namespace GameStore.Core.Models.Manager
{
    public class GameDeleteModel
    {
        public string Id { get; init; }

        public string Name { get; init; }

        public string Developer { get; init; }

        public string Publisher { get; init; }

        public decimal Price { get; init; }

        public DateTime ReleaseDate { get; init; }
    }
}
