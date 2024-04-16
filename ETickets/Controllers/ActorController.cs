using ETickets.Data;
using ETickets.IRepositry;
using ETickets.Models;
using ETickets.Repositry;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ETickets.Controllers
{
    public class ActorController : Controller
    {


        IActorRepositry actorRepositry;

        public ActorController(IActorRepositry actorRepositry)
        {
            this.actorRepositry = actorRepositry;
            
        }
    
        public IActionResult Index()
        {

            return View();
        }

        [Authorize]
        public IActionResult GetOneActor(int id)
        {
            Actor? actor = actorRepositry.GetById(id);
                //context.Actors.FirstOrDefault(e=>e.Id==id);

            return View(actor);
        }
    }
}
