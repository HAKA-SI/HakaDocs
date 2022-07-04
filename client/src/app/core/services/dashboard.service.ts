import { Client } from './../../shared/models/client';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
 // public data: ChartModel[]=[];
  public data: any[]=[];
  private hubConnection: HubConnection;
  hubUrl = environment.hubUrl;
  baseUrl = environment.apiUrl;

  private onlineClasses = new BehaviorSubject<any[]>([]);
  classes$ = this.onlineClasses.asObservable();

  constructor(private http: HttpClient) {

  }
  public startConnection = () => {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl+'dashboard')
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection to the dashboard started'))
      .catch((err) => console.log('Error while starting connection: ' + err));

      this.addTransferChartDataListener();
  }


  public addTransferChartDataListener = () => {
    this.hubConnection.on('transferdashboarddata', (data) => {
     this.onlineClasses.next(data);
   this.data = data;
    //  console.log(data);
    });
  };

  public getChart() {
    return this.http.get(this.baseUrl + 'dashboard/getcities');
  }
  public getEmailCategories(clients:Client[]) {
    return this.http.post(this.baseUrl + 'dashboard/getEmailCategories', clients);
  }
  public getSmsRecap(clients:Client[]) {
    return this.http.post(this.baseUrl + 'dashboard/GetSmsRecap', clients);
  }

  stopHubConnection() {
    this.hubConnection.stop();
  }
}
