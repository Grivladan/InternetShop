using DataAccess.Interfaces;
using InternetShop.DataAccess.Entities;

namespace DataAccess.Entities
{
    public class Detail : IEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public virtual Book Book { get; set; }
        public int BookId { get; set; }
        public virtual Order Order { get; set; }
    }
}
