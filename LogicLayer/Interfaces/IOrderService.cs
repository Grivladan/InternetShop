using DataAccess.Entities;

namespace LogicLayer.Interfaces
{
    public interface IOrderService
    {
        void Create(Order order);
        void Update(Order order);
    }
}
