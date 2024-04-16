using ETickets.Data;
using ETickets.IRepositry;
using ETickets.Models;

namespace ETickets.Repositry
{
    public class OrderRepositry: IOrderRepositry
    { 
    ApplicationDbContext context;//= new ApplicationDbContext();

    public OrderRepositry(ApplicationDbContext context)
    {
        this.context = context;
    }


    public void Create(Order order)
    {
        context.Orders.Add(order);

        context.SaveChanges();
    }

    public void Delete(int id)
    {
        Order order = context.Orders.Find(id);

        if (order != null)
        {
            context.Orders.Remove(order);
            context.SaveChanges();
        }

    }

    public List<Order> GetAll()
    {
        return context.Orders.ToList();
    }

    public Order GetById(int id)
    {

        return context.Orders.Find(id);
    }





    //public void Update(Order _cinema)
    //{

    //    Order order = context.Orders.Find(_cinema.Id);

    //    if (order != null)
    //    {
    //        order.Name = _cinema.Name;
    //        order.Address = _cinema.Address;
    //        order.Description = _cinema.Description;
    //        order.CinemaLogo = _cinema.CinemaLogo;

    //        context.SaveChanges();
    //    }

    //}
}

}
