using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TV_Track.Models;
using TV_Track.Interfaces;
using TV_Track.Services;
using Microsoft.AspNetCore.Identity;

namespace TV_Track.Controllers
{
    public class UserController : Controller
    {
        private readonly IFilmTrackService _filmTrackService;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IFilmTrackService filmTrackService, UserManager<IdentityUser> userManager)
        {
            _filmTrackService = filmTrackService;
            _userManager = userManager;
        }


        [HttpPost]
        public IActionResult AddWatchedMovie(Movie movie)
        {
            var userId = _userManager.GetUserAsync(User).Result.Id;
            var movieAdded = _filmTrackService.AddWatchedMovie(movie, userId);

            if (movieAdded)
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetWatchedMovies()
        {
            var userId = _userManager.GetUserAsync(User).Result.Id;

            var movies = _filmTrackService.GetUserWatchedMovies(userId);

            return View("", movies);


        }
    }
}
