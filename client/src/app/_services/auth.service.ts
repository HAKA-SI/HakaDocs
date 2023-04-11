import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, ReplaySubject } from 'rxjs';
import { User } from 'src/app/_models/user.model';
import { environment } from 'src/environments/environment';
import { map, tap } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { EncryptStorage } from 'encrypt-storage';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = environment.apiUrl;
  private currentuserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentuserSource.asObservable();
  jwtHelper = new JwtHelperService();
  redirectUrl: string;
  decodedToken: any;
  encryptStoragge = new EncryptStorage("3bbf2ab9b46f4cb5ad0f7f7330d58576ab137160eba045b4bc1fd7a66011cc4970a4bab5fb9c4e2b8da2e73dbdeffe86");

  constructor(
    private http: HttpClient,
    //private presenceService: PresenceService,
    private router: Router
  ) { }

  login(model: any) {
    const url = this.baseUrl + "auth/login";
    return this.http.post<User>(url, model).pipe(
      tap((user: User) => {
          this.setCurrentuser(user);
      })
    );
  }

  roleMatch(allowedRoles): boolean {
    let isMatch = false;
    const userRoles = this.decodedToken.role as Array<string>;
    allowedRoles.forEach(element => {
      if (userRoles.includes(element)) {
        isMatch = true;
        return;
      }
    });
    return isMatch;
  }


  // isLogin(): boolean {
  //   var response: boolean;
  //   if (
  //     this.currentUser$.subscribe((user) => {
  //       if (user) return (response = true);
  //       return (response = false);
  //     })
  //   )
  //     return response;
  // }

  isLogin(): boolean {
    const userdata = localStorage.getItem('user');
    if(!!userdata) {
      const decryptedUser:User = this.decryptValue(userdata);
      return !this.jwtHelper.isTokenExpired(decryptedUser.token);
    }
    return false;
   
  }

  // loggedIn() {
  //   const token = localStorage.getItem('token');
  //   const isExpired = !this.jwtHelper.isTokenExpired(token);
  //   return isExpired;
  // }


  logout(gotoHomePage: boolean = true) {
    localStorage.removeItem('user');
    //  localStorage.removeItem('clients');
    this.currentuserSource.next(null);
    // this.presenceService.stopHubConnection();
    // this.dashboardService.stopHubConnection();
    if (gotoHomePage)
      this.router.navigate(['/auth/login']);
    this.decodedToken = null;
  }

  setCurrentuser(user: User) {
    if (!!user) {
      user.roles = [];
      const roles = this.getDecodeToken(user.token).role;
      Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);
      const encryptedUser = this.encryptValue(user);
      localStorage.setItem('user', encryptedUser);
      this.currentuserSource.next(user);
    }
    this.currentuserSource.next(user);
  }



  getDecodeToken(token) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  // isLogin(): boolean {
  //   var response: boolean;
  //   if (
  //     this.currentUser$.subscribe((user) => {
  //       if (user) return (response = true);
  //       return (response = false);
  //     })
  //   )
  //     return response;
  // }


  register(values: any) {
    return this.http.post(this.baseUrl + 'auth/register', values);
  }

  setUserLoginPassword(id: number, loginModel: any) {
    return this.http.post(this.baseUrl + 'account/' + id + '/setLoginPassword', loginModel)
      .pipe(
        map((user: User) => {
          if (user) {
            this.setCurrentuser(user);
          }
        })
      );
  }

  encryptValue(value) {
    if (!!value) {
      const encryptedValue = this.encryptStoragge.encryptValue(value);
      return encryptedValue
    }

    return null;
  }

  decryptValue(value) {
    if (!!value) {
      const decryptedValue = this.encryptStoragge.decryptValue(value);
      return decryptedValue;
    }
  }

}
