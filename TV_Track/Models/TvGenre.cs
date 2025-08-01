using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class TvGenre
    {
        public int TvGenreId { get; set; }
        public int? MovieId { get; set; }
        public int? TvSeriesId { get; set; }
        public int GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Movie Movie { get; set; }
        public virtual TvSeries TvSeries { get; set; }
    }
}
