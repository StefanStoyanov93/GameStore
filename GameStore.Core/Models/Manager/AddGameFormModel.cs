using System.ComponentModel.DataAnnotations;
using static GameStore.Data.DataConstants;


namespace GameStore.Core.Models
{
    public class AddGameFormModel
    {
        [Required]
        [StringLength(GameNameMaxLength, ErrorMessage = "The game name must be between {2} and {1} characters long!", MinimumLength = GameNameMinLength)]
        [Display(Name = "Name of the game")]
        [DataType(DataType.Text)]
        public string Name { get; init; }

        [Required]
        [StringLength(DeveloperMaxLength, ErrorMessage = "{0} name must be between {2} and {1} characters long!", MinimumLength = DeveloperMinLength)]
        [Display(Name = "Developer")]
        [DataType(DataType.Text)]
        public string Developer { get; init; }

        [Required]
        [StringLength(PublisherMaxLength, ErrorMessage = "{0} name must be between {2} and {1} characters long!", MinimumLength = PublisherMinLength)]
        [Display(Name = "Publisher")]
        [DataType(DataType.Text)]
        public string Publisher { get; init; }

        [Required]
        [StringLength(DescriptionLength, ErrorMessage = "The [0] can't be more than [2] characters")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; init; }

        [Required]
        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; init; }

        [Required]
        public decimal Price { get; init; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; init; }

        public ICollection<int> GenreIds { get; init; } = new List<int>();

        public IEnumerable<GenreSortingModel> Genres { get; set; } = new List<GenreSortingModel>();
    }
}
