using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_Track.Interfaces;
using TV_Track.Models;
using TV_Track.Repositories;

namespace TV_Track.Services
{
    public class FilmTrackService : IFilmTrackService
    {
        private readonly IFilmTrackRepository _filmTrackRepository;

        public FilmTrackService(IFilmTrackRepository filmTrackRepository)
        {
            _filmTrackRepository = filmTrackRepository;
        }


        public bool AddMovie(Movie movie)
        {
            return _filmTrackRepository.AddMovie(movie);
        }


        public bool AddWatchedMovie(Movie movie, string userId)
        {
            AddMovie(movie);

            var watchedMovie = new WatchedMovie();
            watchedMovie.UserId = userId;
            watchedMovie.MovieId = movie.MovieId;


            _filmTrackRepository.AddWatchedMovie(watchedMovie);

            return true;

            
           
        }

        public List<Movie> GetUserWatchedMovies(string userId)
        {
            var movies = _filmTrackRepository.GetWatchedMovies(userId);

            return movies;
        }
    }
}
