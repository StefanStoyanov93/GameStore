using System.ComponentModel.DataAnnotations;
using static GameStore.Data.DataConstants;
namespace GameStore.Data.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [StringLength(GenreNameMaxLength), MinLength(GenreNameMinLength)]
        public string GenreName { get; set; }

        public virtual ICollection<GameGenre> Games { get; set; } = new List<GameGenre>();
    }
}
