using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameStore.Data.DataConstants;

namespace GameStore.Data.Models
{
    public class User : IdentityUser
    {
        [StringLength(UserFirstName)]
        public string? FirstName { get; set; }

        [StringLength(UserLastName)]
        public string? LastName { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }

        public Country Country { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        public ICollection<OwnedGame> OwnedGames { get; set; } = new List<OwnedGame>();

        public ICollection<Wishlist> WishlistedGames { get; set; } = new List<Wishlist>();
    }
}
