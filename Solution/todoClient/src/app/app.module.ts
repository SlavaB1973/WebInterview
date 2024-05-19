import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Import FormsModule for forms

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TodoListComponent } from './todo-list/todo-list.component';
import { TodoItemComponent } from './todo-item/todo-item.component';
import { TodoServiceHttp } from './services/todo.service.http';
import { TodoServiceMemory } from './services/todo.service.memory';
import { TodoListComponentFinal } from './todo-list/todo-list.component.final';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http'; // Import HttpClientModule
import { TodoItemComponentFinal } from './todo-item/todo-item.component.final';
import { ResponseInterceptor } from './interceptors/response-interceptor';

@NgModule({
  declarations: [
    AppComponent,
    TodoListComponent,
    TodoListComponentFinal,
    TodoItemComponent,
    TodoItemComponentFinal,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    TodoServiceHttp , { provide: HTTP_INTERCEPTORS, useClass: ResponseInterceptor, multi: true },
    TodoServiceHttp ,
    TodoServiceMemory], // Add the service to providers
  bootstrap: [AppComponent]
})
export class AppModule { }
