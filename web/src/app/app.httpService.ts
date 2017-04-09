import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/operator/do';
import 'rxjs/add/observable/throw'

export class Activity {
  constructor(public id: number,
    public name: string,
    public units: number,
    public startToFinish: number,
    public unitDelta: number,
    public durationFunction: number,
    public lag: number,
    public safeProductivityRate: number,
    public aggressiveProductivityRate: number,
    public duration: number,
    public aggressiveDuration: number,
    public inputProdRate: boolean,
    public dependencies: string,
    public process: string,
    public section: string,
    public processId: string) { }
}

export class Process {
  constructor(public id: number,
    public name: string) { }
}

export class ControllingLink {
  constructor(public upperActivity: string,
    public lowerActivity: string,
    public timePeriod: number) { }
}

@Injectable()
export class HttpService {
  constructor(private http: Http) { }
  addActivity(activity) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http
      .post('http://localhost:5000/api/activities/', JSON.stringify(activity), { headers: headers }).catch(this.handleError);
  }
  getActivities() {
    return this.http
      .get('http://localhost:5000/api/activities/')
      .map((response: Response) => <Activity[]>response.json())
      .catch(this.handleError);
  }
  getProcesses() {
    return this.http
      .get('http://localhost:5000/api/processes/')
      .map((response: Response) => <Process[]>response.json())
      .catch(this.handleError);
  }
  getGraph(withCriticalPath) {
    return this.http
      .get('http://localhost:5000/api/activities/compute/' + withCriticalPath)
      .map((response: Response) => response.json())
      .do(data => {
        data.plotOptions.line.dataLabels.formatter =
          function () {
            return this.x.toFixed(2)
          };
      })
      .catch(this.handleError);
  }
  updateActivity(activity) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http
      .put('http://localhost:5000/api/activities/', JSON.stringify(activity), { headers: headers })
      .catch(this.handleError);;
  }
  deleteActivity(id) {
    return this.http
      .delete('http://localhost:5000/api/activities/' + id)
      .catch(this.handleError);
  }
  deleteProcess(id) {
    return this.http
      .delete('http://localhost:5000/api/processes/' + id)
      .catch(this.handleError);
  }
  addProcess(process) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http
      .post('http://localhost:5000/api/processes/', JSON.stringify(process), { headers: headers })
      .catch(this.handleError);
  }
  updateProcess(process) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http
      .put('http://localhost:5000/api/processes/', JSON.stringify(process), { headers: headers })
      .catch(this.handleError);
  }
  getLink() {
    return this.http
      .get('http://localhost:5000/api/links/')
      .map((response: Response) => <ControllingLink>response.json())
      .catch(this.handleError);
  }
  updateLink(link) {
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    return this.http
      .put('http://localhost:5000/api/links/', JSON.stringify(link), { headers: headers })
      .catch(this.handleError);;
  }
  private handleError(error: Response) {
    return Observable.throw(error.json().Message);
  }
}