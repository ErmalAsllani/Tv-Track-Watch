using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class Director
    {
        public Director()
        {
            Movies = new HashSet<Movie>();
            TvSeriesEpisodes = new HashSet<TvSeriesEpisode>();
        }

        public int DirectorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
        public virtual ICollection<TvSeriesEpisode> TvSeriesEpisodes { get; set; }
    }
}
