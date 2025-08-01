using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TV_Track.Models;
using TV_Track.Services;
using Microsoft.Extensions.Configuration;
using TV_Track.ViewModels;
using TV_Track.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using TV_Track.Repositories;

namespace TV_Track.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieDbService _movieDbService;

        public HomeController(ILogger<HomeController> logger, IMovieDbService movieDbService)
        {
            _logger = logger;
            _movieDbService = movieDbService;
        }

        public async Task<IActionResult> Index()
        {
        
            HomeViewModel homeViewModel = new HomeViewModel();

            homeViewModel.TrendingMovies = await _movieDbService.GetTrendingMovies();
            homeViewModel.TrendingTvSeries = await _movieDbService.GetTrendingTvSeries();
            homeViewModel.UpcomingMovies = await _movieDbService.GetUpComingMovies();

            return View(homeViewModel);
        }

        public async Task<IActionResult> SearchResults(string searchTerm, int pageNumber = 1)
        {
            
            GeneralViewModel searchResults = await _movieDbService.SearchMediaByTitle(searchTerm, pageNumber);

            return View(searchResults);
        }

        public async Task<IActionResult> Movies(int genreID = 28, int pageNumber = 1)
        {

            GeneralViewModel viewModel = await _movieDbService.GetMoviesByGenre(genreID, pageNumber);

            return View(viewModel);
        }

        public async Task<IActionResult> TvShows(int genreID = 10759, int pageNumber = 1)
        {
  
            GeneralViewModel viewModel = await _movieDbService.GetTvSeriesByGenre(genreID, pageNumber);
      
            return View(viewModel);
        }

        public async Task<IActionResult> GetMedia(int id = 505, string mediaType = "movie")
        {
            if(mediaType == "movie")
            {
                Movie movie = await _movieDbService.GetMovie(id);
                return View("MovieDetails", movie);

            }
            else
            {
                TvSeries tvSeries = await _movieDbService.GetSeries(id);
                return View("TVDetails", tvSeries);
            }     
        }

        public async Task<IActionResult> GetSeasonByIdJson(int tvId, int seasonNumber)
        {
            TvSeriesSeason tvSeason = await _movieDbService.GetSeasonsByTvSeriesId(tvId, seasonNumber);

            return Json(tvSeason);
        }

        // Add These two views if i have time
        public IActionResult AboutUs()
        {
            // About Us Page
            return View();
        }

        public IActionResult PrivacyPolicy()
        {
            // Privacy Policy Page
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
