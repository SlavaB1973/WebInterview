namespace ToDoServer.Model;

public interface ITodoService
{
    IEnumerable<TodoItem> GetAllTodos();
    void AddTodo(TodoItem todo);
    void UpdateTodo(TodoItem todo);
    void DeleteTodo(int id);
}

