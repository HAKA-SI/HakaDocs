import { Component, OnInit } from '@angular/core';
import { ApiNotification } from 'src/app/_models/notification.model';
import { ApiNotificationService } from 'src/app/_services/api-notification.service';
import { ConfirmService } from 'src/app/core/services/confirm.service';

@Component({
  selector: 'app-list-notification',
  templateUrl: './list-notification.component.html',
  styleUrls: ['./list-notification.component.scss']
})
export class ListNotificationComponent implements OnInit {

  notifications: any[] = [];
  selectedIds = [];

  constructor(private apiNotificationService: ApiNotificationService, private confirmService: ConfirmService) {
  }

  ngOnInit(): void {
    this.getNotifications();
  }

  getNotifications() {
    this.apiNotificationService.getNotificationThread().subscribe((notifications: ApiNotification[]) => {
      this.notifications = notifications;
    }

    )
  }

  selectNotificication(id: number) {
    const idx = this.selectedIds.findIndex(a => a == id);
    if (idx === -1) this.selectedIds.push(id);
    else this.selectedIds.splice(idx, 1);
  }

  deleteNotifications() {
    this.confirmService.confirm('confirmation ', 'voulez vous vraiment effectuer cette suppression?')
      .subscribe(result => {
        if (result) {
          this.apiNotificationService.deleteNotification(this.selectedIds).subscribe(() => {
            this.notifications = this.notifications.filter(a => !this.selectedIds.includes(a.id));
            this.apiNotificationService.startPolling();
          })
        }
      })
  }

  markAsReaded() {
   this.apiNotificationService.markNotificationAsReaded(this.selectedIds).subscribe(() => {
    this.selectedIds.forEach(element => {
      const idx = this.notifications.findIndex(a => a.id ===element);
        this.notifications[idx].read=true;
        this.notifications[idx].isSelected=false;

    });
    this.apiNotificationService.startPolling();
   });

  }

}
