using ETickets.Data.Enums;
using ETickets.Models;

namespace ETickets.ViewModels
{
    public class MovieWithCinemaCategoryViewModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
      
        public double MoviePrice { get; set; }
        public string MovieImgUrl { get; set; }

        public string MovieTrailerUrl { get; set; }
        public DateTime MovieStartDate { get; set; }
        public DateTime MovieEndDate { get; set; }
        public  string MovieDescription { get; set; }

        public MovieStatus MovieStatus { get; set; }

        public string CinemaName { get; set; }
        
        public int CinemaId { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName{ get; set; }
        public int Views { get; set; }
    }
}
