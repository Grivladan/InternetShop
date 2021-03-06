﻿using DataAccess.Interfaces;
using System;

namespace DataAccess.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public byte[] Image { get; set; } 

        public Product()
        {
            Date = DateTime.Now;
        }
    }
}
