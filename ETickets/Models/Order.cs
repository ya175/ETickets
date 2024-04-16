﻿namespace ETickets.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public double TotalAmount { get; set; }
        //public string PaymentStatus { get; set; }
        public DateTime OrderDate { get; set; }


        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
