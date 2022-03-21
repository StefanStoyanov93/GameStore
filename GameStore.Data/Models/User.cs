using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static GameStore.Data.DataConstants;

namespace GameStore.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(UserDisplayName)]
        public string DisplayName { get; set; }

        [StringLength(UserFirstName)]
        public string? FirstName { get; set; }

        [StringLength(UserLastName)]
        public string? LastName { get; set; }

        [Required]
        [StringLength(UserCountry)]
        public string Country { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<OwnedGame> OwnedGames { get; set; } = new List<OwnedGame>();

        public ICollection<Wishlist> WishlistedGames { get; set; } = new List<Wishlist>();
    }
}
