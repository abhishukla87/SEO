import { Injectable} from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable,  throwError } from 'rxjs';
import { catchError, tap} from 'rxjs/operators';

import { Search } from './search';

@Injectable({
  providedIn: 'root'
})

export class SearchService {

  private searchUrl = 'http://localhost:5000/search';

  constructor(private http: HttpClient) { }


  getSearchResult(keyword: string, searchEngine: string): Observable<Search> {

    console.log(keyword);
    console.log(searchEngine);
    const headers = new HttpHeaders({
      'Access-Control-Allow-Origin': '*',    
      "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, PATCH, OPTIONS",
      "Access-Control-Allow-Headers": "*"
    });

    const url = `${this.searchUrl}?Keyword=${keyword}&searchEngine=${searchEngine}`;

    return this.http.get<Search>(url, { headers })
      .pipe(
        tap(data => console.log('searchResult: ' + JSON.stringify(data))),
        catchError(this.handleError)
      );
  }

  private handleError(err): Observable<never> {
    
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    return throwError(errorMessage);
  }
}
