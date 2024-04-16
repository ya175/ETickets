using ETickets.Models;

namespace ETickets.IRepositry
{
    public interface IOrderItemRepositry
    {
        //public void Delete(int id);

        //public void Update(Cinema cinema);

        //public Cinema GetById(int id);
        //public List<Cinema> GetAll();

        public void Create(OrderItem item);
        List<OrderItem> ReadByOrderId(int id);
    }
}
