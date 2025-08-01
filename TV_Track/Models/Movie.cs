using System;
using System.Collections.Generic;

#nullable disable

namespace TV_Track.Models
{
    public partial class Movie
    {
        public Movie()
        {
            Casts = new HashSet<Cast>();
            ListContents = new HashSet<ListContent>();
            NotifyOnContentReleases = new HashSet<NotifyOnContentRelease>();
            TvGenres = new HashSet<TvGenre>();
            WatchedMovies = new HashSet<WatchedMovie>();
        }

        public int MovieId { get; set; }
        public string Title { get; set; }
        public string MoviePoster { get; set; }
        public int? DirectorId { get; set; }
        public DateTime? ReleaseYear { get; set; }
        public string RunningTime { get; set; }
        public string MovieDescription { get; set; }
        public string VoteAverage { get; set; }
        public string CountryName { get; set; }

        public virtual Director Director { get; set; }
        public virtual ICollection<Cast> Casts { get; set; }
        public virtual ICollection<ListContent> ListContents { get; set; }
        public virtual ICollection<NotifyOnContentRelease> NotifyOnContentReleases { get; set; }
        public virtual ICollection<TvGenre> TvGenres { get; set; }
        public virtual ICollection<WatchedMovie> WatchedMovies { get; set; }
    }
}
