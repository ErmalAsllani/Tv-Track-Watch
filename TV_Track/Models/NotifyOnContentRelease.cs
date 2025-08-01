using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class NotifyOnContentRelease
    {
        public int NotifyId { get; set; }
        public int? MovieId { get; set; }
        public int? TvSeriesId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual TvSeries TvSeries { get; set; }
    }
}
