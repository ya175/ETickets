using ETickets.Models;

namespace ETickets.Repositry
{
    public interface IActorMovieRepositry
    {

        public void Delete(ActorMovie actorMovie);
        public void Create(ActorMovie actorMovie);

        public ActorMovie FindById(ActorMovie actorMovie);
    }
}