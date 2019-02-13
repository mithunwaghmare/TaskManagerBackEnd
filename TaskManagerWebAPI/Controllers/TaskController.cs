using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TaskManagerBusinessLayer;
using TaskManagerEntities;
using System.Web.Http.Cors;
namespace TaskManagerWebAPI.Controllers
{
    
    public class TaskController : ApiController
    {
        ITaskService service;
        public TaskController()
        {
            service = TaskService.CreateService();
        }
        [HttpGet]
        public IHttpActionResult GetAllTask()
        {
            List<TaskDetails> data = service.GetTasks();
            return Ok(data);
        }
        [HttpGet]
        [Route("api/Task/GetTaskByID/{TaskID}")]
        public IHttpActionResult GetTaskByID(int TaskID)
        {
            TaskDetails data = service.GetTaskByID(TaskID);
            return Ok(data);
        }
        [HttpPost]
        public IHttpActionResult SaveTask(TaskDetails task)
        {
            bool re = service.AddTask(task);
            return Ok(re);

        }
        [HttpPost]
        public IHttpActionResult UpdateTask(TaskDetails task)
        {
            bool re = service.UpdateTasks(task);
            return Ok(re);

        }
        [HttpDelete]
        [Route("api/Task/DeleteTask/{TaskID}")]
        public IHttpActionResult DeleteTask(int TaskID)
        {
            bool re = service.RemoveTask(TaskID);
            return Ok(re);

        }

    }
}
