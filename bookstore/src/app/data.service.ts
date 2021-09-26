import { Injectable } from '@angular/core';
import {HttpClient, HttpClientModule} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from './models/book.model';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private REST_API_URL = 'http://localhost:6422/api/Book';
  constructor(private http:HttpClient) { }

  getBooks():Observable<Book[]>
  {
    return this.http.get<Book[]>(this.REST_API_URL)
  }

  getBooksById(Id:string):Observable<Book>
  {
    let URL =this.REST_API_URL+'/'+Id;
    return this.http.get<Book>(URL);
  }
  addBook(data:Book) :Observable<Book>
  {
    return this.http.post<Book>(this.REST_API_URL,data);
  }
  updateBook(Id:number,data:Book):Observable<Book>
  {
    let URL=this.REST_API_URL+'/'+Id.toString();
    return this.http.put<Book>(URL,data);
  }
  DeleteBook(Id:number)
  {
    let URL=this.REST_API_URL+'/'+Id.toString();
    return this.http.delete(URL);
  }
}
