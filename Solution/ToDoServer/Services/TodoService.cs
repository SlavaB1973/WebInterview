using System.Text.Json;
using ToDoServer.Model;

namespace ToDoServer.Services;

public class TodoService : ITodoService
{
    private readonly string _filePath;
    private List<TodoItem> _todos;
    private ILogger<TodoService> _logger;

    private readonly List<IFileSaveObserver> _observers;

    public TodoService(IConfiguration configuration, IEnumerable<IFileSaveObserver> observers, ILogger<TodoService> logger)
    {
        _filePath = configuration["ToDoFile"] ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _todos = LoadTodos();
        _observers = observers.ToList(); // Convert to a list for iteration
    }

    private List<TodoItem> LoadTodos()
    {
        try
        {
            using var stream = File.OpenRead(_filePath);
            return JsonSerializer.Deserialize<List<TodoItem>>(stream);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            // Handle potential file access or deserialization errors (optional logging)
            return [];
        }
    }

    private void SaveTodos()
    {
        try
        {
            using (var stream = File.OpenWrite(_filePath))
            {
                JsonSerializer.Serialize(stream, _todos);
            }
            // Notify observers after successful save
            foreach (var observer in _observers)
            {
                observer.OnFileSaved(_todos.Count);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            // Handle potential file access or serialization errors (optional logging)
        }
    }

    public IEnumerable<TodoItem> GetAllTodos()
    {
        return _todos.AsEnumerable(); // Return a copy to avoid modifying original list
    }

    public void AddTodo(TodoItem todo)
    {
        todo.Id = _todos.Count + 1; // Generate a unique ID
        _todos.Add(todo);
        SaveTodos();
    }

    public void UpdateTodo(TodoItem todo)
    {
        var index = _todos.FindIndex(t => t.Id == todo.Id);
        if (index != -1)
        {
            _todos[index] = todo;
            SaveTodos();
        }
    }

    public void DeleteTodo(int id)
    {
        var index = _todos.FindIndex(t => t.Id == id);
        if (index != -1)
        {
            _todos.RemoveAt(index);
            SaveTodos();
        }
    }
}


