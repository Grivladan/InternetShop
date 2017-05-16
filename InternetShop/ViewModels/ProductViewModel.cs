using DataAccess.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string Name { get; set; }
        public virtual Category Category { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        public int CategoryId { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        public DateTime? Date { get; set; }
        public byte[] Image { get; set; }
    }
}