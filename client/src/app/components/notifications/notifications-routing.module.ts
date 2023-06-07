import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListNotificationComponent } from './list-notification/list-notification.component';
import { RouterModule, Routes } from '@angular/router';



const routes: Routes = [
  {
    path: '',
    children: [
      { path: 'list-notification', component: ListNotificationComponent, data: { title: "Notification List", breadcrumb: "Notification List" } },
   
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NotificationsRoutingModule { }
