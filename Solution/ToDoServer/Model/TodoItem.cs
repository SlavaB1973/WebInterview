﻿namespace ToDoServer.Model;
public class TodoItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool Completed { get; set; }
}

