namespace ToDoServer.Model;

public interface IFileSaveObserver
{
    void OnFileSaved(int toDoCount);
}

