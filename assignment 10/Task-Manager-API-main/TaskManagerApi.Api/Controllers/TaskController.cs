using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Models;
using TaskManager.API.Services;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;

        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TaskItem>> GetAllTasks([FromQuery] TaskFilterParams filterParams)
        {
            var result = _taskService.GetAll(filterParams);
            return Ok(result);
        }
    }
}