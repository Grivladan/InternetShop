﻿using DataAccess.Entities;

namespace LogicLayer.DTO
{
    public class DetailDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }
        public virtual Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
