namespace ETickets.Models
{
    public class OrderItem
    {

        public int OrderId { get; set; }
        public int MovieId { get; set; }      
        public int Quantity { get; set; }
        public DateTime TicketDate { get; set; }
        public double Price { get; set; }
        
        public Order Order { get; set; }
        public Movie Movie { get; set; }


    }
}