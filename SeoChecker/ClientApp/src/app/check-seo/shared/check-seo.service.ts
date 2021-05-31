import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, BehaviorSubject } from 'rxjs';

import { CheckSeoResult } from './check-seo-result.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CheckSeoService {

  private http: HttpClient;
  private baseUrl: string;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
  }

  public getRanks(searchEngine: string, keywords: string): Observable<CheckSeoResult> {
    let searchResult = new Subject<CheckSeoResult>();

    this.http.get(
      this.baseUrl + `${environment.seoCheckerApi}/www.${searchEngine}.com/${keywords}`,
      { observe: 'response', responseType: 'text' }
    ).subscribe(
      result => {
        let newResult = {
          searchEngine: searchEngine,
          keywords: keywords,
          ranks: result.body.toString()
        };

        // Emit the result
        searchResult.next(newResult);
      },
      error => {
        console.error(error);
        searchResult.next(null);
      })

    return searchResult.asObservable();
  }
}
