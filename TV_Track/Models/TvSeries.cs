using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class TvSeries
    {
        public TvSeries()
        {
            Casts = new HashSet<Cast>();
            ListContents = new HashSet<ListContent>();
            NotifyOnContentReleases = new HashSet<NotifyOnContentRelease>();
            TvGenres = new HashSet<TvGenre>();
            TvSeriesSeasons = new HashSet<TvSeriesSeason>();
            WatchedTvshows = new HashSet<WatchedTvshow>();
        }

        public int TvSeriesId { get; set; }
        public string Title { get; set; }
        public string TvSeriesPoster { get; set; }
        public string Creator { get; set; }
        public string Description { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime? EndYear { get; set; }
        public string CountryName { get; set; }
        public int? NumberOfSeasons { get; set; }
        public string RunningTime { get; set; }
        public string VoteAverage { get; set; }

        public virtual ICollection<Cast> Casts { get; set; }
        public virtual ICollection<ListContent> ListContents { get; set; }
        public virtual ICollection<NotifyOnContentRelease> NotifyOnContentReleases { get; set; }
        public virtual ICollection<TvGenre> TvGenres { get; set; }
        public virtual ICollection<TvSeriesSeason> TvSeriesSeasons { get; set; }
        public virtual ICollection<WatchedTvshow> WatchedTvshows { get; set; }
    }
}
