import { AuthService } from './_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user.model';
import { LanguageService } from './core/services/language.service';
import { BackgroundService } from './_services/background.service';
import { InactivityService } from './_services/inactivity.service';
import { OrdersService } from './_services/orders.service';
import { ApiNotificationService } from './_services/api-notification.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  constructor(private authService : AuthService, private languageService: LanguageService, private ordersService: OrdersService
    , private backgroundService: BackgroundService, private inactivityService:InactivityService,
    private apiNotificationService: ApiNotificationService
    ) {}
  ngOnInit(): void {
    this.setCurrentuser();
    this.backgroundService.startChecking();
  this.setSetPhysicalBasket();
  }

  setSetPhysicalBasket() {
       const physicalBasket =JSON.parse(localStorage.getItem('physicalBasket'));
       if(!!physicalBasket) this.ordersService.setBasket(physicalBasket);
       else this.ordersService.setBasket({});

  }

  setCurrentuser() {
    const userdata = localStorage.getItem('user');
    
    if(!!userdata) {
      const decryptedUser = this.authService.decryptValue(userdata);
      const user: User = decryptedUser;
      this.authService.setCurrentuser(user);
      this.apiNotificationService.createHubConnection(user);
      this.languageService.initLanguage();

    //  this.presenceService.createHubConnection(user);
    } else {
      localStorage.removeItem('user');
    //  localStorage.removeItem('token');
      this.authService.setCurrentuser(null);
    }
  }
}
