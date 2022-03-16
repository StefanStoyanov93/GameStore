using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameStore.Data.Models
{
    public class GameGenre
    {
        [ForeignKey(nameof(Game))]
        public Guid GameId { get; set; }
        public Game Game { get; set; }

        [ForeignKey(nameof(Genre))]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
