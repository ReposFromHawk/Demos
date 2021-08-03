using CareHomeTaskManager.Core;
using CareHomeTaskManager.Core.DataInterface;
using CareHomeTaskManager.Core.Validation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CareHomeTaskManager.Api.Controllers
{
    public class CareHomeTaskControllerTests 
    {
        private Mock<ICareTaskManager> _careTaskManager;
        private Mock<IUserManager> _userManager;
       
        [SetUp]
        public void SetUp()
        {
            _careTaskManager = new Mock<ICareTaskManager>();
            _userManager = new Mock<IUserManager>();
        }

        [Test]
        public async Task ShouldReturnAllCareTasksWhenCareHomeTasksEndPointCalled()
        {
            _careTaskManager.Setup(x => x.GetAllTasks()).Returns(new List<CareTask>());
            //Arrange
            CareHomeTaskController careHomeTaskController = new CareHomeTaskController(_careTaskManager.Object,_userManager.Object);
            //Act
           await careHomeTaskController.Get();
            //Assert
            _careTaskManager.Verify(x => x.GetAllTasks(), Times.Once);
        }
        [Test]
        [TestCase(1)]
        [TestCase(10)]
        public async Task ShouldReturnCareTaskRequestedById(int requestedId)
        {
            CareTask careTask = new CareTask() { Id = requestedId };
            _careTaskManager.Setup(x => x.GetWithId(requestedId)).Returns(careTask);
            CareHomeTaskController careHomeTaskController = new CareHomeTaskController(_careTaskManager.Object, _userManager.Object);
            var res = await careHomeTaskController.Get(requestedId) as OkObjectResult;
            var expectedResultObject = res.Value as CareTask;
            _careTaskManager.Verify(x => x.GetWithId(requestedId), Times.Once);
            Assert.That(expectedResultObject.Id, Is.EqualTo(requestedId));
        }
        [Test]
        [TestCase(1)]
        public async Task RequestedByIdShouldReturnAnIActionResult(int requestedId)
        {
            CareHomeTaskController careHomeTaskController = new CareHomeTaskController(_careTaskManager.Object, _userManager.Object);
            var result=await careHomeTaskController.Get(requestedId);
            Assert.That(result.GetType(),Is.AssignableFrom(typeof(IActionResult).GetType()));
        }
        [Test]
        [TestCase(1)]
        public async Task RequestedByIdShouldReturn400ResultWhenNoItemFindWIthGivenId(int requestedId)
        {
            _careTaskManager.Setup(x => x.GetWithId(requestedId)).Returns(() => null);
            CareHomeTaskController careHomeTaskController = new CareHomeTaskController(_careTaskManager.Object, _userManager.Object);
            var result = await careHomeTaskController.Get(requestedId);
            Assert.That(result.GetType(), Is.EqualTo(typeof(BadRequestResult)));
        }
        [Test]
        public async Task GetShouldShouldReturnIActionResult()
        {
            CareHomeTaskController careHomeTaskController = new CareHomeTaskController(_careTaskManager.Object, _userManager.Object);
            var result = await careHomeTaskController.Get();
            Assert.That(result.GetType(), Is.AssignableFrom(typeof(IActionResult).GetType()));
        }
        [Test]
        public async Task GetShouldReturn_404_When_No_ContentFound()
        {
            _careTaskManager.Setup(x => x.GetAllTasks()).Returns(()=>null);
            CareHomeTaskController careHomeTaskController = new CareHomeTaskController(_careTaskManager.Object, _userManager.Object);   
            var result = await careHomeTaskController.Get();
            Assert.That(result.GetType(), Is.EqualTo(typeof(NotFoundResult)));
        }
        [Test]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(130)]
        public async Task ShouldCallSaveOnPost(int id)
        {
            CareTask ct = new CareTask() { Id = id };
            CareHomeTaskController careHomeTaskController = new CareHomeTaskController(_careTaskManager.Object, _userManager.Object);
            await careHomeTaskController.Post(ct);
            _careTaskManager.Verify(x => x.Save(ct), Times.Once);
        }
        [Test]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(130)]
        public async Task ShouldReturn201WhenSaveCompleted(int id)
        {
            CareTask ct = new CareTask() { Id = id };
            CareHomeTaskController careHomeTaskController = new CareHomeTaskController(_careTaskManager.Object, _userManager.Object);
            var result =await careHomeTaskController.Post(ct);
            Assert.That(result.GetType(), Is.EqualTo(typeof(CreatedAtActionResult)));
        }
        [Test]
        [TestCase(1)]
        [TestCase(15)]
        [TestCase(130)]
        public async Task ShouldReturn400WhenSaveFailed(int id)
        {
            CareTask ct = new CareTask() { Id = id };
            _careTaskManager.Setup(x => x.Save(ct)).Throws(new ArgumentException());
            CareHomeTaskController careHomeTaskController = new CareHomeTaskController(_careTaskManager.Object, _userManager.Object);
            var result = await careHomeTaskController.Post(ct);
            Assert.That(result.GetType(), Is.EqualTo(typeof(BadRequestResult)));
        }
        [Test]
        public async Task ShouldLoginAndSaveUser()
        {
            User user = new User { Email = "erdem@test.com", Password = "Test123" };
            _userManager.Setup(s => s.GetUser(user.Email)).Returns(()=>user);
            CareHomeTaskController careHomeTaskController = new CareHomeTaskController(_careTaskManager.Object, _userManager.Object);
            var result = await careHomeTaskController.Login(user);
            Assert.That(result.GetType(), Is.AssignableFrom(typeof(IActionResult).GetType()));
            var res = result as OkObjectResult;
            var expectedResultObject = res.Value as User;
            Assert.That(expectedResultObject.Email, Is.EqualTo(user.Email));
        }
        [Test]
        [TestCase("erdem@test.com","Test123")]
        [TestCase("tester@test.com", "Test123")]
        public async Task ShouldSaveUser(string email,string password)
        {
            User user = new User { Email = email, Password = password };
            CareHomeTaskController careHomeTaskController = new CareHomeTaskController(_careTaskManager.Object, _userManager.Object);
            var result = await careHomeTaskController.Create(user);
            Assert.That(result.GetType(), Is.AssignableFrom(typeof(IActionResult).GetType()));
            var res = result as OkObjectResult;
            var expectedResultObject = res.Value as string;
            Assert.That(expectedResultObject, Is.EqualTo($"New User with { user.Email } is created!"));
        }
        
    }
}
