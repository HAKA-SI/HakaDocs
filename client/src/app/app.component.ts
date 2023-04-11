import { AuthService } from './_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user.model';
import { LanguageService } from './core/services/language.service';
import { BackgroundService } from './_services/background.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  constructor(private authService : AuthService, private languageService: LanguageService, private backgroundService: BackgroundService) {}
  ngOnInit(): void {
    this.setCurrentuser();
    this.backgroundService.startChecking();
  }

  setCurrentuser() {
    const userdata = localStorage.getItem('user');
    
    if(!!userdata) {
      const decryptedUser = this.authService.decryptValue(userdata);
      const user: User = decryptedUser;
      this.authService.setCurrentuser(user);
      this.languageService.initLanguage();

    //  this.presenceService.createHubConnection(user);
    } else {
      localStorage.removeItem('user');
    //  localStorage.removeItem('token');
      this.authService.setCurrentuser(null);
    }
  }
}
