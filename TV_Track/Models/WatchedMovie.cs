using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class WatchedMovie
    {
        public int WatchedMovieId { get; set; }
        public string UserId { get; set; }
        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
