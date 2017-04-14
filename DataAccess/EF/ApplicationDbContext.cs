using DataAccess.Entities;
using InternetShop.DataAccess.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext()
        : base("DefaultConnection")
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Detail> Details { get; set; }
    public DbSet<Cart> Carts { get; set; }  

    public static ApplicationDbContext Create()
    {
        return new ApplicationDbContext();
    }
}
