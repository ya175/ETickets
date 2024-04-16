using ETickets.Data;
using ETickets.IRepositry;
using ETickets.Models;

namespace ETickets.Repositry
{
    public class CartItemRepositry: ICartItemRepositry
    {
        ApplicationDbContext context;//= new ApplicationDbContext();
   
        IMovieRepositry movieRepositry;

        public CartItemRepositry(ApplicationDbContext context,IMovieRepositry movieRepositry)
        {
            this.context = context;
            this.movieRepositry= movieRepositry;
        }


        public CartItem Delete(CartItem item)
        {
            var removedItem = FindById(item);

            if (removedItem != null)
            {
                 context.Set<CartItem>().Remove(removedItem);

                  context.SaveChanges();
            }

            return removedItem;
        }
       
        public void Update(CartItem item, int _Quantity)
        {
            var oldItem = FindById(item);

            if (oldItem != null)
            {
                oldItem.Quantity = _Quantity;
                context.SaveChanges();

            }

        }
        


        
        public CartItem FindById(CartItem item)
        {
            return context.Set<CartItem>().Where(e => e.CartId == item.CartId && e.MovieId == item.MovieId).FirstOrDefault();

        }
    }
}
