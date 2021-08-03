using CareHomeTaskManager.Core;
using CareHomeTaskManager.Core.Validation;
using CareHomeTaskManager.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CareHomeTaskManager.DataAccess.Tests.Repositories
{
    [TestFixture]
    public class CareHomeTaskRepositoryTests
    {
       
        public CareHomeTaskRepositoryTests()
        {
           
        }
        [Test]
        public void ShouldSaveTask()
        {
            var options = new DbContextOptionsBuilder<CareHomeTaskManagerContext>()
                .UseInMemoryDatabase(databaseName: "ShouldSaveTask").Options;
            //Arrange
            var careTask = new CareTask()
            {
                Id = 1,
                Action = "3 hot meals each day and encourage the client to eat at least 75% of each meal. Provide 1 glass of water with each meal.",
                ActualStartDateTime = new DateTime(2021, 08, 01),
                Completed = false,
                CreatedByUser = "Alex.savage",
                EndDateTime = null,
                Frequency = "3 times a day",
                Outcome = "Test Outcome",
                PatientName = "Dorris Day",
                Reason = "Test Reason",
                TargetDateTime = new DateTime(2021, 08, 10),
                Title = "Feeding care plan"
            };

            //Act
            using (var context = new CareHomeTaskManagerContext(options))
            {
                var repo = new CareHomeTaskManagerRepository(context);
                repo.Save(careTask);
            }
            //Assert
            using (var context = new CareHomeTaskManagerContext(options))
            {
                var tasks = context.CareTasks.ToList();
                Assert.AreEqual(1, tasks.Count);
                var storedTask = tasks[0];
                Assert.AreEqual(careTask.Id, storedTask.Id);
                Assert.AreEqual(careTask.Action, storedTask.Action);
                Assert.AreEqual(careTask.ActualStartDateTime, storedTask.ActualStartDateTime);
                Assert.AreEqual(careTask.Completed, storedTask.Completed);
                Assert.AreEqual(careTask.CreatedByUser, storedTask.CreatedByUser);
                Assert.AreEqual(careTask.EndDateTime, storedTask.EndDateTime);
                Assert.AreEqual(careTask.Frequency, storedTask.Frequency);
                Assert.AreEqual(careTask.Outcome, storedTask.Outcome);
                Assert.AreEqual(careTask.PatientName, storedTask.PatientName);
                Assert.AreEqual(careTask.Reason, storedTask.Reason);
                Assert.AreEqual(careTask.TargetDateTime, storedTask.TargetDateTime);
                Assert.AreEqual(careTask.Title, storedTask.Title);
            }
        }

        [Test]
        public void ShouldReturnCareTaskRequestedWithId()
        {
            var options = new DbContextOptionsBuilder<CareHomeTaskManagerContext>()
               .UseInMemoryDatabase(databaseName: "ShouldReturnCareTaskRequestedWithId(").Options;
            //Arrange
            var tasks = CreateDummyTasks(5);
            using (var context = new CareHomeTaskManagerContext(options))
            {
                foreach (var deskBooking in tasks)
                {
                    context.Add(deskBooking);
                    context.SaveChanges();
                }
            }

            //Act
            using (var context = new CareHomeTaskManagerContext(options))
            {
                var repo = new CareHomeTaskManagerRepository(context);
                var task = repo.Get(3);
                Assert.That(task, Is.Not.Null);
                Assert.That(task.Id, Is.EqualTo(3));
            }
        }

        [Test]
        public void ShouldUpdateTask()
        {
            var options = new DbContextOptionsBuilder<CareHomeTaskManagerContext>()
                .UseInMemoryDatabase(databaseName: "ShouldUpdateTask").Options;
            //Arrange
            var careTask = new CareTask()
            {
                Id = 1,
                Action = "3 hot meals each day and encourage the client to eat at least 75% of each meal. Provide 1 glass of water with each meal.",
                ActualStartDateTime = new DateTime(2021, 08, 01),
                Completed = false,
                CreatedByUser = "Alex.savage",
                EndDateTime = null,
                Frequency = "3 times a day",
                Outcome = "Test Outcome",
                PatientName = "Dorris Day",
                Reason = "Test Reason",
                TargetDateTime = new DateTime(2021, 08, 10),
                Title = "Feeding care plan"
            };

            //Act
            using (var context = new CareHomeTaskManagerContext(options))
            {
                var repo = new CareHomeTaskManagerRepository(context);
                repo.Save(careTask);
                careTask.Completed = true;
                repo.Save(careTask);
            }

            
            using (var context = new CareHomeTaskManagerContext(options))
            {
                var task = context.CareTasks.First();
                Assert.That(task.Completed);
            }
        }

        [Test]
        public void ShouldGetAllCareTasks()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CareHomeTaskManagerContext>()
                .UseInMemoryDatabase(databaseName: "ShouldGetAllCareTasks").Options;
            var storedTasks = CreateDummyTasks(5);
            using (var context = new CareHomeTaskManagerContext(options))
            {
                foreach (var deskBooking in storedTasks)
                {
                    context.Add(deskBooking);
                    context.SaveChanges();
                }
            }
            //Act
            List<CareTask> resultTasks;
            using (var context = new CareHomeTaskManagerContext(options))
            {
                var repo = new CareHomeTaskManagerRepository(context);
                resultTasks = repo.GetAll().ToList();
            }

            CollectionAssert.AreEqual(storedTasks, resultTasks, new CareTaskEqualityComparer());
        }

        private List<CareTask> CreateDummyTasks(int listSize)
        {
            List<CareTask> tasks = new List<CareTask>();
            for (int i = 1; i <= listSize; i++)
            {
                tasks.Add(new CareTask() {
                    Id = i,
                    Action = i+"  -- 3 hot meals each day and encourage the client to eat at least 75% of each meal. Provide 1 glass of water with each meal.",
                    ActualStartDateTime = new DateTime(2021, 08, i),
                    Completed = false,
                    CreatedByUser = i+ " - Alex.savage",
                    EndDateTime = null,
                    Frequency = i + " - 3 times a day",
                    Outcome = "Test Outcome : "+ i,
                    PatientName = "Dorris Day "+i,
                    Reason = "Test Reason : "+ i,
                    TargetDateTime = new DateTime(2021, 08, i+10),
                    Title = "Feeding care plan : "+ i
                });
            }
            return tasks;
        }
    }

    internal class CareTaskEqualityComparer : IComparer
    {
        public int Compare(object x,object y)
        {
            var careTask1 = (CareTask)x;
            var careTask2 = (CareTask)y;
            if (careTask1.Id > careTask2.Id)
                return 1;
            if (careTask1.Id < careTask2.Id)
                return -1;
            else
                return 0;
        }
    }
}
