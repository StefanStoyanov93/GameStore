using System.ComponentModel.DataAnnotations;
using static GameStore.Data.DataConstants;

namespace GameStore.Data.Models
{
    public class Country
    {
        [Key]
        public int Id { get; init; }

        [Required]
        [StringLength(UserCountry)]
        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
