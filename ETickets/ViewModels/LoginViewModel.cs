using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace ETickets.ViewModels
{
    public class LoginViewModel
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }


    }
}
