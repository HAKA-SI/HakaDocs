import { AuthService } from './components/auth/auth.service';
import { Component, OnInit } from '@angular/core';
import { User } from './shared/models/user.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  constructor(private authService : AuthService) {}
  ngOnInit(): void {
    this;this.setCurrentuser();
  }

  setCurrentuser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    if(!!user) {
      this.authService.setCurrentuser(user);

    //  this.presenceService.createHubConnection(user);
    }
  }
}
