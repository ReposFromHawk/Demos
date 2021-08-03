using CareHomeTaskManager.Core.Authentication;
using CareHomeTaskManager.Core.DataInterface;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.Core.Tests.Authentication
{
    public class UserManagerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserManager _userManager;
        private readonly User _user;
       
        public UserManagerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userManager = new UserManager(_userRepositoryMock.Object);
            _user = new User { Email = "erdem@test.com", JwtToken = "", Password = "Test123" };
        }

        [Test]
        public void UserManagerShouldSaveUser()
        {
            _userManager.SaveUser(_user);
            _userRepositoryMock.Verify(x => x.SaveUser(_user), Times.Once);
        }
        [Test]
        public void ShouldReturnRequestedUser()
        {
            _userRepositoryMock.Setup(x => x.GetUser("erdem@test.com")).Returns(_user);
            var res = _userManager.GetUser("erdem@test.com");
            Assert.That(res, Is.Not.Null);            
        }
    }
}
