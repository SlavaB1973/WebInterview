import { Component, Input } from '@angular/core';
import { TodoItem } from '../model/todo.model';

@Component({
  selector: 'app-todo-item-final',
  templateUrl: './todo-item.component.final.html'
})
export class TodoItemComponentFinal {
  @Input() todo: TodoItem | undefined;
}

