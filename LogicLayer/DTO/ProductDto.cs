using DataAccess.Entities;
using System;

namespace LogicLayer.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public byte[] Image { get; set; }
    }
}
