using ETickets.Data.Enums;
using ETickets.Models;
using System.ComponentModel.DataAnnotations;

namespace ETickets.ViewModels.Movie
{
    public class CreateEditMovieViewModel
    {
        public int MovieId { get; set; }

        [Display(Name ="Movie Name")]
        [Required]
        public string MovieName { get; set; }


        [Required]
        [Display(Name = "Ticket Price")]
        [Range(100,500)]
        public double MoviePrice { get; set; }


        [Required]
        [Display(Name = "Image URL")]
        public string MovieImgUrl { get; set; }


        [Required]
        [Display(Name = "Video URL")]
        public string MovieTrailerUrl { get; set; }


        [Required]
        [Display(Name = "Start Date")]
        public DateTime MovieStartDate { get; set; }


        [Required]
        [Display(Name = "End Date")]
        public DateTime MovieEndDate { get; set; }


        [Required]
        [Display(Name = "Descreption")]
        public string MovieDescription { get; set; }


        [Display(Name = " Choose Cinema")]
        public int CinemaId { get; set; }


        [Display(Name = "Choose Category")]
        public int CategoryId { get; set; }


        public int Views { get; set; }

    }
}
