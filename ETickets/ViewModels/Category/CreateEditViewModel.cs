using System.ComponentModel.DataAnnotations;

namespace ETickets.ViewModels.Category
{
    public class CreateEditViewModel
    {

        public int Id { get; set; }

        [Required]
         public string Name { get; set; }
    }

}
 