import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationsRoutingModule } from './notifications-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { ListNotificationComponent } from './list-notification/list-notification.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ListNotificationComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    NotificationsRoutingModule
  ]
})
export class NotificationsModule { }
