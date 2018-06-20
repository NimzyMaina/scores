import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { ConfigService } from './config.service';
import { Observable } from 'rxjs/Observable';
import { Match } from '../shared/interfaces';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

@Injectable()
export class DataService {

  _baseUrl: string = '';

  constructor(private http: Http,
    private configService: ConfigService) {
    this._baseUrl = configService.getApiURI();
  }

  getMatches(): Observable<Match[]> {
    return this.http.get(this._baseUrl + 'matches')
        .map(res => this.extractData(res))
        .catch(this.handleError);
  }

  private extractData(res: any) {
    let body = res.json();
    return body || {};
  }

  private handleError(error: any) {
    // In a real world app, we might use a remote logging infrastructure
    // We'd also dig deeper into the error to get a better message
    let errMsg = (error.message) ? error.message :
        error.status ? `${error.status} - ${error.statusText}` : 'Server error';
    console.error(errMsg); // log to console instead
    return Observable.throw(errMsg);
  }
}
