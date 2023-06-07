import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationsRoutingModule } from './notifications-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ListNotificationComponent } from './list-notification/list-notification.component';



@NgModule({
  declarations: [
    ListNotificationComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    NotificationsRoutingModule
  ]
})
export class NotificationsModule { }
