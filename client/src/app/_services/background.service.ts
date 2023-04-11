import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { User } from '../_models/user.model';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BackgroundService {
  constructor(private authService:AuthService) { 
    
  }

  startChecking() {
    setInterval(() => {
      this.checkIfTokenExpired();
    }, 1000);
  }

  checkIfTokenExpired() {

    this.authService.currentUser$.pipe(take(1)).subscribe((user) => {
     if(!!user) {
     if (this.authService.isLogin()) return;
     alert("votre session est terminée...");
     this.authService.logout(true);
     }
      
    });
    // console.log(this.loggedUser);
    
    // if (!!this.loggedUser) {
    //   const isExpired = this.authService.isLogin();
    //  console.log(this.loggedUser.id);
    //  console.log(isExpired);
     
      // if(!isExpired) return;
      // alert("votre session a expiré...");
      // this.authService.logout(true);
    // }
    // La fonction que vous souhaitez vérifier toutes les secondes
    // console.log("Fonction vérifiée toutes les secondes");
    // console.log(this.authService.decodedToken);
    
  }
}
