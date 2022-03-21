using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Data.Models
{
    public class OwnedGame
    {
        [ForeignKey(nameof(Game))]
        public Guid GameId { get; set; }
        public Game Game { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
