using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_Track.Models;

namespace TV_Track.Interfaces
{
    public interface IFilmTrackService
    {
        public bool AddMovie(Movie movie);
        public bool AddWatchedMovie(Movie movie, string userId);
        public List<Movie> GetUserWatchedMovies(string userId);
    }
}
