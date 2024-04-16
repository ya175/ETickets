using ETickets.Data;
using ETickets.IRepositry;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repositry
{
    public class CinemaRepositry:ICinemaRepositry
    {
        ApplicationDbContext context;//= new ApplicationDbContext();
        
        public CinemaRepositry(ApplicationDbContext context)
        {
            this.context=context;
        }
        public void Create(Cinema cinema)
        {
            context.Cinemas.Add(cinema);

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            Cinema cinema = context.Cinemas.Find(id);

            if (cinema != null)
            {
                context.Cinemas.Remove(cinema);
                context.SaveChanges();
            }

        }

        public List<Cinema> GetAll()
        {
            return context.Cinemas.ToList();
        }

        public Cinema GetById(int id)
        {

            return context.Cinemas.Find(id);
        }





        public void Update(Cinema _cinema)
        {
            
            Cinema cinema = context.Cinemas.Find(_cinema.Id);

            if (cinema != null)
            {
                cinema.Name = _cinema.Name;
                cinema.Address = _cinema.Address;
                cinema.Description = _cinema.Description;
                cinema.CinemaLogo = _cinema.CinemaLogo;

                context.SaveChanges();
            }

        }
    }
}
