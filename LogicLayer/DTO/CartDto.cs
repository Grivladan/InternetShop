using DataAccess.Entities;
using System;

namespace LogicLayer.DTO
{
    public class CartDto
    {
        public string SessionId { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public virtual Product Product { get; set; }
    }
}
