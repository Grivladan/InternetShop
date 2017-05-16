using DataAccess.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "category name is required")]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}