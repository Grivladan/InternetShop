using DataAccess.Interfaces;
using InternetShop.DataAccess.Entities;
using LogicLayer.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

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

        public void AddToBlackList(string id)
        {
            var user = _unitOfWork.UserManager.FindById(id);
            user.IsEnabled = false;
            _unitOfWork.Save();
        }

        public void RemoveFromBlackList(string id)
        {
            var user = _unitOfWork.UserManager.FindById(id);
            user.IsEnabled = true;
            _unitOfWork.Save();
        }

        public IEnumerable<ApplicationUser> GetBlackList()
        {
            var bannedUsers = _unitOfWork.UserManager.Users.Where(x => x.IsEnabled == false);
            return bannedUsers; 
        }
    }
}
