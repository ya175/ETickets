using ETickets.Data;
using ETickets.Models;

namespace ETickets.IRepositry
{
    public interface IOrderRepositry
    {
        public void Delete(int id);

        //public void Update(Order order);

        public Order GetById(int id);
        public List<Order> GetAll();

        public void Create(Order order);
    }
}
