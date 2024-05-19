using Microsoft.AspNetCore.Mvc;
using ToDoServer.Model;

namespace ToDoServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController(ITodoService todoService, ILogger<TodoController> logger) : ControllerBase
{
    private readonly ITodoService _todoService = todoService;
    private readonly ILogger<TodoController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [HttpGet]
    public ActionResult<IEnumerable<TodoItem>> GetTodos()
    {
        _logger.LogDebug($"{nameof(TodoController)}.{nameof(GetTodos)}");
        return Ok(_todoService.GetAllTodos());
    }

    [HttpPost]
    public IActionResult AddTodo([FromBody] TodoItem todo)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning($"{nameof(TodoController)}.{nameof(AddTodo)} ModelState is not valid");
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
        }
        _todoService.AddTodo(todo);
        _logger.LogDebug($"{nameof(TodoController)}.{nameof(AddTodo)}");
        return Ok(todo);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTodo(int id, [FromBody] TodoItem todo)
    {
        if (id != todo.Id)
        {
            _logger.LogWarning($"{nameof(TodoController)}.{nameof(UpdateTodo)} id != todo.Id");
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            _logger.LogWarning($"{nameof(TodoController)}.{nameof(UpdateTodo)} ModelState is not valid");
            return BadRequest(ModelState.Values.SelectMany(v => v.Errors));
        }

        _todoService.UpdateTodo(todo);
        _logger.LogDebug($"{nameof(TodoController)}.{nameof(DeleteTodo)}");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTodo(int id)
    {
        _logger.LogDebug($"{nameof(TodoController)}.{nameof(DeleteTodo)}");
        _todoService.DeleteTodo(id);
        return NoContent();
    }
}


