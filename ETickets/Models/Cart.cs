﻿namespace ETickets.Models
{
    public class Cart
    {

        public int Id { get; set; }

        public string UserId { get; set; }
        public double Total { get; set; }
        public ApplicationUser User { get; set; }
        public List<Movie> Movies { get; set; }

    }
}
