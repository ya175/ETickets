using ETickets.Data.Enums;
using ETickets.IRepositry;
using ETickets.Models;
using ETickets.ViewModels;
using ETickets.ViewModels.Cart;
using ETickets.ViewModels.Category;
using ETickets.ViewModels.Movie;

namespace ETickets.Services
{
   static public class MapRepositry 
    {


        static public MovieWithCinemaCategoryViewModel Map(Movie movie)
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
                MovieDescription = movie.Description,
                Views = movie.Views

            };


        }
        static public CreateEditMovieViewModel MapToEditMovieViewModel(Movie movie)
        {
            return new CreateEditMovieViewModel
            {
                MovieId = movie.Id,
                MovieName = movie.Name,
                MoviePrice = movie.Price,
                MovieImgUrl = movie.ImgUrl,
                MovieTrailerUrl = movie.TrailerUrl,
                MovieStartDate = movie.StartDate,
                MovieEndDate = movie.EndDate,
                CinemaId = movie.CinemaId,
                CategoryId = movie.CategoryId,
                MovieDescription = movie.Description,


            };


        }
        static public Movie MapToMovie(CreateEditMovieViewModel movie)
        {
            return new Movie
            {

                Id = movie.MovieId,
                Name = movie.MovieName,
                Price = movie.MoviePrice,
                ImgUrl = movie.MovieImgUrl,
                TrailerUrl = movie.MovieTrailerUrl,
                StartDate = movie.MovieStartDate,
                EndDate = movie.MovieEndDate,
                Description = movie.MovieDescription,
                CinemaId = movie.CinemaId,
                CategoryId = movie.CategoryId
            };


        }
        static public CreateEditViewModel MapToCreateEditViewModel(Category category)
        {
            return new CreateEditViewModel
            {
                Id = category.Id,
                Name = category.Name
            };
        }





        static public Category MapToCategory(CreateEditViewModel categoryVm)
        {
            return new Category
            {
                Id = categoryVm.Id,
                Name = categoryVm.Name
            };

        }

        static public AddToCartViewModel MapToAddTOCart(CartItem item)
        {

            return new AddToCartViewModel
            {
                CartId=item.CartId,
                MovieId = item.Movie.Id,
                MovieName = item.Movie.Name,
                MoviePrice = item.Movie.Price,
                Quantity = item.Quantity

            };
        }
    }
}
