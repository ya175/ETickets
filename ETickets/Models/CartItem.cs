namespace ETickets.Models
{
    public class CartItem
    {

        public int CartId { get; set; }
        public int MovieId { get; set; }
        public int Quantity { get; set; }
        public Cart Cart { get; set; }
        public Movie Movie { get; set; }


    }
}
