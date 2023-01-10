import { AuthService } from './components/auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { User } from './shared/models/user.model';
import { LanguageService } from './core/services/language.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  constructor(private authService : AuthService, private languageService: LanguageService) {}
  ngOnInit(): void {
    this;this.setCurrentuser();
  }

  setCurrentuser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    if(!!user) {
      this.authService.setCurrentuser(user);
      this.languageService.initLanguage();

    //  this.presenceService.createHubConnection(user);
    } else {
      localStorage.removeItem('user');
      localStorage.removeItem('token');
      this.authService.setCurrentuser(null);
    }
  }
}
