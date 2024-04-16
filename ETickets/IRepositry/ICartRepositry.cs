using ETickets.Models;

namespace ETickets.IRepositry
{
    public interface ICartRepositry
    {
        public Cart FindById(int id);
        public void Create(Cart cart);


        Cart FindByUser(string id);
        public void AddMovie(Movie movie, int cartId, int Quantity);
        public List<CartItem> ReadCartitems(int cartId);
        public  void UpdateTotal(int Quantity, double price, int cartId);
        public void Delete(int id);
    }
}