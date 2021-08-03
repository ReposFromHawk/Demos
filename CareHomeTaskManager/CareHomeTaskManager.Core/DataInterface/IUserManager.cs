namespace CareHomeTaskManager.Core.DataInterface
{
    public interface IUserManager
    {
        public User GetUser(string email);
        public void SaveUser(User user);
    }
}