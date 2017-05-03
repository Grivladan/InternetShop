using Ninject.Modules;
using LogicLayer.Interfaces;
using LogicLayer.Services;

namespace InternetShop.Infrastructure
{
    public class WebHostModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IProductService>().To<ProductService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<ICartService>().To<CartService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}