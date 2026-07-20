using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Api.Models;
using TaskManagerApi.Api.Models.Entities;
using TaskManagerApi.Api.Services.Interfaces;

namespace TaskManagerApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// Gets a filtered, paginated list of tasks.
    /// </summary>
    /// <param name="filter">Search, completion status, sorting, and pagination options.</param>
    /// <response code="200">Returns the matching list of tasks.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskItem>), StatusCodes.Status200OK)]
    public IActionResult GetAll([FromQuery] TaskFilterParams filter)
    {
        var result = _taskService.GetTasks(filter);
        return Ok(result);
    }

    /// <summary>
    /// Gets a single task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to retrieve.</param>
    /// <response code="200">Returns the requested task.</response>
    /// <response code="404">No task exists with the given ID.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskItem), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult GetById(int id)
    {
        var task = _taskService.GetTaskById(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    /// <summary>
    /// Creates a new task.
    /// </summary>
    /// <param name="task">The task to create. Only the title is required.</param>
    /// <response code="201">The task was created successfully.</response>
    /// <response code="400">The title was missing or empty.</response>
    [HttpPost]
    [ProducesResponseType(typeof(TaskItem), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] TaskItem task)
    {
        if (task == null || string.IsNullOrWhiteSpace(task.Title))
        {
            return BadRequest("Title is required.");
        }

        var createdTask = _taskService.CreateTask(task.Title);
        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }

    /// <summary>
    /// Updates an existing task's title and completion status.
    /// </summary>
    /// <param name="id">The ID of the task to update.</param>
    /// <param name="task">The updated title and completion status.</param>
    /// <response code="200">The task was updated successfully.</response>
    /// <response code="400">The title was missing or empty.</response>
    /// <response code="404">No task exists with the given ID.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TaskItem), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] TaskItem task)
    {
        if (task == null || string.IsNullOrWhiteSpace(task.Title))
        {
            return BadRequest("Title is required.");
        }

        var updatedTask = _taskService.UpdateTask(id, task.Title, task.IsCompleted);
        if (updatedTask == null) return NotFound();
        return Ok(updatedTask);
    }

    /// <summary>
    /// Deletes a task by its ID.
    /// </summary>
    /// <param name="id">The ID of the task to delete.</param>
    /// <response code="204">The task was deleted successfully.</response>
    /// <response code="404">No task exists with the given ID.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var deleted = _taskService.DeleteTask(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}