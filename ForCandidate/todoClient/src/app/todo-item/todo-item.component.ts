import { Component, Input } from '@angular/core';
import { TodoItem } from '../model/todo.model';

@Component({
  selector: 'app-todo-item',
  templateUrl: './todo-item.component.html'
})
export class TodoItemComponent {
  @Input() todo: TodoItem | undefined;
}

