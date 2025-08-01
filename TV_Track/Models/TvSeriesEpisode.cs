using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class TvSeriesEpisode
    {
        public TvSeriesEpisode()
        {
            WatchedTvshows = new HashSet<WatchedTvshow>();
        }

        public int TvEpisodeId { get; set; }
        public int TvSeasonId { get; set; }
        public string Title { get; set; }
        public int DirectorId { get; set; }
        public string Description { get; set; }
        public DateTime AirDate { get; set; }
        public TimeSpan EpisodeLength { get; set; }
        public int? EpisodeNumber { get; set; }

        public virtual Director Director { get; set; }
        public virtual TvSeriesSeason TvSeason { get; set; }
        public virtual ICollection<WatchedTvshow> WatchedTvshows { get; set; }
    }
}
