using ToDoServer.Model;

namespace ToDoServer.Observer;

public class ConsoleFileSaveObserver : IFileSaveObserver
{
    public void OnFileSaved(int toDoCount) => Console.WriteLine($"To-Do list count : {toDoCount}");
    
}
