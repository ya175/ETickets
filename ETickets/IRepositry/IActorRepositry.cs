using ETickets.Data;
using ETickets.Models;

namespace ETickets.IRepositry
{
    public interface IActorRepositry
    {
        

        public void Delete(int id);

        public void Update(Actor actor);
        
        public Actor GetById(int id);
        public List<Actor> GetAll();

        public void Create(Actor actor);




    }
}
