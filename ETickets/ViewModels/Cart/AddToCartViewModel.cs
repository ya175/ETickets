using System.ComponentModel.DataAnnotations;

namespace ETickets.ViewModels.Cart
{
    public class AddToCartViewModel
    {
        [Key]
        public int MovieId { get; set; }
        public int CartId { get; set; }

        [Display(Name = "Movie Name")]
        

        public string MovieName { get; set; }

        [Display(Name = "Ticket Price")]
        public double MoviePrice { get; set; }
        public int Quantity { get; set; }


    }
}
