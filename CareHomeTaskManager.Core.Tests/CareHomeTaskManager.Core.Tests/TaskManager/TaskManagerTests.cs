using CareHomeTaskManager.Core.DataInterface;
using CareHomeTaskManager.Core.Validation;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CareHomeTaskManager.Core.Tests.TaskManager
{
    public class TaskManagerTests
    {
        
        private readonly Mock<ICareTaskRepository> _careTaskRepositoryMock;
        private readonly CareTaskManager _careTaskManager;
        private readonly CareTask _careTask;
        private readonly List<CareTask> _careTasks;
        public TaskManagerTests()
        {
           
            _careTasks = new List<CareTask> { new CareTask() };
            _careTaskRepositoryMock = new Mock<ICareTaskRepository>();                       
            _careTaskManager = new CareTaskManager(_careTaskRepositoryMock.Object);
            _careTask = new CareTask();
            _careTask.Id = 1;
            
        }

        [Test]
        public void ShouldReturnAllTaskRecords()
        {
            _careTaskRepositoryMock.Setup(x => x.GetAll()).Returns(_careTasks);
            var lst= _careTaskManager.GetAllTasks();           
            Assert.That(lst, Is.Not.Null);
            Assert.That(lst.Count(), Is.EqualTo(1));
        }

        [Test]
        public void ShouldSaveCareTask()
        {
            _careTaskManager.Save(_careTask);
            _careTaskRepositoryMock.Verify(x=>x.Save(It.IsAny<CareTask>()),Times.Once);
        }
        [Test]
        public void ShouldReturnTaskWithRequestedId()
        {
            _careTaskRepositoryMock.Setup(x => x.Get(1)).Returns(_careTask);
            var requestedTask = _careTaskManager.GetWithId(1);
            Assert.That(requestedTask.Id, Is.EqualTo(_careTask.Id));
        }


    }
}
