using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class WatchedTvshow
    {
        public int WatchedTvshowsId { get; set; }
        public string UserId { get; set; }
        public int TvseriesId { get; set; }
        public int TvseasonId { get; set; }
        public int TvepisodeId { get; set; }

        public virtual TvSeriesEpisode Tvepisode { get; set; }
        public virtual TvSeriesSeason Tvseason { get; set; }
        public virtual TvSeries Tvseries { get; set; }
        public virtual AspNetUser User { get; set; }
    }
}
