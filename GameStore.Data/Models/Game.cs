using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameStore.Data.DataConstants;

namespace GameStore.Data.Models
{
    public class Game
    {
        [Key]
        public Guid Id { get; init; } = Guid.NewGuid();

        [Required]
        [StringLength(GameNameMaxLength), MinLength(GameNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(DeveloperMaxLength), MinLength(DeveloperMinLength)]
        public string Developer { get; set; }

        [Required]
        [StringLength(PublisherMaxLength), MinLength(PublisherMinLength)]
        public string Publisher { get; set; }

        [Required]
        [StringLength(DescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [MaxLength(140)]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime ReleaseDate { get; set; }

        public virtual ICollection<GameGenre> Genres { get; set; } = new List<GameGenre>();
        public virtual ICollection<OwnedGame> GameOwners { get; set; } = new List<OwnedGame>();
        public virtual ICollection<Wishlist> Wishlist { get; set; } = new List<Wishlist>();
    }
}
