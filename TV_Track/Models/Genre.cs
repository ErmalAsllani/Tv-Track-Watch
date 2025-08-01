using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class Genre
    {
        public Genre()
        {
            TvGenres = new HashSet<TvGenre>();
        }

        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public string MediaType { get; set; }

        public virtual ICollection<TvGenre> TvGenres { get; set; }
    }
}
