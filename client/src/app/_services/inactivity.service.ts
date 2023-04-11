import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class InactivityService {

  private timeout: any;

  constructor(private authService:AuthService) {
    this.resetTimer();
    window.addEventListener('mousemove', () => this.resetTimer());
    window.addEventListener('click', () => this.resetTimer());
    window.addEventListener('keypress', () => this.resetTimer());
  }

  resetTimer() {
    clearTimeout(this.timeout);
    this.timeout = setTimeout(() => {
      if(this.authService.isLogin())
       alert('L\'utilisateur est inactif depuis une #) minutes.');
    }, 1800000); // 1 heure en millisecondes
  }
}
