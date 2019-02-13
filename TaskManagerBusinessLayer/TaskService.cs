using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using TaskManagerEntities;
using TaskManagerDataLayer;
using AutoMapper;
using System.Data.Entity;
namespace TaskManagerBusinessLayer
{
    public class TaskService : ITaskService
    {
        TaskManagerContext _context;
        IMapper mapper;

        public static TaskService CreateService()
        {
            return new TaskService(new TaskManagerContext());
        }

        public TaskService(TaskManagerContext context)
        {
            var mapConfig = new MapperConfiguration(x => x.CreateMap<Task, TaskDetails>().ReverseMap());
            mapper = mapConfig.CreateMapper();
            _context = context;

        }
        public bool AddTask(TaskDetails task)
        {
            TaskManagerDataLayer.Task taskData = mapper.Map<Task>(task);
            _context.tasks.Add(taskData);
            _context.SaveChanges();
            return true;

        }

        public List<TaskDetails> GetTasks()
        {
            var data = _context.tasks.ToList<Task>();
            return mapper.Map<List<Task>, List<TaskDetails>>(data);

        }
        public TaskDetails GetTaskByID(int id)
        {
            var data = _context.tasks.Find(id);
            return mapper.Map<Task, TaskDetails>(data);

        }

        public bool UpdateTasks(TaskDetails task)
        {
            Task taskData = mapper.Map<Task>(task);
            var entity = _context.tasks.Find(task.TaskID);
            _context.Entry(entity).CurrentValues.SetValues(task);
            _context.SaveChanges();
            return true;
        }
        public bool RemoveTask(int taskID)
        {
            var entity = _context.tasks.Find(taskID);
            _context.tasks.Remove(entity);
            _context.SaveChanges();
            return true;
        }
    }
}
