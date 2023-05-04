import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user.model';

@Injectable({
  providedIn: 'root'
})
export class ApiNotificationService {

  private hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  private notificationThreadSource = new BehaviorSubject<Notification[]>([]);
  notificationThread$ = this.notificationThreadSource.asObservable();

  constructor(private http: HttpClient) { }


  createHubConnection(user: User) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'stockAlert', {
        accessTokenFactory: () => user.token,
      })
      .withAutomaticReconnect()
      .build();
    this.hubConnection.start().catch((error) => console.log(error));

    this.hubConnection.on("NotificationsThread", notifications => {
     this.notificationThreadSource.next(notifications);
    })

  }

  stopHubConnection() {
    if (this.hubConnection) {

      this.hubConnection.stop();
    }
  }


}
