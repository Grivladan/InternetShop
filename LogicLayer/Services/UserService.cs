using LogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InternetShop.DataAccess.Entities;
using DataAccess.Interfaces;

namespace LogicLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            var users = _unitOfWork.UserManager.Users;
            return users;
        }
    }
}
