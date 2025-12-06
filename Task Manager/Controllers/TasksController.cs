using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_Manager.Data;
using Task_Manager.Models;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class TasksController : ControllerBase
    {
       
        [HttpGet]
        public IActionResult GetAllTasks()
        {
            return Ok(AppData.tasks);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = AppData.tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Task not found");

            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask(TaskModel newTask)
        {
            newTask.Id = AppData.tasks.Count + 1;
            newTask.CreatedBy = int.Parse(User.Claims.First(c => c.Type == "UserId").Value);

            AppData.tasks.Add(newTask);

            return Ok("Task created successfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] 
        public IActionResult UpdateTask(int id, TaskModel updatedTask)
        {
            var task = AppData.tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Task not found");

            task.Title = updatedTask.Title;
            task.Decription = updatedTask.Decription;

            return Ok("Task updated successfully");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] 
        public IActionResult DeleteTask(int id)
        {
            var task = AppData.tasks.FirstOrDefault(t => t.Id == id);
            if (task == null)
                return NotFound("Task not found");

            AppData.tasks.Remove(task);

            return Ok("Task deleted successfully");
        }
    }
}
