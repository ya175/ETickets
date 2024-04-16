using ETickets.Models;

namespace ETickets.IRepositry
{
    public interface ICartItemRepositry
    {


        public CartItem Delete(CartItem item);

        public CartItem FindById(CartItem item);

        public void Update(CartItem item, int _Quantity);
    }
}
