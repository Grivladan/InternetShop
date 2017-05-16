using DataAccess.Entities;
using InternetShop.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace InternetShop.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(160)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(160)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(70)]
        public string Adress { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(40)]
        [RegularExpression(@"([a-zA-Z]+|[a-zA-Z]+\\s[a-zA-Z]+)", ErrorMessage = "Entered country name is not valid.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [StringLength(40)]
        [RegularExpression(@"([a-zA-Z]+|[a-zA-Z]+\\s[a-zA-Z]+)", ErrorMessage = "Entered country name is not valid.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Email Address is required")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Email is is not valid.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"/^[0-9()-]+$/", ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }

        public DateTime? Date { get; set; }
        public decimal Total { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public virtual ApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }
        public virtual ICollection<Detail> Details { get; set; }
    }
}