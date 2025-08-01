using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_Track.Models;
using TV_Track.ViewModels;

namespace TV_Track.Interfaces
{
    public interface IMovieDbService
    {
        public Task<Movie> GetMovie(int movieID);
        public Task<TvSeries> GetSeries(int seriesID);
        public Task<TvSeriesSeason> GetSeasonsByTvSeriesId(int seriesID, int seasonNumber);
        public Task<List<Cast>> GetCreditsByTvSeriesID(int seriesID);
        public Task<GeneralViewModel> SearchMediaByTitle(string title, int pageNumber);
        public Task<GeneralViewModel> GetMoviesByGenre(int genreID, int pagenumber);
        public Task<GeneralViewModel> GetTvSeriesByGenre(int genreID, int pagenumber);    
        public Task<List<Movie>> GetTrendingMovies();
        public Task<List<TvSeries>> GetTrendingTvSeries();
        public List<Movie> GetTopMoviesByYear(int year);
        public Task<List<Movie>> GetUpComingMovies();
        public Task<(List<Cast>, Director)> GetCreditsByMovieId(int movieId);


       
        
    }
}
