using DataAccess.Entities;
using InternetShop.DataAccess.Entities;
using System;
using System.Collections.Generic;

namespace LogicLayer.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? Date { get; set; }
        public decimal Total { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }
        public virtual ICollection<Detail> Details { get; set; }
    }
}
