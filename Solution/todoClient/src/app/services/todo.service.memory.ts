import { Injectable } from '@angular/core';
import { TodoItem } from '../model/todo.model';

@Injectable({
  providedIn: 'root'
})
export class TodoServiceMemory {
  private todos: TodoItem[] = []; // Memory array to store todos

  // Generate a unique ID for new todos
  private nextId = 1;

  getTodos(): TodoItem[] {
    return [...this.todos]; // Return a copy to prevent modification of the original array
  }

  addTodo(todo: Partial<TodoItem>): TodoItem { // Accepts partial data for flexibility
    const newTodo: TodoItem = {
      id: this.nextId++,
      title: todo.title ?? '', // Set default title if empty
      completed: todo.completed || false, // Set default completed state
    };
    this.todos.push(newTodo);
    return newTodo;
  }

  updateTodo(updatedTodo: TodoItem): TodoItem {
    const index = this.todos.findIndex((todo) => todo.id === updatedTodo.id);
    if (index !== -1) {
      this.todos[index] = updatedTodo;
    }
    return updatedTodo;
  }

  deleteTodo(id: number): void {
    const index = this.todos.findIndex((todo) => todo.id === id);
    if (index !== -1) {
      this.todos.splice(index, 1);
    }
  }
}
