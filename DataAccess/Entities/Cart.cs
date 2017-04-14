using DataAccess.Interfaces;
using System;

namespace DataAccess.Entities
{
    public class Cart : IEntity
    {
        public string SessionId { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }
        public virtual Book Book { get; set; }
    }
}
