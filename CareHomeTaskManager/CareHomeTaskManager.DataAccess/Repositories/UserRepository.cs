using CareHomeTaskManager.Core.DataInterface;
using CareHomeTaskManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CareHomeTaskManagerContext _context;
        public UserRepository(CareHomeTaskManagerContext context)
        {
            _context = context;
        }
        public User GetUser(string email)
        {
            return _context.Users.Find(email);
        }

        public void SaveUser(User user)
        {
            if (_context.Users.FirstOrDefault(x => x.Email == user.Email) == null)
            {
                _context.Users.Add(user);
            }
            else
            {
                _context.Users.Update(user);
            }
            _context.SaveChanges();
        }
    }
}
