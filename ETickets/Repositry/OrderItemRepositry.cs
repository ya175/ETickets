using ETickets.Data;
using ETickets.IRepositry;
using ETickets.Models;

namespace ETickets.Repositry
{
    public class OrderItemRepositry : IOrderItemRepositry
    {
        ApplicationDbContext context;//= new ApplicationDbContext();

        public OrderItemRepositry(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(OrderItem orderItem)
        {
            var item = FindById(orderItem);
            if(item == null) {

                context.OrderItems.Add(orderItem);
                 context.SaveChanges();
            }

        }
        public OrderItem FindById(OrderItem item)
        {
        return context.OrderItems.SingleOrDefault(am => am.OrderId == item.OrderId&& am.MovieId == item.MovieId);
    
        }

     public  List<OrderItem> ReadByOrderId(int id)
        {
            return context.OrderItems.Where(e=>e.OrderId == id).ToList(); 
        }
    }
}
