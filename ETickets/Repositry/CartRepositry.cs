using ETickets.Controllers;
using ETickets.Data;
using ETickets.IRepositry;
using ETickets.Models;
using Microsoft.EntityFrameworkCore;

namespace ETickets.Repositry
{
    public class CartRepositry : ICartRepositry
    {
        ApplicationDbContext context;
        ICartItemRepositry cartItemRepositry;
        public CartRepositry(ApplicationDbContext context,      ICartItemRepositry cartItemRepositry)
        {

            this.context = context;
            this.cartItemRepositry=cartItemRepositry;
        }



        //create Cart

        public void Create(Cart cart)
        {

            context.Carts.Add(cart);

            context.SaveChanges();
            
        }

        public Cart FindById(int id)
        {
            Cart cart = context.Carts.Find(id);
            return cart;
        }

        public Cart FindByUser(string id)
        {
            return context.Carts.FirstOrDefault(c => c.UserId == id);

        }

        public void AddMovie(Movie movie, int cartId, int Quantity)
        {


            CartItem cartItem= new CartItem() { CartId=cartId, Quantity=Quantity,MovieId=movie.Id};

            CartItem item = cartItemRepositry.FindById(cartItem);

            if (item == null)
            {
                context.Set<CartItem>().Add(cartItem);
                context.SaveChanges();
                UpdateTotal(Quantity,movie.Price,cartId);

            }

         }

        public void UpdateTotal(int Quantity,double price,int cartId)
        {

            var cart = context.Carts.Find(cartId);
            cart.Total += price * Quantity;
            context.SaveChanges();

        }

        public List<CartItem> ReadCartitems(int cartId)
        {

               return context.Set<CartItem>().Where(e=>e.CartId==cartId).Include(e=>e.Movie).ToList();
        
        }

        public void Delete( int id)
        {

          Cart cart = context.Carts.Find(id);
            context.Carts.Remove(cart);
            context.SaveChanges();
        }
    }
}
