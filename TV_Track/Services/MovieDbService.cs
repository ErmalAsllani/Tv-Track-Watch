using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TV_Track.Interfaces;
using TV_Track.Models;
using TV_Track.ViewModels;

namespace TV_Track.Services
{
    public class MovieDbService : IMovieDbService
    {
        public MovieDbService()
        {
            APIHelper.Initialize();
        }

        #region Media
        public async Task<GeneralViewModel> SearchMediaByTitle(string title, int pagenumber)
        {
            string url = "https://api.themoviedb.org/3/search/multi?api_key=993b7eb52954ee6f755a7670975de5ff&query=" + title.Replace(" ", "+") + "&page=" + pagenumber;

            using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();


                    GeneralViewModel resultsViewModel = new GeneralViewModel();
                    resultsViewModel.Media = new List<GeneralMedia>();


                    var jsearchResults = JObject.Parse(content);

                    resultsViewModel.TotalPages = int.Parse(jsearchResults["total_pages"].ToString());
                    resultsViewModel.SearchTerm = title;
                    resultsViewModel.PageNumber = pagenumber;
                    resultsViewModel.TotalResults = int.Parse(jsearchResults["total_results"].ToString());


                    JArray resultItems = (JArray)jsearchResults["results"];


                    foreach (var item in resultItems)
                    {
                        GeneralMedia media = new GeneralMedia();
                        string mediaType = item["media_type"].ToString();
                        if (mediaType == "movie" || mediaType == "tv")
                        {
                            media.ID = int.Parse(item["id"].ToString());
                            media.Title = mediaType == "movie" ? item["original_title"].ToString() : item["original_name"].ToString();

                            string posterPath = item["poster_path"].ToString();

                            if (string.IsNullOrEmpty(posterPath))
                                posterPath = "/img/no_poster.jpg";
                            else
                                posterPath = "https://image.tmdb.org/t/p/w500" + posterPath;

                            media.Poster = posterPath;
                            // Replace this with an icon later on
                            media.MediaType = mediaType == "movie" ? "movie" : "tv";

                            string voteAverage = item["vote_average"].ToString();

                            media.VoteAverage = voteAverage != "" ? voteAverage : "";

                            resultsViewModel.Media.Add(media);
                        }


                    }

                    return resultsViewModel;
                }
                else
                {
                    throw new Exception();
                }
            }
        }
        #endregion

        #region MovieDetails
        public async Task<Movie> GetMovie(int movieID)
        {
            string url = "https://api.themoviedb.org/3/movie/" + movieID + "?api_key=993b7eb52954ee6f755a7670975de5ff&language=en-US";

            using(HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var jcontent = JObject.Parse(content);

                    Movie movie = new Movie();
                    movie.MovieId = int.Parse(jcontent["id"].ToString());
                    movie.Title = jcontent["original_title"].ToString();
                    movie.MovieDescription = jcontent["overview"].ToString();

                    JArray genres = JArray.Parse(jcontent["genres"].ToString());

                    string posterPath = jcontent["poster_path"].ToString();

                    if (String.IsNullOrEmpty(posterPath))
                        posterPath = "/img/no_poster.jpg";
                    else
                        posterPath = "https://image.tmdb.org/t/p/w500" + posterPath;

                    movie.MoviePoster = posterPath;




                    List<TvGenre> tvGenreList = new List<TvGenre>();

                    foreach (var genre in genres)
                    {
                        TvGenre tvGenre1 = new TvGenre();
                        Genre mGenre = new Genre();
                        mGenre.GenreId = int.Parse(genre["id"].ToString());
                        mGenre.GenreName = genre["name"].ToString();
                        tvGenre1.Genre = mGenre;
                        tvGenre1.GenreId = mGenre.GenreId;
                        tvGenre1.MovieId = movie.MovieId;

                        tvGenreList.Add(tvGenre1);

                    }

                    movie.TvGenres = tvGenreList;

                    (List<Cast>, Director) castList = await GetCreditsByMovieId(movieID);
                    movie.Casts = castList.Item1;
                    movie.DirectorId = castList.Item2.DirectorId;
                    movie.Director = castList.Item2;

                    movie.ReleaseYear = DateTime.Parse(jcontent["release_date"].ToString());
                    movie.RunningTime = jcontent["runtime"].ToString();


                    return movie;

                }
                else
                {
                    throw new Exception();
                }
            }
        }
        public async Task<(List<Cast>, Director)> GetCreditsByMovieId(int movieId = 634649)
        {

            string url = "https://api.themoviedb.org/3/movie/" + movieId + "/credits?api_key=993b7eb52954ee6f755a7670975de5ff&language=en-US&language=en-US";

            using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var jcontent = JObject.Parse(responseBody);

                    JArray jcasts = (JArray)jcontent["cast"];

                    List<Cast> castList = new List<Cast>();

                    foreach (var jcast in jcasts)
                    {
                        if (castList.Count > 5)
                            break;

                        Cast cast = new Cast();
                        Actor actor = new Actor();
                        actor.ActorId = int.Parse(jcast["id"].ToString());

                        string[] fullName = jcast["name"].ToString().Split(' ');

                        if(fullName.Length > 1)
                        {
                            actor.FirstName = fullName[0];
                            actor.LastName = fullName[1];
                        }
                        else
                        {
                            actor.FirstName = fullName[0];
                        }

                       

                        cast.Actor = actor;
                        cast.ActorId = actor.ActorId;

                        castList.Add(cast);
                    }


                    JArray crew = (JArray)jcontent["crew"];
                    Director director = new Director();
                    foreach (var item in crew)
                    {
                        if (item["job"].ToString() == "Director")
                        {
                            director.DirectorId = int.Parse(item["id"].ToString());
                            string[] fullName = item["name"].ToString().Split(' ');
                            director.FirstName = fullName[0];
                            director.LastName = fullName[1];
                            break;
                        }
                    }


                    return (castList, director);

                }
                else
                {
                    throw new Exception();
                }
            }
        }
        #endregion

        #region Movies
        public async Task<GeneralViewModel> GetMoviesByGenre(int genreID, int pageNumber)
        {
            string url = "https://api.themoviedb.org/3/discover/movie?api_key=993b7eb52954ee6f755a7670975de5ff&with_genres=" + genreID + "&page=" + pageNumber;

            using (HttpResponseMessage responseMessage = await APIHelper.APIClient.GetAsync(url))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    string content = await responseMessage.Content.ReadAsStringAsync();

                    var jcontent = JObject.Parse(content);
                    JArray results = (JArray)jcontent["results"];

                    GeneralViewModel viewModel = new GeneralViewModel();
                    viewModel.Media = new List<GeneralMedia>();
                    viewModel.PageNumber = pageNumber;
                    viewModel.GenreID = genreID;
                    viewModel.TotalPages = int.Parse(jcontent["total_pages"].ToString());
                    viewModel.TotalResults = int.Parse(jcontent["total_results"].ToString());

                    foreach (var item in results)
                    {
                        GeneralMedia movie = new GeneralMedia();
                        movie.ID = int.Parse(item["id"].ToString());
                        movie.Title = item["original_title"].ToString();

                        string posterPath = item["poster_path"].ToString();

                        if (String.IsNullOrEmpty(posterPath))
                            posterPath = "/img/no_poster.jpg";
                        else
                            posterPath = "https://image.tmdb.org/t/p/w500" + posterPath;

                        movie.Poster = posterPath;

                        string voteAverage = item["vote_average"].ToString();

                        movie.VoteAverage = voteAverage != "" ? voteAverage : "";

                        List<string> genresIdList = new List<string>();
                        foreach (var genre in item["genre_ids"])
                        {
                            genresIdList.Add(genre.ToString());
                        }

                        movie.GenreIdList = genresIdList;
                        viewModel.Media.Add(movie);


                    }




                    return viewModel;


                }
                else
                {
                    throw new Exception();
                }
            }

        }

        public List<Movie> GetTopMoviesByYear(int year)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetTrendingMovies()
        {
            // Gets Trending Movies By Day or Week => needs configuration
            string url = "https://api.themoviedb.org/3/trending/movie/day?api_key=993b7eb52954ee6f755a7670975de5ff";

            using (HttpResponseMessage responseMessage = await APIHelper.APIClient.GetAsync(url))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    string content = await responseMessage.Content.ReadAsStringAsync();

                    var jcontent = JObject.Parse(content);
                    JArray results = (JArray)jcontent["results"];

                    List<Movie> trendingMovies = new List<Movie>();

                    foreach (var item in results)
                    {
                        Movie movie = new Movie();
                        movie.MovieId = int.Parse(item["id"].ToString());
                        movie.Title = item["original_title"].ToString();

                        string posterPath = item["poster_path"].ToString();

                        if (String.IsNullOrEmpty(posterPath))
                            posterPath = "/img/no_poster.jpg";
                        else
                            posterPath = "https://image.tmdb.org/t/p/w500" + posterPath;

                        movie.MoviePoster = posterPath;

                        string voteAverage = item["vote_average"].ToString();

                        movie.VoteAverage = voteAverage != "" ? voteAverage : "";

                        trendingMovies.Add(movie);


                    }

                    return trendingMovies;


                }
                else
                {
                    throw new Exception();
                }
            }
        }

        public async Task<List<Movie>> GetUpComingMovies()
        {
            // This needs Reworking 
            string url = "https://api.themoviedb.org/3/movie/upcoming?api_key=993b7eb52954ee6f755a7670975de5ff&language=en-US&page=1";

            using (HttpResponseMessage responseMessage = await APIHelper.APIClient.GetAsync(url))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    string content = await responseMessage.Content.ReadAsStringAsync();

                    var jcontent = JObject.Parse(content);
                    JArray results = (JArray)jcontent["results"];

                    List<Movie> trendingMovies = new List<Movie>();

                    foreach (var item in results)
                    {
                        Movie movie = new Movie();
                        movie.MovieId = int.Parse(item["id"].ToString());
                        movie.Title = item["original_title"].ToString();

                        string posterPath = item["poster_path"].ToString();

                        if (String.IsNullOrEmpty(posterPath))
                            posterPath = "/img/no_poster.jpg";
                        else
                            posterPath = "https://image.tmdb.org/t/p/w500" + posterPath;

                        movie.MoviePoster = posterPath;

                        string voteAverage = item["vote_average"].ToString();

                        movie.VoteAverage = voteAverage != "" ? voteAverage : "";

                        trendingMovies.Add(movie);


                    }

                    return trendingMovies;


                }
                else
                {
                    throw new Exception();
                }
            }

        }

        #endregion

        #region TvSeries

        public async Task<TvSeries> GetSeries(int seriesID)
        {
            string url = "https://api.themoviedb.org/3/tv/" + seriesID + "?api_key=993b7eb52954ee6f755a7670975de5ff&language=en-US";

            using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var jcontent = JObject.Parse(responseBody);

                    TvSeries tvSeries = new TvSeries();

                    tvSeries.TvSeriesId = int.Parse(jcontent["id"].ToString());
                    tvSeries.Title = jcontent["original_name"].ToString();
                    tvSeries.Description = jcontent["overview"].ToString();


                    string posterPath = jcontent["poster_path"].ToString();

                    if (String.IsNullOrEmpty(posterPath))
                        posterPath = "/img/no_poster.jpg";
                    else
                        posterPath = "https://image.tmdb.org/t/p/w500" + posterPath;

                    tvSeries.TvSeriesPoster = posterPath;
                    tvSeries.NumberOfSeasons = int.Parse(jcontent["number_of_seasons"].ToString());

                    tvSeries.Casts = await GetCreditsByTvSeriesID(seriesID);

                    JArray jgenres = (JArray)jcontent["genres"];
                    List<TvGenre> tvGenres = new List<TvGenre>();

                    foreach (var item in jgenres)
                    {
                        Genre genre = new Genre();
                        genre.GenreId = int.Parse(item["id"].ToString());
                        genre.GenreName = item["name"].ToString();

                        TvGenre tvGenre = new TvGenre();
                        tvGenre.TvSeriesId = seriesID;
                        tvGenre.Genre = genre;

                        tvGenres.Add(tvGenre);
                    }


                    tvSeries.TvGenres = tvGenres;
                    JArray jcreator = (JArray)jcontent["created_by"];
                    tvSeries.Creator = jcreator[0]["name"].ToString();

                    var runtime = jcontent["episode_run_time"].ToString();
                    if (runtime != "[]")
                        tvSeries.RunningTime = jcontent["episode_run_time"][0].ToString();
                    else
                        tvSeries.RunningTime = "0";

                    tvSeries.StartYear = DateTime.Parse(jcontent["first_air_date"].ToString());
                    string lastAir = jcontent["next_episode_to_air"].ToString();

                    if (String.IsNullOrEmpty(lastAir))
                        tvSeries.EndYear = DateTime.Parse(jcontent["last_air_date"].ToString());
                  

                    tvSeries.CountryName = jcontent["origin_country"][0].ToString();


                   // This was made like this if in the future we want to store all seasons of the show
                    tvSeries.TvSeriesSeasons = new List<TvSeriesSeason>() { await GetSeasonsByTvSeriesId(seriesID, 1) };



                    return tvSeries;
                }
                else
                {
                    throw new Exception();
                }


            }


        }
        public async Task<List<Cast>> GetCreditsByTvSeriesID(int seriesID)
        {
            string url = "https://api.themoviedb.org/3/tv/" + seriesID + "/credits?api_key=993b7eb52954ee6f755a7670975de5ff&language=en-US";

            using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {

                    string responseBody = await response.Content.ReadAsStringAsync();

                    var jcontent = JObject.Parse(responseBody);
                    JArray jcast = (JArray)jcontent["cast"];

                    List<Cast> seriesCast = new List<Cast>();

                    foreach (var item in jcast)
                    {
                        if (seriesCast.Count > 5)
                            break;

                        Cast cast = new Cast();
                        Actor actor = new Actor();

                        actor.ActorId = int.Parse(item["id"].ToString());
                        string[] fullName = item["name"].ToString().Split(" ");

                        if (fullName.Length > 1)
                        {
                            actor.FirstName = fullName[0];
                            actor.LastName = fullName[1];
                        }
                        else
                        {
                            actor.FirstName = fullName[0];
                        }


                        cast.Actor = actor;
                        cast.TvSeriesId = seriesID;

                        seriesCast.Add(cast);
                    }



                    return seriesCast;

                }
                else
                {
                    throw new Exception();
                }
            }


        }
        public async Task<TvSeriesSeason> GetSeasonsByTvSeriesId(int seriesID, int seasonNumber)
        {
            string url = "https://api.themoviedb.org/3/tv/" + seriesID + "/season/" + seasonNumber + "?api_key=993b7eb52954ee6f755a7670975de5ff&language=en-US";

            using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();

                    var jresponse = JObject.Parse(responseBody);


                    TvSeriesSeason season = new TvSeriesSeason();
                    season.SeasonNumber = int.Parse(jresponse["season_number"].ToString());
                    season.Summary = jresponse["overview"].ToString();

                    JArray jseasonEpisodes = (JArray)jresponse["episodes"];

                    List<TvSeriesEpisode> tvSeriesEpisodes = new List<TvSeriesEpisode>();

                    foreach (var jepisode in jseasonEpisodes)
                    {
                        TvSeriesEpisode episode = new TvSeriesEpisode();
                        episode.TvSeasonId = seriesID;
                        episode.Title = jepisode["name"].ToString();
                        episode.Description = jepisode["overview"].ToString();
                        episode.AirDate = DateTime.Parse(jepisode["air_date"].ToString());
                        episode.EpisodeNumber = int.Parse(jepisode["episode_number"].ToString());

                        tvSeriesEpisodes.Add(episode);
                    }

                    season.TvSeriesEpisodes = tvSeriesEpisodes;

      
                    return season;

                }
                else
                {
                    throw new Exception();
                }
            }

            throw new NotImplementedException();
        }
        public async Task<List<TvSeries>> GetTrendingTvSeries()
        {
            string url = "https://api.themoviedb.org/3/trending/tv/day?api_key=993b7eb52954ee6f755a7670975de5ff";

            using (HttpResponseMessage response = await APIHelper.APIClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var jcontent = JObject.Parse(content);
                    JArray jcontentArray = (JArray)jcontent["results"];

                    List<TvSeries> trendingTvSeries = new List<TvSeries>();

                    foreach (var item in jcontentArray)
                    {
                        TvSeries tvSeries = new TvSeries();
                        tvSeries.TvSeriesId = int.Parse(item["id"].ToString());
                        tvSeries.Title = item["original_name"].ToString();

                        string posterPath = item["poster_path"].ToString();

                        if (String.IsNullOrEmpty(posterPath))
                            posterPath = "/img/no_poster.jpg";
                        else
                            posterPath = "https://image.tmdb.org/t/p/w500" + posterPath;

                        tvSeries.TvSeriesPoster = posterPath;

                        string voteAverage = item["vote_average"].ToString();

                        tvSeries.VoteAverage = voteAverage != "" ? voteAverage : "";

                        trendingTvSeries.Add(tvSeries);
                    }

                    return trendingTvSeries;
                }
                else
                {
                    throw new Exception();
                }


            }
        }
        public async Task<GeneralViewModel> GetTvSeriesByGenre(int genreID, int pagenumber)
        {
            string url = "https://api.themoviedb.org/3/discover/tv?api_key=993b7eb52954ee6f755a7670975de5ff&with_genres=" + genreID + "&page=" + pagenumber;

            using (HttpResponseMessage responseMessage = await APIHelper.APIClient.GetAsync(url))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    string content = await responseMessage.Content.ReadAsStringAsync();

                    var jcontent = JObject.Parse(content);
                    JArray results = (JArray)jcontent["results"];

                    GeneralViewModel viewModel = new GeneralViewModel();
                    viewModel.Media = new List<GeneralMedia>();
                    viewModel.PageNumber = pagenumber;
                    viewModel.GenreID = genreID;
                    viewModel.TotalPages = int.Parse(jcontent["total_pages"].ToString());
                    viewModel.TotalResults = int.Parse(jcontent["total_results"].ToString());

                    foreach (var item in results)
                    {
                        GeneralMedia movie = new GeneralMedia();
                        movie.ID = int.Parse(item["id"].ToString());
                        movie.Title = item["original_name"].ToString();

                        string posterPath = item["poster_path"].ToString();

                        if (String.IsNullOrEmpty(posterPath))
                            posterPath = "/img/no_poster.jpg";
                        else
                            posterPath = "https://image.tmdb.org/t/p/w500" + posterPath;

                        movie.Poster = posterPath;

                        string voteAverage = item["vote_average"].ToString();

                        movie.VoteAverage = voteAverage != "" ? voteAverage : "";

                        List<string> genresIdList = new List<string>();
                        foreach (var genre in item["genre_ids"])
                        {
                            genresIdList.Add(genre.ToString());
                        }

                        movie.GenreIdList = genresIdList;
                        viewModel.Media.Add(movie);


                    }

                    return viewModel;


                }
                else
                {
                    throw new Exception();
                }
            }

        }

     


        #endregion


    }
}
