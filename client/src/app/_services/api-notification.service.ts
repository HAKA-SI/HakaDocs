import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject, Observable, interval, timer } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user.model';
import { ApiNotification } from '../_models/notification.model';
import { switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiNotificationService {


  // private hubUrl = environment.hubUrl;
  // private hubConnection: HubConnection;
  private notificationThreadSource = new BehaviorSubject<ApiNotification[]>([]);
  notificationThread$ = this.notificationThreadSource.asObservable();
  apiUrl = environment.apiUrl + 'notification/';

  constructor(private http: HttpClient) { }


  // createHubConnection(user: User) {
  //   this.hubConnection = new HubConnectionBuilder()
  //     .withUrl(this.hubUrl + 'stockAlert', {
  //       accessTokenFactory: () => user.token,
  //     })
  //     .withAutomaticReconnect()
  //     .build();
  //   this.hubConnection.start().catch((error) => console.log(error));

  //   this.hubConnection.on("NotificationsThread", notifications => {

  //    this.notificationThreadSource.next(notifications);
  //   })

  //   this.hubConnection.on("StockAlertsReceived", (notifications:ApiNotification[]) => {
  //    console.log("notifications received");

  //     this.notificationThreadSource.next(notifications.filter(a => a.recipientId ===user.id));
  //    })

  // }

  // stopHubConnection() {
  //   if (this.hubConnection) {

  //     this.hubConnection.stop();
  //   }
  // }

  startPolling(): void {
    //  interval(60000) // Récupère les notifications toutes les minutes
    timer(0, 60000)// la fonction est declenchée à t0 et s'execute chaque minute
      .pipe(
        switchMap(() => this.http.get<any>(this.apiUrl + 'NotificationThread'))
      )
      .subscribe(
        (notifications: any[]) => {
          this.notificationThreadSource.next(notifications);
        },
        (error) => {
          console.error('Une erreur s\'est produite lors de la récupération des notifications :', error);
        }
      );

  }

  getNotificationThread() {
    return this.http.get(this.apiUrl + 'NotificationThread');
  }


  deleteNotification(ids:number[]) {
    return this.http.post(this.apiUrl+'DeleteNotifications',ids);
  }

  markNotificationAsReaded(ids:number[]) {
    return this.http.post(this.apiUrl+'MarkNotificationsAsReaded',ids);
  }


}
