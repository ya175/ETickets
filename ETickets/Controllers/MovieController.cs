using ETickets.Data;
using ETickets.IRepositry;
using ETickets.Models;
using ETickets.Repositry;
using ETickets.Services;
using ETickets.ViewModels;
using ETickets.ViewModels.Movie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ETickets.Controllers
{

    [Authorize(Roles = "Admin")]

    public class MovieController : Controller
    {

        ApplicationDbContext context;// = new ApplicationDbContext();
       IMovieRepositry movieRepositry;//= new MovieRepositry();
        ICinemaRepositry cinemaRepositry;//=new CinemaRepositry(); 
        ICategoryRepositry categoryRepositry;//=new CategoryRepositry();
        
        IActorRepositry actorRepositry;


        public MovieController(IMovieRepositry movieRepositry, ICinemaRepositry cinemaRepositry, ICategoryRepositry categoryRepositry, ApplicationDbContext context,  IActorRepositry actorRepositry)
        {

            this.movieRepositry = movieRepositry;
            this.cinemaRepositry= cinemaRepositry;
            this.categoryRepositry= categoryRepositry;
            this.context= context;
            this.actorRepositry = actorRepositry;
        }


        [AllowAnonymous]
        public IActionResult Index()
        {
            List<MovieWithCinemaCategoryViewModel> movies = movieRepositry.ReadAllWithCinemaCategory().Select(MapRepositry.Map).ToList();

            //return movies.Select(Map).ToList();
            return View(movies);
        }


        [AllowAnonymous]
        public IActionResult GetDetails(int id)
        {

            MovieWithCinemaCategoryViewModel movie =MapRepositry. Map(movieRepositry.ReadOneWithCinemaCategory(id));
            // call update method 
            
            movieRepositry.Updateviews(id);

            var Actors = context.Set<ActorMovie>().Where(e => e.MoviesId == id).Select(e => new Actor()
            {
                FirstName = e.Actor.FirstName
                ,
                LastName = e.Actor.LastName
                ,
                Bio = e.Actor.Bio
            ,
                ProfilePicture = e.Actor.ProfilePicture
            ,
                Id = e.Actor.Id
            ,
                News = e.Actor.News
             ,

            }).ToList();

            ViewData["Actors"] = Actors;
            return View(movie);
        }


        [AllowAnonymous]
        public IActionResult FindMoviesByCategoryId(int id)
        {
         
            ViewData["CategoryName"] =categoryRepositry.GetById(id).Name;

            List <MovieWithCinemaCategoryViewModel> movies=movieRepositry.SearchByCategoryId(id).Select(MapRepositry.Map).ToList();
            return View( movies);


        }


        [AllowAnonymous]
        public IActionResult SearchByName(string Name)
        {
            List<MovieWithCinemaCategoryViewModel> movies = movieRepositry.SeacrhByNameWithCinemaAndCategory(Name).Select(MapRepositry.Map).ToList();

            if (movies.Count == 0)
                 return View("MovieNotFound");
            
            return View(movies);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            Movie movie = movieRepositry.ReadOneWithCinemaCategoryActors(id);
            CreateEditMovieViewModel  EditMovieVm =MapRepositry.MapToEditMovieViewModel(movie);



            ViewData["Cinemas"] =  cinemaRepositry.GetAll();

            ViewData["Categories"] = categoryRepositry.GetAll();
            ViewData["SelectedActors"] = movie.Actors;

            var actors = actorRepositry.GetAll();
           
            ViewData["Actors"] = actors;
         
            return View(EditMovieVm);
        }


        [HttpPost]
        public IActionResult Edit(CreateEditMovieViewModel movie, List<int> SelectedActors)
        {
            //map
            if (ModelState.IsValid)
            {

                Movie mappedMovie = MapRepositry.MapToMovie(movie);
                movieRepositry.Update(mappedMovie, SelectedActors);

            return RedirectToAction("Index");

            }

            return View("Edit", movie);
        }



        public IActionResult EditActors(int Id, List<int> SelectedActors)
        {
            var currentActors = movieRepositry.ReadOneWithCinemaCategoryActors(Id).Actors.ToList();


            var selectedActors = SelectedActors; //IDs

            //////remove uncheked

            foreach (Actor actor in currentActors)
            {

                if (!selectedActors.Contains(actor.Id))
                {
                    //context.Set<ActorMovie>().Remove(new ActorMovie { MoviesId = Id, ActorsId = actor.Id });


                    var actorMovieToRemove = context.Set<ActorMovie>()
                .SingleOrDefault(am => am.MoviesId == Id && am.ActorsId == actor.Id);

                    if (actorMovieToRemove != null)
                    {
                        context.Set<ActorMovie>().Remove(actorMovieToRemove);


                        context.SaveChanges();
                    }
                }
            }

            /// add selectedActors
            /// 
            foreach (int actorId in selectedActors)
            {

                // context.Set<ActorMovie>().Add(new ActorMovie { MoviesId = Id, ActorsId = actorId });


                // Check if the ActorMovie entry already exists
                var existingActorMovie = context.Set<ActorMovie>()
                    .SingleOrDefault(am => am.MoviesId == Id && am.ActorsId == actorId);

                if (existingActorMovie == null)
                {
                    // Create a new ActorMovie entry if it doesn't exist
                    context.Set<ActorMovie>().Add(new ActorMovie { MoviesId = Id, ActorsId = actorId });
                    context.SaveChanges();
                }

            }

            return RedirectToAction("Index");

        }


        [HttpGet]
        public IActionResult Create()
        {

            ViewData["Cinemas"] = cinemaRepositry.GetAll();

            ViewData["Categories"] = categoryRepositry.GetAll();

            ViewData["Actors"] = actorRepositry.GetAll();

            CreateEditMovieViewModel movie = new CreateEditMovieViewModel();

            return View(movie);
        }

        [HttpPost]
        public IActionResult Create(CreateEditMovieViewModel movie, List<int> SelectedActors)
        {
            if (ModelState.IsValid)
            {
                Movie mappedMovie = MapRepositry.MapToMovie(movie);
                movieRepositry.Create(mappedMovie, SelectedActors);
 
                return RedirectToAction("Index");
            }
            return View("create",movie);
        }


        public IActionResult ManageMovies()
        {
            //movies
            List<Movie> movies = movieRepositry.ReadAllWithCinemaCategory();

            return View(movies);
        }

   
    }
}
            

//ServiceFolder 


