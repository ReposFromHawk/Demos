using CareHomeTaskManager.Core;
using CareHomeTaskManager.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.DataAccess.Tests.Repositories
{
    public class UserRespositoryTests
    {
        [Test]
        public void UserRepositoryShouldSaveUser()
        {
            var options = new DbContextOptionsBuilder<CareHomeTaskManagerContext>()
              .UseInMemoryDatabase(databaseName: "ShouldSaveUserWithEmail").Options;
            var user = new User() {
            Email="erdem@test.com",
            Password="Test123",
            JwtToken=""
            };
            using (var context = new CareHomeTaskManagerContext(options))
            {
                var repo = new UserRepository(context);
                repo.SaveUser(user);
            }
            using (var context = new CareHomeTaskManagerContext(options))
            {
                var users = context.Users.ToList();
                Assert.AreEqual(1, users.Count);
                var storedUser = users[0];
                Assert.That(user.Email, Is.EqualTo(storedUser.Email));
                Assert.That(user.Password, Is.EqualTo(storedUser.Password));
                Assert.That(user.JwtToken, Is.EqualTo(storedUser.JwtToken));

            }
        }
        [Test]
        public void UserRepositoryShouldFindAndReturnUserWithEmail()
        {
            var options = new DbContextOptionsBuilder<CareHomeTaskManagerContext>()
               .UseInMemoryDatabase(databaseName: "ShouldReturnUserWithEmail").Options;
           
                var users = CreateDummyUsers(5);
                using (var context = new CareHomeTaskManagerContext(options))
                {
                    foreach (var usr in users)
                    {
                        context.Add(usr);
                        context.SaveChanges();
                    }
                }
            using (var context = new CareHomeTaskManagerContext(options))
            {
                var repo = new UserRepository(context);
                var user = repo.GetUser(3+ "__UserEmail@test.com");
                Assert.That(user, Is.Not.Null);
                Assert.That(user.Email, Is.EqualTo(3 + "__UserEmail@test.com"));
            }

        }
        private List<User> CreateDummyUsers(int listSize)
        {
            List<User> users = new List<User>();
            for (int i = 1; i <= listSize; i++)
            {
                users.Add(new User()
                {
                    Email=i+"__UserEmail@test.com",
                    Password="Abc_"+i,
                    JwtToken="J-W-T-"+i
                });
            }
            return users;
        }

    }
}
