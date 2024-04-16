using ETickets.IRepositry;
using ETickets.Models;
using ETickets.Repositry;
using ETickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ETickets.Controllers
{
    public class CinemaController : Controller
    {
        ICinemaRepositry cinemaRepositry;//=new CinemaRepositry();
        IMovieRepositry movieRepositry;//=new MovieRepositry();

        public CinemaController(ICinemaRepositry cinemaRepositry, IMovieRepositry movieRepositry)
        {
            this.cinemaRepositry = cinemaRepositry;
            this.movieRepositry = movieRepositry;

        }
  
        
        private MovieWithCinemaCategoryViewModel Map(Movie movie)
        {
            return new MovieWithCinemaCategoryViewModel
            {
                MovieId = movie.Id,
                MovieName = movie.Name,
                MoviePrice = movie.Price,
                MovieImgUrl = movie.ImgUrl,
                MovieTrailerUrl = movie.TrailerUrl,
                MovieStartDate = movie.StartDate,
                MovieEndDate = movie.EndDate,
                MovieStatus = movie.MovieStatus,
                CinemaName = movie.Cinema.Name,
                CinemaId = movie.CinemaId,
                CategoryId = movie.CategoryId,
                CategoryName = movie.Category.Name,
                MovieDescription = movie.Description
            };
        }

        public IActionResult Index()
        {
            List<Cinema>cinemas= cinemaRepositry.GetAll();
            return View(cinemas);

        }


        public IActionResult GetCinemaDetails(int id)
        {

            Cinema cinema=cinemaRepositry.GetById(id);
            List<MovieWithCinemaCategoryViewModel> movies = movieRepositry.SearchByCinemaIdlWithCinemaCategory(id).Select(Map).ToList();
            ViewData["movies"]=movies;

            return View(cinema);
        }
    }
}
