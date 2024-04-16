using ETickets.Data;
using ETickets.Models;

namespace ETickets.Repositry
{
    public class ActorMovieRepositry : IActorMovieRepositry
    {

        ApplicationDbContext context;
        public ActorMovieRepositry(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(ActorMovie actorMovie)
        {
            var AddedMovie = FindById(actorMovie);

            if (AddedMovie == null)
            {
                context.Set<ActorMovie>().Add(actorMovie);
                
            }

            context.SaveChanges();
        }

        public void Delete(ActorMovie actorMovie)
        {
            var removedMovie = FindById(actorMovie);

            if (removedMovie != null)
            {
                context.Set<ActorMovie>().Remove(removedMovie);

                context.SaveChanges();
            }
        }

        public ActorMovie FindById(ActorMovie actorMovie)
        {
           return  context.Set<ActorMovie>()
                       .SingleOrDefault(am => am.MoviesId == actorMovie.MoviesId && am.ActorsId == actorMovie.ActorsId);

        }
    }
}
