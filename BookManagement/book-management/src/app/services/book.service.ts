import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Book } from '../models/book.model';


@Injectable({
  providedIn: 'root'
})
export class BookService {
  private apiURL = 'http://localhost:5187/api/Books';

  constructor(private httpClient : HttpClient) { }


  public getBooks() : Observable<Book[]> {
    return this.httpClient.get<Book[]>(this.apiURL);
  }

  public createBook(book : Book) : Observable<any> {
    return this.httpClient.post<Book>(this.apiURL, book);
  }

}
