
import { Component } from '@angular/core';
import { TodoItem } from '../model/todo.model';


@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
})
export class TodoListComponent {
  todos: TodoItem[] = [
    { id: 1, title: 'Buy groceries', completed: false },
    { id: 2, title: 'Finish report', completed: true },
  ];
  newTodo: string ='';

  addTodo(title: string) {
    if (title.trim()) {
      this.todos.push({ id: Math.random(), title, completed: false });
    }
  }

  deleteTodo(todo: TodoItem) {
    const index = this.todos.findIndex((item) => item.id === todo.id);
    if (index !== -1) {
      this.todos.splice(index, 1);
    }
  }
}
