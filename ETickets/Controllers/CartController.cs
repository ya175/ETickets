
using ETickets.IRepositry;
using ETickets.Models;
using ETickets.Repositry;
using ETickets.Services;
using ETickets.ViewModels;
using ETickets.ViewModels.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ETickets.Controllers
{
    public class CartController : Controller
    {
        IMovieRepositry movieRepositry;
        ICartRepositry cartRepositry;
        ICartItemRepositry cartItemRepositry;
        IOrderRepositry orderRepositry;
        IOrderItemRepositry orderItemRepositry;
        UserManager<ApplicationUser> userManager;
        
        private readonly IEmailSender emailSender;

        public CartController(IMovieRepositry movieRepositry, ICartRepositry cartRepositry, UserManager<ApplicationUser> userManager, IEmailSender emailSender, IOrderRepositry orderRepositry, IOrderItemRepositry orderItemRepositry,
        ICartItemRepositry cartItemRepositry)
        {
            this.movieRepositry = movieRepositry;
            this.cartRepositry = cartRepositry;
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.orderRepositry = orderRepositry;
            this.orderItemRepositry=orderItemRepositry;
            this.cartItemRepositry=cartItemRepositry;
        }

        [Authorize]

        public IActionResult Index()
        {
            var userId = GetCurrentUserIdAsync().Result;

            Cart cart = cartRepositry.FindByUser(userId);
            List<AddToCartViewModel> cartItemsVm = new List<AddToCartViewModel>();
            if (cart != null)
            {
                List<CartItem> cartItems = cartRepositry.ReadCartitems(cart.Id);

                cartItemsVm = cartItems.Select(MapRepositry.MapToAddTOCart).ToList();

                ViewData["cartId"] = cart.Id;
            }

            return View(cartItemsVm);

        }
        [Authorize]

        private async Task<string> GetCurrentUserIdAsync()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            return await userManager.GetUserIdAsync(user);
        }

        [Authorize]
        public IActionResult AddToCart(int id)
        {
            var userId = GetCurrentUserIdAsync().Result;
            
            //find user cart
            Cart cart = cartRepositry.FindByUser(userId);


            if(cart==null)
            {
                //create new cart
                cart = new Cart { UserId = userId };
                cartRepositry.Create(cart);
                cart = cartRepositry.FindByUser(userId);
               }




            //Add Movie ToCart
            int quantity = 1;
            Movie movie = movieRepositry.ReadOneById(id);
            cartRepositry.AddMovie(movie,cart.Id,quantity);


            //// ReadAll && Map
            //List<CartItem> cartItems = cartRepositry.ReadCartitems(cart.Id);

            //List<AddToCartViewModel> cartItemsVm = cartItems.Select(MapRepositry.MapToAddTOCart).ToList();

            //ViewData["cartId"] = cart.Id;
            ////return View(movies);
            //return View(cartItemsVm);

            return RedirectToAction("Index");
        
        }

        [Authorize]

        //[HttpPost]
        public async Task<IActionResult> Checkout(int id)
        {
            int cartId = id;
            Cart cart = cartRepositry.FindById(cartId);

            List<CartItem> cartItems = cartRepositry.ReadCartitems(cartId);

            if(cartItems==null)
            {
                return RedirectToAction("Index", "Movie");
            }

            // suppose payment is successfully done

            // create Order

            Order order = new Order() { OrderDate = DateTime.Now, TotalAmount = cart.Total, UserId = cart.UserId };
            
            
            orderRepositry.Create(order);

            //add orderItems
            foreach(var item in cartItems)
            {

                //TicketDate
                TimeSpan avialableTime = item.Movie.StartDate - DateTime.Now;
                DateTime movieTicketDate;
                if (avialableTime >= TimeSpan.FromHours(5))
                {
                    movieTicketDate = DateTime.Now;

                    
                }
                else movieTicketDate = DateTime.Now.AddDays(1);

                var orderItem = new OrderItem() { OrderId = order.Id, Quantity = item.Quantity, MovieId = item.MovieId ,TicketDate=movieTicketDate};
                
                orderItemRepositry.Create(orderItem);


            }

            //First Remove CartItems 

            foreach (CartItem item in cartItems)
            {
                cartItemRepositry.Delete(item);
            
            }

            //Second remove cart

            cartRepositry.Delete( cart.Id);


            //ListOf OrderItems
            List<OrderItem> orderItems = orderItemRepositry.ReadByOrderId(order.Id);
            //string email, string subject, string message

            string email = "yassminemohamad2001@gmail.com";
            string subject = "Order Details";
            string message = $"Order id: {order.Id}\n.Order Total amount :{order.TotalAmount}\n\n";



            foreach (OrderItem item in orderItems) {
                message += $"Movie : {item.Movie.Name} , Quantity :{item.Quantity} , Tickets Date : {item.TicketDate}.\n ";
            
            }


            await emailSender.SendEmailAsync(email, subject, message);



            return RedirectToAction("Index");
        }

        [Authorize]

        public IActionResult DeleteItem(int cartId, int movieId)
        {

            CartItem item = new CartItem() { CartId = cartId, MovieId = movieId };
            // count subtotal
           
            var removedItem= cartItemRepositry.Delete(item);
            // upadate total in cart
            double moviePrice = movieRepositry.ReadOneById(removedItem.MovieId).Price;

            cartRepositry.UpdateTotal(removedItem.Quantity * -1, moviePrice, removedItem.CartId);

            return RedirectToAction("Index");

        }
        [Authorize]

        public IActionResult UpdateItemQuantity(int cartId, int movieId, int Quantity)
        {

            CartItem item = new CartItem() { CartId = cartId, MovieId = movieId };

            var oldQuantity = cartItemRepositry.FindById(item).Quantity;

            cartItemRepositry.Update(item, Quantity);




            // count subtotal
            // upadate total in cart

            double moviePrice = movieRepositry.ReadOneById(movieId).Price;

            cartRepositry.UpdateTotal(Quantity-oldQuantity, moviePrice, cartId);
            
            return RedirectToAction("Index");

        }
    }

}