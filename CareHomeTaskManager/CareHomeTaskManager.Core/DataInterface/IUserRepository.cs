using CareHomeTaskManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.Core.DataInterface
{
    public interface IUserRepository
    {
        public void SaveUser(User user);
        public User GetUser(string email);
        
    }
}
