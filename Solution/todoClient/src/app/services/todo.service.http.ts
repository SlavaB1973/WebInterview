import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { TodoItem } from '../model/todo.model';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
};

 @Injectable({
  providedIn: 'root'
})
export class TodoServiceHttp {

  private apiUrl = 'https://localhost:7298/api/Todo'; // Replace with actual API endpoint
  constructor(private http: HttpClient) {}

  getTodos(): Observable<TodoItem[]> {
    return this.http.get<TodoItem[]>(this.apiUrl)
      .pipe(
        catchError(this.handleError<TodoItem[]>('getTodos'))
      );
  }

  addTodo(todo: TodoItem): Observable<TodoItem> {
    return this.http.post<TodoItem>(this.apiUrl, todo, httpOptions)
      .pipe(
        catchError(this.handleError<TodoItem>('addTodo'))
      );
  }

  updateTodo(todo: TodoItem): Observable<TodoItem> {
    return this.http.put<TodoItem>(`${this.apiUrl}/${todo.id}`, todo, httpOptions)
      .pipe(
        catchError(this.handleError<TodoItem>('updateTodo'))
      );
  }

  deleteTodo(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`, httpOptions)
      .pipe(
        catchError(this.handleError<void>('deleteTodo'))
      );
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error); // log the error for debugging

      // Return a bad observable with a user facing error message
      return of(result as T);
    };
  }
}
