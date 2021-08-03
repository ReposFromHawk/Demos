using CareHomeTaskManager.Core.DataInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.Core.Authentication
{
    public class UserManager : IUserManager
    {
        private IUserRepository _repo;

        public UserManager(IUserRepository repo)
        {
           _repo = repo;
        }

        public User GetUser(string email)
        {
            return _repo.GetUser(email);
        }
        public void SaveUser(User user)
        {
             _repo.SaveUser(user);
        }
    }
}
