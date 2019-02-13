using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NBench;
using Moq;
using TaskManagerDataLayer;
using System.Data.Entity;
using TaskManagerBusinessLayer;
using TaskManagerEntities;
using EntityFramework.Testing;
namespace TaskManagerWebAPI.Tests.Controllers
{

    [TestFixture]
    public class TaskControllerTest
    {
        [Test]
        [PerfBenchmark(NumberOfIterations =500, RunMode = RunMode.Iterations,TestMode = TestMode.Measurement)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetAllTaskTest()
        {
            var data = new List<TaskManagerDataLayer.Task>()
            {
                new TaskManagerDataLayer.Task {TaskID=1,TaskName="Task1",ParentTaskName="Task1Parent",Priority=10,StartDate=DateTime.Now,EndDate=DateTime.Now },
                new TaskManagerDataLayer.Task { TaskID=1,TaskName="Task2",ParentTaskName="Task2Parent",Priority=10,StartDate=DateTime.Now,EndDate=DateTime.Now }
            }.AsQueryable();
            var mock = new Mock<DbSet<TaskManagerDataLayer.Task>>();
            mock.As<IQueryable<TaskManagerDataLayer.Task>>().Setup(x => x.Provider).Returns(data.Provider);
            mock.As<IQueryable<TaskManagerDataLayer.Task>>().Setup(x => x.Expression).Returns(data.Expression);
            mock.As<IQueryable<TaskManagerDataLayer.Task>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<TaskManagerDataLayer.Task>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var context = new Mock<TaskManagerContext>();
            context.Setup(x => x.tasks).Returns(mock.Object);

            var service = new TaskService(context.Object);
            List<TaskDetails> taskDetails = service.GetTasks();
            Assert.That(taskDetails.Count == 2);



        }

        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void GetTaskByIDTest()
        {
            var data = new List<TaskManagerDataLayer.Task>()
            {
                new TaskManagerDataLayer.Task {TaskID=1,TaskName="Task1",ParentTaskName="Task1Parent",Priority=10,StartDate=DateTime.Now,EndDate=DateTime.Now },
                new TaskManagerDataLayer.Task { TaskID=2,TaskName="Task2",ParentTaskName="Task2Parent",Priority=10,StartDate=DateTime.Now,EndDate=DateTime.Now }
            };
            var context = new Mock<TaskManagerContext>();
            context.Setup(x => x.tasks).Returns(new Mock<DbSet<TaskManagerDataLayer.Task>>().SetupData(data, o =>
            {
                return data.Single(x => x.TaskID == (int)o.First());
            }).Object);


            var service = new TaskService(context.Object);
            TaskDetails taskDetails = service.GetTaskByID(1);
            Assert.That(taskDetails.TaskID == 1);
        }
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void AddTaskTest()
        {
            var data = new List<TaskManagerDataLayer.Task>()
            {
                new TaskManagerDataLayer.Task {TaskID=1,TaskName="Task1",ParentTaskName="Task1Parent",Priority=10,StartDate=DateTime.Now,EndDate=DateTime.Now },
                new TaskManagerDataLayer.Task { TaskID=1,TaskName="Task2",ParentTaskName="Task2Parent",Priority=10,StartDate=DateTime.Now,EndDate=DateTime.Now}
            }.AsQueryable();
            var mock = new Mock<DbSet<TaskManagerDataLayer.Task>>();
            mock.As<IQueryable<TaskManagerDataLayer.Task>>().Setup(x => x.Provider).Returns(data.Provider);
            mock.As<IQueryable<TaskManagerDataLayer.Task>>().Setup(x => x.Expression).Returns(data.Expression);
            mock.As<IQueryable<TaskManagerDataLayer.Task>>().Setup(x => x.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<TaskManagerDataLayer.Task>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            var context = new Mock<TaskManagerContext>();
            context.Setup(x => x.tasks).Returns(mock.Object);

            TaskDetails task = new TaskDetails { TaskID = 1, TaskName = "Task1", ParentTaskName = "Task1Parent", Priority = 10, StartDate = DateTime.Now, EndDate = DateTime.Now };

            var service = new TaskService(context.Object);
            var ret = service.AddTask(task);
            Assert.That(ret == true);

        }
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void UpdateTaskTest()
        {
            var data = new List<TaskManagerDataLayer.Task>()
            {
                new TaskManagerDataLayer.Task {TaskID=1,TaskName="Task1",ParentTaskName="Task1Parent",Priority=10,StartDate=DateTime.Now,EndDate=DateTime.Now},
                new TaskManagerDataLayer.Task { TaskID=2,TaskName="Task2",ParentTaskName="Task2Parent",Priority=10,StartDate=DateTime.Now,EndDate=DateTime.Now }
            };
            var context = new Mock<TaskManagerContext>();
            context.Setup(x => x.tasks).Returns(new Mock<DbSet<TaskManagerDataLayer.Task>>().SetupData(data, o =>
            {
                return data.Single(x => x.TaskID == (int)o.First());
            }).Object);

            var service = new TaskService(context.Object);
            TaskDetails detail = service.GetTaskByID(1);
            var mockDataService = new Mock<ITaskService>();
            var ret = mockDataService.Object.UpdateTasks(detail);
            Assert.That(ret == false);



        }
        [Test]
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Iterations, TestMode = TestMode.Test)]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        public void DeleteTaskTest()
        {
            var data = new List<TaskManagerDataLayer.Task>()
            {
                new TaskManagerDataLayer.Task {TaskID=1,TaskName="Task1",ParentTaskName="Task1Parent",Priority=10,StartDate=DateTime.Now,EndDate=DateTime.Now },
                new TaskManagerDataLayer.Task { TaskID=2,TaskName="Task2",ParentTaskName="Task2Parent",Priority=10,StartDate=DateTime.Now,EndDate=DateTime.Now}
            };
            var context = new Mock<TaskManagerContext>();
            context.Setup(x => x.tasks).Returns(new Mock<DbSet<TaskManagerDataLayer.Task>>().SetupData(data, o =>
            {
                return data.Single(x => x.TaskID == (int)o.First());
            }).Object);

            var service = new TaskService(context.Object);
            var ret = service.RemoveTask(1);
            //var mockDataService = new Mock<ITaskService>();
            //var ret = mockDataService.Object.UpdateTasks(detail);
            Assert.That(ret == true);



        }
    }
}
