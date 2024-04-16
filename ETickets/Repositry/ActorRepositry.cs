using ETickets.Controllers;
using ETickets.Data;
using ETickets.IRepositry;
using ETickets.Models;

namespace ETickets.Repositry
{
    public class ActorRepositry : IActorRepositry
    {
        ApplicationDbContext context;//= new ApplicationDbContext();


        public ActorRepositry(ApplicationDbContext context)
        {
            this.context = context;
            
        }
        public void Create(Actor actor)
        {
            context.Actors.Add(actor);

            context.SaveChanges();
         }

        public void Delete(int id)
        {
            Actor actor = context.Actors.Find(id);
            if (actor != null)
            {
                context.Actors.Remove(actor);
                context.SaveChanges();
            }

        }

        public List<Actor> GetAll()
        {
            return context.Actors.ToList();
        }

        public Actor GetById(int id)
        {

            return context.Actors.Find(id);
        }
        
        public void Update(Actor _actor)
        {


            Actor actor = context.Actors.Find(_actor.Id);

            actor.FirstName=_actor.FirstName;
            actor.LastName=_actor.LastName;
            actor.ProfilePicture=_actor.ProfilePicture;
            actor.Bio=_actor.Bio;
            actor.News=_actor.News;

            context.SaveChanges();
        }
    }
}
