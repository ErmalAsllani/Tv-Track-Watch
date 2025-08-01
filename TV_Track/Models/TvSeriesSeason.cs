using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class TvSeriesSeason
    {
        public TvSeriesSeason()
        {
            TvSeriesEpisodes = new HashSet<TvSeriesEpisode>();
            WatchedTvshows = new HashSet<WatchedTvshow>();
        }

        public int TvSeasonId { get; set; }
        public int TvSeriesId { get; set; }
        public int SeasonNumber { get; set; }
        public string Summary { get; set; }

        public virtual TvSeries TvSeries { get; set; }
        public virtual ICollection<TvSeriesEpisode> TvSeriesEpisodes { get; set; }
        public virtual ICollection<WatchedTvshow> WatchedTvshows { get; set; }
    }
}
