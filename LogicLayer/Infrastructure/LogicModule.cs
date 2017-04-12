using System;
using Ninject.Modules;
using DataAccess.Interfaces;
using DataAccess.Repository;

namespace LogicLayer.Infrastructure
{
    public class LogicModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
