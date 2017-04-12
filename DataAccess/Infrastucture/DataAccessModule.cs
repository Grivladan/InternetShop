using Ninject.Modules;
using DataAccess.Interfaces;
using DataAccess.Repository;
using Ninject.Extensions.Factory;
using InternetShop.DataAccess.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccess.Infrastucture
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepositoryFactory>().ToFactory();
            Bind<ApplicationDbContext>().ToSelf();
            Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>();
            Bind<UserManager<ApplicationUser>>().ToSelf();
            Bind(typeof(IRepository<>)).To(typeof(Repository<>));
        }
    }
}
