using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TV_Track.Models;

namespace TV_Track.ViewModels
{
    public class HomeViewModel
    {
        public List<Movie> TrendingMovies { get; set; }

        public List<TvSeries> TrendingTvSeries { get; set; }
        public List<Movie> UpcomingMovies { get; set; }

    }
}
