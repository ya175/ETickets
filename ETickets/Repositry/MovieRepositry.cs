using ETickets.Data;
using ETickets.Data.Enums;
using ETickets.IRepositry;
using ETickets.Models;
using ETickets.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ETickets.Repositry
{
    public class MovieRepositry:IMovieRepositry
    {
        ApplicationDbContext context;// = new ApplicationDbContext();

        IActorMovieRepositry actorMovieRepositry;
        public MovieRepositry(ApplicationDbContext context, IActorMovieRepositry actorMovieRepositry)
        {
            this.context=context;
            this.actorMovieRepositry=actorMovieRepositry;
        }   

        public void Delete(int id)
        {
            var movie = context.Movies.Find(id);
            if(movie != null)
            {
                context.Movies.Remove(movie);
                context.SaveChanges();
            }

        }
        
        public void Update(Movie movie ,List<int>selectedActors)
        {

            var res = context.Movies.Find(movie.Id);

            if (res != null)
            {
                res.Price = movie.Price;
                res.CategoryId = movie.CategoryId;
                res.CinemaId = movie.CinemaId;
                res.Description = movie.Description;
                res.EndDate = movie.EndDate;
                res.StartDate = movie.StartDate;
                res.ImgUrl = movie.ImgUrl;
                res.Name = movie.Name;
                res.TrailerUrl = movie.TrailerUrl;


                var currentActors = ReadOneWithCinemaCategoryActors(movie.Id).Actors.ToList();

                //////remove uncheked

                foreach (Actor actor in currentActors)
                {

                    if (!selectedActors.Contains(actor.Id))
                    {
                        actorMovieRepositry.Delete(new ActorMovie { MoviesId = movie.Id, ActorsId = actor.Id });

                    }
                }

                /// add new selectedActors
                
                foreach (int actorId in selectedActors)
                {

                    actorMovieRepositry.Create(new ActorMovie { MoviesId = movie.Id, ActorsId = actorId });

                }
                context.SaveChanges();
            }
        }
      
        public void Create(Movie movie, List<int> SelectedActors)
        {

            context.Movies.Add(movie);

            context.SaveChanges();
            foreach(var actorMovieId in SelectedActors)
            {
                actorMovieRepositry.Create(new ActorMovie { MoviesId = movie.Id, ActorsId = actorMovieId });

            }

            
        
        }


        //Get All
        public List<Movie> ReadAllWithCinemaCategory()
        {
            List<Movie> movies = context.Movies.Include(e => e.Cinema).Include(e => e.Category).ToList();

            return movies;
        }

        public List<Movie> ReadAll()
        {

            return context.Movies.ToList();
        }
        //Get 
        public Movie ReadOneById(int id)
        {

            return context.Movies.Find(id);

        }
        public List<Movie> SearchByCategoryId(int id)
        {
            List<Movie> movies = context.Movies.Where(e => e.CategoryId == id).Include(e => e.Cinema).Include(e => e.Category).ToList();
           

            return movies;


        }
        public Movie ReadOneWithCinemaCategory(int id)
        {
            Movie? movie = context.Movies.Where(e => e.Id == id).Include(e => e.Cinema).Include(e => e.Category).FirstOrDefault();
            return movie;
        }
        public Movie ReadOneWithCinemaCategoryActors(int id)
        {
            Movie? movie = context.Movies.Where(e => e.Id == id).Include(e => e.Cinema).Include(e => e.Category).Include(e=>e.Actors).FirstOrDefault();
            return movie;
        }
        public List<Movie> SearchByCinemaIdlWithCinemaCategory(int id)
        {
            List<Movie> movies = context.Movies.Where(e => e.CinemaId == id).Include(e => e.Cinema).Include(e => e.Category).ToList();
          

            return movies;
        }

        public List<Movie> SeacrhByNameWithCinemaAndCategory(string _Name)
        {

            List<Movie> movies = context.Movies.Where(e => e.Name.Contains(_Name)).Include(e => e.Cinema).Include(e => e.Category).ToList();
           

            return movies;

        }

        void IMovieRepositry.Updateviews(int id)
        {
            Movie movie = ReadOneById(id);
            movie.Views++;
            context.SaveChanges();

        }
    }
}
