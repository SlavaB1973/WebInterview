
import { Component, OnInit } from '@angular/core';
import { TodoServiceHttp } from '../services/todo.service.http';
import { TodoItem } from '../model/todo.model';
import { TodoServiceMemory } from '../services/todo.service.memory';

@Component({
  selector: 'app-todo-list-final',
  templateUrl: './todo-list.component.final.html',
})
export class TodoListComponentFinal implements OnInit {
  todos: TodoItem[] = [];
  newTodo: string = '';
  errorMessage: string = '';

  constructor(private todoService: TodoServiceHttp) {}
  //constructor(private todoService: TodoServiceMemory) {}

  ngOnInit() {
    this.fetchTodos();
  }

  fetchTodos() {
    this.todoService.getTodos()
      .subscribe(
        (todos) => (this.todos = todos),
        (error) => (this.errorMessage = error.message)
      );
  }

  addTodo() {
    if (this.newTodo.trim()) {
      this.todoService.addTodo({
        title: this.newTodo, completed: false,
        id: 0
      })
        .subscribe(
          (todo) => {
            this.todos.push(todo);
            this.newTodo = '';
          },
          (error) => (this.errorMessage = error.message)
        );
    }
  }
  updateTodo(todo: TodoItem) {
    todo.completed = !todo.completed; // Toggle completed state
    this.todoService.updateTodo(todo)
      .subscribe(
        (updatedTodo) => {
          const index = this.todos.findIndex((item) => item?.id && updatedTodo?.id && item.id === updatedTodo.id);
          if (index !== -1) {
            this.todos[index] = updatedTodo;
          }
        },
        (error) => (this.errorMessage = error.message)
      );
  }
  deleteTodo(todo: TodoItem) {
    this.todoService.deleteTodo(todo.id)
      .subscribe(
        () => {
          const index = this.todos.findIndex((item) => item.id === todo.id);
          if (index !== -1) {
            this.todos.splice(index, 1);
          }
        },
        (error) => (this.errorMessage = error.message)
      );
  }
}
