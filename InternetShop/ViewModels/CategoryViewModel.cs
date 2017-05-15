using DataAccess.Entities;
using System.Collections.Generic;

namespace InternetShop.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}