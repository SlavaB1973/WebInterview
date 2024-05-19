using ToDoServer.Model;

namespace ToDoServer.Observer;

public class LogFileSaveObserver : IFileSaveObserver
{
    private readonly ILogger<LogFileSaveObserver> _logger;

    public LogFileSaveObserver(ILogger<LogFileSaveObserver> logger)
    {
        _logger = logger;
    }

    public void OnFileSaved(int toDoCount)
    {
        _logger.LogInformation($"To-Do list count: {toDoCount}");
    }
}

