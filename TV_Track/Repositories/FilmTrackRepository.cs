using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_Track.Models;
using TV_Track.Interfaces;

namespace TV_Track.Repositories
{
    public class FilmTrackRepository : IFilmTrackRepository
    {
        private readonly FilmTrackContext filmTrackContext;

        public FilmTrackRepository(FilmTrackContext _filmTrackContext)
        {
            filmTrackContext = _filmTrackContext;
        }

        public bool AddMovie(Movie movie)
        {
            filmTrackContext.Movies.Add(movie);

            return filmTrackContext.SaveChanges() > 0;
        }

        public List<Genre> GetGenresByMediaType(string mediaType)
        {

            if(mediaType == "movie")
            {
                 return filmTrackContext.Genres.Where(p => p.MediaType == "movie" || p.MediaType == "movietv").ToList();
            }
            else 
            {
                return filmTrackContext.Genres.Where(p => p.MediaType == "tv" || p.MediaType == "movietv").ToList();
            }

        }

        public bool AddWatchedMovie(WatchedMovie watchedMovie)
        {

            filmTrackContext.WatchedMovies.Add(watchedMovie);

            return filmTrackContext.SaveChanges() > 0;


        }

        public List<Movie> GetWatchedMovies(string userId)
        {

            var movieIds = filmTrackContext.WatchedMovies.Where(m => m.UserId == userId).Select(c => c.MovieId).ToList();

            var movies = filmTrackContext.Movies.Where(m => movieIds.Contains(m.MovieId)).ToList();


            return movies;
        }

    }
}
