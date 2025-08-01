using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_Track.Models;

namespace TV_Track.Interfaces
{
    public interface IFilmTrackRepository
    {
        public List<Genre> GetGenresByMediaType(string mediaType);
        public bool AddMovie(Movie movie);
        public bool AddWatchedMovie(WatchedMovie watchedMovie);
        public List<Movie> GetWatchedMovies(string userId);
    }
}
